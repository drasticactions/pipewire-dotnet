using PipeWire.Native;

namespace PipeWire;

public readonly unsafe struct PipeWireBuffer
{
    private readonly pw_buffer* _buffer;

    internal PipeWireBuffer(pw_buffer* buffer) => _buffer = buffer;

    public pw_buffer* Handle => _buffer;

    public bool IsNull => _buffer is null;

    public int DataCount => _buffer is null || _buffer->buffer is null ? 0 : (int)_buffer->buffer->n_datas;

    public ulong RequestedFrames => _buffer is null ? 0 : _buffer->requested;

    public ulong Time => _buffer is null ? 0 : _buffer->time;

    public PipeWireBufferData this[int index]
    {
        get
        {
            if (_buffer is null || index < 0 || index >= DataCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, $"The buffer has {DataCount} data block(s).");
            }

            return new PipeWireBufferData(&_buffer->buffer->datas[index]);
        }
    }
}

public readonly unsafe struct PipeWireBufferData
{
    private readonly spa_data* _data;

    internal PipeWireBufferData(spa_data* data) => _data = data;

    public spa_data* Handle => _data;

    public bool HasMemory => _data is not null && _data->data is not null;

    public spa_data_type DataType => _data is null ? 0 : (spa_data_type)_data->type;

    public long Fd => _data is null ? -1 : _data->fd;

    public Span<byte> Memory
        => _data is null || _data->data is null ? default : new Span<byte>(_data->data, (int)_data->maxsize);

    public Span<byte> Chunked
    {
        get
        {
            if (_data is null || _data->data is null || _data->chunk is null)
            {
                return default;
            }

            uint offset = Math.Min(_data->chunk->offset, _data->maxsize);
            uint size = Math.Min(_data->chunk->size, _data->maxsize - offset);
            return new Span<byte>((byte*)_data->data + offset, (int)size);
        }
    }

    public uint Offset => _data is null || _data->chunk is null ? 0 : _data->chunk->offset;

    public uint Size => _data is null || _data->chunk is null ? 0 : _data->chunk->size;

    public int Stride => _data is null || _data->chunk is null ? 0 : _data->chunk->stride;

    public uint MaxSize => _data is null ? 0 : _data->maxsize;

    public void SetChunk(uint offset, uint size, int stride)
    {
        if (_data is null || _data->chunk is null)
        {
            return;
        }

        _data->chunk->offset = offset;
        _data->chunk->size = size;
        _data->chunk->stride = stride;
    }
}
