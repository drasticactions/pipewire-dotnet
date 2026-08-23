using System.Runtime.CompilerServices;
using System.Threading;

namespace PipeWire;

public sealed class PipeWireRingBuffer
{
    private readonly byte[] _buffer;
    private readonly int _mask;

    private int _writeIndex;
    private int _readIndex;
    private long _droppedBytes;

    public PipeWireRingBuffer(int capacityBytes)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacityBytes, 1);

        int capacity = 1;
        while (capacity < capacityBytes)
        {
            capacity <<= 1;
        }

        _buffer = new byte[capacity];
        _mask = capacity - 1;
    }

    public int Capacity => _buffer.Length;

    public int Available => Volatile.Read(ref _writeIndex) - Volatile.Read(ref _readIndex);

    public int FreeSpace => Capacity - Available;

    public long DroppedBytes => Volatile.Read(ref _droppedBytes);

    public int Write(ReadOnlySpan<byte> data)
    {
        int write = _writeIndex;
        int free = Capacity - (write - Volatile.Read(ref _readIndex));
        int count = Math.Min(data.Length, free);

        if (count < data.Length)
        {
            Interlocked.Add(ref _droppedBytes, data.Length - count);
        }

        if (count == 0)
        {
            return 0;
        }

        int offset = write & _mask;
        int firstChunk = Math.Min(count, Capacity - offset);
        data[..firstChunk].CopyTo(_buffer.AsSpan(offset));

        if (firstChunk < count)
        {
            data[firstChunk..count].CopyTo(_buffer.AsSpan(0));
        }

        Volatile.Write(ref _writeIndex, write + count);
        return count;
    }

    public int Read(Span<byte> destination)
    {
        int read = _readIndex;
        int available = Volatile.Read(ref _writeIndex) - read;
        int count = Math.Min(destination.Length, available);

        if (count == 0)
        {
            return 0;
        }

        int offset = read & _mask;
        int firstChunk = Math.Min(count, Capacity - offset);
        _buffer.AsSpan(offset, firstChunk).CopyTo(destination);

        if (firstChunk < count)
        {
            _buffer.AsSpan(0, count - firstChunk).CopyTo(destination[firstChunk..]);
        }

        Volatile.Write(ref _readIndex, read + count);
        return count;
    }

    public void Clear() => Volatile.Write(ref _readIndex, Volatile.Read(ref _writeIndex));
}
