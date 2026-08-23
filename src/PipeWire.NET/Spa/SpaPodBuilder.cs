using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;
using PipeWire.Native;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Spa;

public sealed unsafe class SpaPodBuilder : IDisposable
{
    public const int PodAlign = 8;

    public const int PodHeaderSize = 8;

    private const uint FlagBody = 1 << 0;
    private const uint FlagFirst = 1 << 1;

    private byte[] _buffer;
    private int _offset;
    private uint _flags;
    private Frame[] _frames = new Frame[8];
    private int _depth;
    private bool _disposed;

    public SpaPodBuilder(int initialCapacity = 1024)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(initialCapacity);
        _buffer = ArrayPool<byte>.Shared.Rent(Math.Max(initialCapacity, PodAlign));
    }

    public int Length => _offset;

    public int Depth => _depth;

    public ReadOnlySpan<byte> WrittenSpan => _buffer.AsSpan(0, _offset);

    public byte[] ToArray()
    {
        if (_depth != 0)
        {
            throw new InvalidOperationException($"{_depth} POD container(s) are still open; dispose their frames before reading the result.");
        }

        return WrittenSpan.ToArray();
    }

    public void Reset()
    {
        _offset = 0;
        _flags = 0;
        _depth = 0;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        ArrayPool<byte>.Shared.Return(_buffer);
        _buffer = [];
    }

    public SpaPodFrame PushObject(uint objectType, uint objectId)
    {
        int offset = _offset;

        WriteHeader(sizeof(uint) * 2, SPA_TYPE_Object);
        WriteUInt32(objectType);
        WriteUInt32(objectId);

        return Push(sizeof(uint) * 2, SPA_TYPE_Object, offset);
    }

    public SpaPodFrame PushStruct()
    {
        int offset = _offset;
        WriteHeader(0, SPA_TYPE_Struct);
        return Push(0, SPA_TYPE_Struct, offset);
    }

    public SpaPodFrame PushArray()
    {
        int offset = _offset;
        WriteHeader(0, SPA_TYPE_Array);
        return Push(0, SPA_TYPE_Array, offset);
    }

    public SpaPodFrame PushChoice(spa_choice_type choiceType, uint flags = 0)
    {
        int offset = _offset;

        WriteHeader(sizeof(uint) * 2, SPA_TYPE_Choice);
        WriteUInt32((uint)choiceType);
        WriteUInt32(flags);

        return Push(sizeof(uint) * 2, SPA_TYPE_Choice, offset);
    }

    public SpaPodFrame PushSequence(uint unit = 0)
    {
        int offset = _offset;
        WriteHeader(sizeof(uint) * 2, SPA_TYPE_Sequence);
        WriteUInt32(unit);
        WriteUInt32(0);
        return Push(sizeof(uint) * 2, SPA_TYPE_Sequence, offset);
    }

    public void AddProperty(uint key, uint flags = 0)
    {
        WriteUInt32(key);
        WriteUInt32(flags);
    }

    public void AddControl(uint offset, uint type)
    {
        WriteUInt32(offset);
        WriteUInt32(type);
    }

    public void AddNone() => Primitive(0, SPA_TYPE_None, default);

    public void AddBool(bool value) => AddInt32(SPA_TYPE_Bool, value ? 1 : 0);

    public void AddId(uint value) => AddInt32(SPA_TYPE_Id, unchecked((int)value));

    public void AddInt(int value) => AddInt32(SPA_TYPE_Int, value);

    public void AddLong(long value) => PrimitiveValue(SPA_TYPE_Long, value);

    public void AddFloat(float value) => PrimitiveValue(SPA_TYPE_Float, value);

    public void AddDouble(double value) => PrimitiveValue(SPA_TYPE_Double, value);

    public void AddFd(long fd) => PrimitiveValue(SPA_TYPE_Fd, fd);

    public void AddRectangle(uint width, uint height)
        => PrimitiveValue(SPA_TYPE_Rectangle, new spa_rectangle { width = width, height = height });

    public void AddFraction(uint num, uint denom)
        => PrimitiveValue(SPA_TYPE_Fraction, new spa_fraction { num = num, denom = denom });

    public void AddString(string? value)
    {
        value ??= string.Empty;

        int byteCount = Encoding.UTF8.GetByteCount(value);
        byte[]? rented = byteCount > 256 ? ArrayPool<byte>.Shared.Rent(byteCount + 1) : null;
        Span<byte> body = rented ?? stackalloc byte[byteCount + 1];

        Encoding.UTF8.GetBytes(value, body);
        body[byteCount] = 0;
        Primitive(byteCount + 1, SPA_TYPE_String, body[..(byteCount + 1)]);

        if (rented is not null)
        {
            ArrayPool<byte>.Shared.Return(rented);
        }
    }

    public void AddBytes(ReadOnlySpan<byte> value) => Primitive(value.Length, SPA_TYPE_Bytes, value);

    public void AddPointer(uint type, void* value)
        => PrimitiveValue(SPA_TYPE_Pointer, new spa_pod_pointer_body { type = type, _padding = 0, value = value });

    public void AddArray<T>(uint childType, int childSize, ReadOnlySpan<T> values)
        where T : unmanaged
    {
        if (sizeof(T) != childSize)
        {
            throw new ArgumentException($"Element type {typeof(T).Name} is {sizeof(T)} bytes, but childSize is {childSize}.", nameof(values));
        }

        using (PushArray())
        {
            ReadOnlySpan<byte> bytes = MemoryMarshal.AsBytes(values);
            for (int i = 0; i < values.Length; i++)
            {
                Primitive(childSize, childType, bytes.Slice(i * childSize, childSize));
            }

            if (values.Length == 0)
            {
                WriteHeader(childSize, childType);
                _flags &= ~FlagFirst;
            }
        }
    }

    public void AddIdArray(ReadOnlySpan<uint> values) => AddArray(SPA_TYPE_Id, sizeof(uint), values);

    public void AddIntArray(ReadOnlySpan<int> values) => AddArray(SPA_TYPE_Int, sizeof(int), values);

    public void AddChoiceEnum(Action<SpaPodBuilder> write, uint flags = 0)
    {
        ArgumentNullException.ThrowIfNull(write);

        using (PushChoice(spa_choice_type.SPA_CHOICE_Enum, flags))
        {
            write(this);
        }
    }

    public void AddChoiceRange(int defaultValue, int min, int max)
    {
        using (PushChoice(spa_choice_type.SPA_CHOICE_Range))
        {
            AddInt(defaultValue);
            AddInt(min);
            AddInt(max);
        }
    }

    public void AddChoiceRectangleRange(
        uint defaultWidth,
        uint defaultHeight,
        uint minWidth,
        uint minHeight,
        uint maxWidth,
        uint maxHeight)
    {
        using (PushChoice(spa_choice_type.SPA_CHOICE_Range))
        {
            AddRectangle(defaultWidth, defaultHeight);
            AddRectangle(minWidth, minHeight);
            AddRectangle(maxWidth, maxHeight);
        }
    }

    internal void Pop(int depth)
    {
        if (depth != _depth)
        {
            throw new InvalidOperationException($"POD frames must be closed in the order they were opened (closing depth {depth}, innermost open frame is {_depth}).");
        }

        ref Frame frame = ref _frames[depth - 1];

        if ((_flags & FlagFirst) != 0)
        {
            WriteHeader(0, SPA_TYPE_None);
        }

        MemoryMarshal.Write(_buffer.AsSpan(frame.Offset), (uint)frame.Size);
        MemoryMarshal.Write(_buffer.AsSpan(frame.Offset + 4), frame.Type);

        _depth--;
        _flags = frame.Flags;
        Pad();
    }

    private SpaPodFrame Push(int size, uint type, int offset)
    {
        if (_depth == _frames.Length)
        {
            Array.Resize(ref _frames, _frames.Length * 2);
        }

        _frames[_depth] = new Frame(offset, size, type, _flags);
        _depth++;

        _flags = type is SPA_TYPE_Array or SPA_TYPE_Choice ? FlagFirst | FlagBody : _flags;

        return new SpaPodFrame(this, _depth);
    }

    private void AddInt32(uint type, int value) => PrimitiveValue(type, value);

    private void PrimitiveValue<T>(uint type, T value)
        where T : unmanaged
        => Primitive(sizeof(T), type, new ReadOnlySpan<byte>(&value, sizeof(T)));

    private void Primitive(int bodySize, uint type, ReadOnlySpan<byte> body)
    {
        if (_flags != FlagBody)
        {
            _flags &= ~FlagFirst;
            WriteHeader(bodySize, type);
        }

        if (!body.IsEmpty)
        {
            Raw(body);
        }

        Pad();
    }

    private void WriteHeader(int size, uint type)
    {
        spa_pod header = new() { size = (uint)size, type = type };
        Raw(new ReadOnlySpan<byte>(&header, PodHeaderSize));
    }

    private void WriteUInt32(uint value) => Raw(new ReadOnlySpan<byte>(&value, sizeof(uint)));

    private void Pad()
    {
        if (_flags == FlagBody)
        {
            return;
        }

        int padding = ((_offset + PodAlign - 1) & ~(PodAlign - 1)) - _offset;
        if (padding > 0)
        {
            Span<byte> zeroes = stackalloc byte[PodAlign];
            zeroes.Clear();
            Raw(zeroes[..padding]);
        }
    }

    private void Raw(ReadOnlySpan<byte> data)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        EnsureCapacity(data.Length);

        data.CopyTo(_buffer.AsSpan(_offset));
        _offset += data.Length;

        for (int i = 0; i < _depth; i++)
        {
            _frames[i].Size += data.Length;
        }
    }

    private void EnsureCapacity(int additional)
    {
        if (_offset + additional <= _buffer.Length)
        {
            return;
        }

        if ((long)_offset + additional > SPA_POD_MAX_SIZE)
        {
            throw new InvalidOperationException($"A POD cannot exceed SPA_POD_MAX_SIZE ({SPA_POD_MAX_SIZE} bytes).");
        }

        byte[] grown = ArrayPool<byte>.Shared.Rent(Math.Max(_buffer.Length * 2, _offset + additional));
        _buffer.AsSpan(0, _offset).CopyTo(grown);
        ArrayPool<byte>.Shared.Return(_buffer);
        _buffer = grown;
    }

    private struct Frame(int offset, int size, uint type, uint flags)
    {
        public readonly int Offset = offset;

        public int Size = size;

        public readonly uint Type = type;

        public readonly uint Flags = flags;
    }
}
