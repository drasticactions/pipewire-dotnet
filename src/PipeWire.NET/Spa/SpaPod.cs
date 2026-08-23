using System.Text;
using PipeWire.Native;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Spa;

public readonly unsafe struct SpaPod
{
    private readonly spa_pod* _pod;
    private readonly uint _maxSize;

    public SpaPod(spa_pod* pod)
        : this(pod, pod is null ? 0 : (uint)SpaPodBuilder.PodHeaderSize + pod->size)
    {
    }

    public SpaPod(spa_pod* pod, uint maxSize)
    {
        _pod = pod;
        _maxSize = maxSize;
    }

    public spa_pod* Handle => _pod;

    public bool IsNull => _pod is null;

    public uint Type => _pod is null ? SPA_TYPE_None : _pod->type;

    public uint BodySize => _pod is null ? 0 : _pod->size;

    public uint TotalSize => _pod is null ? 0 : (uint)SpaPodBuilder.PodHeaderSize + _pod->size;

    public ReadOnlySpan<byte> Body
        => _pod is null ? default : new ReadOnlySpan<byte>((byte*)_pod + SpaPodBuilder.PodHeaderSize, checked((int)BodySize));

    public ReadOnlySpan<byte> Bytes
        => _pod is null ? default : new ReadOnlySpan<byte>(_pod, checked((int)TotalSize));

    public SpaPod Value
    {
        get
        {
            if (_pod is null || _pod->type != SPA_TYPE_Choice)
            {
                return this;
            }

            return AsChoice().FirstValue;
        }
    }

    public bool AsBool() => TryGetBool(out bool value) ? value : throw Mismatch("Bool");

    public uint AsId() => TryGetId(out uint value) ? value : throw Mismatch("Id");

    public int AsInt() => TryGetInt(out int value) ? value : throw Mismatch("Int");

    public long AsLong() => TryGetLong(out long value) ? value : throw Mismatch("Long");

    public float AsFloat() => TryGetFloat(out float value) ? value : throw Mismatch("Float");

    public double AsDouble() => TryGetDouble(out double value) ? value : throw Mismatch("Double");

    public long AsFd() => TryGetFd(out long value) ? value : throw Mismatch("Fd");

    public spa_rectangle AsRectangle() => TryGetRectangle(out spa_rectangle value) ? value : throw Mismatch("Rectangle");

    public spa_fraction AsFraction() => TryGetFraction(out spa_fraction value) ? value : throw Mismatch("Fraction");

    public string AsString() => TryGetString(out string? value) ? value! : throw Mismatch("String");

    public ReadOnlySpan<byte> AsBytes()
        => Type == SPA_TYPE_Bytes ? Body : throw Mismatch("Bytes");

    public bool TryGetBool(out bool value)
    {
        bool ok = TryRead(SPA_TYPE_Bool, out int raw);
        value = raw != 0;
        return ok;
    }

    public bool TryGetId(out uint value) => TryRead(SPA_TYPE_Id, out value);

    public bool TryGetInt(out int value) => TryRead(SPA_TYPE_Int, out value);

    public bool TryGetLong(out long value) => TryRead(SPA_TYPE_Long, out value);

    public bool TryGetFloat(out float value) => TryRead(SPA_TYPE_Float, out value);

    public bool TryGetDouble(out double value) => TryRead(SPA_TYPE_Double, out value);

    public bool TryGetFd(out long value) => TryRead(SPA_TYPE_Fd, out value);

    public bool TryGetRectangle(out spa_rectangle value) => TryRead(SPA_TYPE_Rectangle, out value);

    public bool TryGetFraction(out spa_fraction value) => TryRead(SPA_TYPE_Fraction, out value);

    public bool TryGetString(out string? value)
    {
        if (Type != SPA_TYPE_String || BodySize == 0)
        {
            value = null;
            return false;
        }

        ReadOnlySpan<byte> body = Body;
        int end = body.IndexOf((byte)0);
        value = Encoding.UTF8.GetString(end < 0 ? body : body[..end]);
        return true;
    }

    public SpaPodObject AsObject()
        => Type == SPA_TYPE_Object ? new SpaPodObject((spa_pod_object*)_pod) : throw Mismatch("Object");

    public SpaPodStruct AsStruct()
        => Type == SPA_TYPE_Struct ? new SpaPodStruct(_pod) : throw Mismatch("Struct");

    public SpaPodArray AsArray()
        => Type == SPA_TYPE_Array ? new SpaPodArray((spa_pod_array*)_pod) : throw Mismatch("Array");

    public SpaPodChoice AsChoice()
        => Type == SPA_TYPE_Choice ? new SpaPodChoice((spa_pod_choice*)_pod) : throw Mismatch("Choice");

    public SpaPodSequence AsSequence()
        => Type == SPA_TYPE_Sequence ? new SpaPodSequence((spa_pod_sequence*)_pod) : throw Mismatch("Sequence");

    public override string ToString()
        => IsNull ? "<null pod>" : $"{SpaPodTypes.GetName(Type)}({BodySize} bytes)";

    private bool TryRead<T>(uint type, out T value)
        where T : unmanaged
    {
        if (_pod is null || _pod->type != type || _pod->size < sizeof(T) ||
            _maxSize < SpaPodBuilder.PodHeaderSize + (uint)sizeof(T))
        {
            value = default;
            return false;
        }

        value = *(T*)((byte*)_pod + SpaPodBuilder.PodHeaderSize);
        return true;
    }

    private InvalidOperationException Mismatch(string expected)
        => new($"Expected a {expected} POD but found {SpaPodTypes.GetName(Type)}.");
}
