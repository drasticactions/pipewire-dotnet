using PipeWire.Native;
using PipeWire.Spa;

namespace PipeWire;

public readonly unsafe struct PipeWireBuffer
{
    private readonly pw_buffer* _buffer;

    public PipeWireBuffer(pw_buffer* buffer) => _buffer = buffer;

    public pw_buffer* Handle => _buffer;

    public bool IsNull => _buffer is null;

    public spa_buffer* Buffer => _buffer is null ? null : _buffer->buffer;

    public int DataCount => _buffer is null || _buffer->buffer is null ? 0 : (int)_buffer->buffer->n_datas;

    public int MetaCount => _buffer is null || _buffer->buffer is null ? 0 : (int)_buffer->buffer->n_metas;

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

    public spa_meta* FindMeta(uint type) => SpaMeta.Find(Buffer, type);

    public spa_meta* FindMeta(spa_meta_type type) => SpaMeta.Find(Buffer, (uint)type);

    public void* FindMetaData(uint type, nuint size) => SpaMeta.FindData(Buffer, type, size);

    public void* FindMetaData(spa_meta_type type, nuint size) => SpaMeta.FindData(Buffer, (uint)type, size);

    public T* FindMetaData<T>(uint type)
        where T : unmanaged
        => SpaMeta.FindData<T>(Buffer, type);

    public T* FindMetaData<T>(spa_meta_type type)
        where T : unmanaged
        => SpaMeta.FindData<T>(Buffer, (uint)type);

    public bool HasMetaFeatures(uint type, uint features) => SpaMeta.HasFeatures(Buffer, type, features);

    public bool HasMetaFeatures(spa_meta_type type, uint features) => SpaMeta.HasFeatures(Buffer, (uint)type, features);

    public bool TryGetMeta(uint type, out Span<byte> data)
    {
        spa_meta* meta = SpaMeta.Find(Buffer, type);
        if (meta is null || meta->data is null)
        {
            data = default;
            return false;
        }

        data = new Span<byte>(meta->data, checked((int)meta->size));
        return true;
    }

    public bool TryGetMeta(spa_meta_type type, out Span<byte> data) => TryGetMeta((uint)type, out data);

    public bool TryGetHeader(out SpaMetaHeader header)
    {
        var value = FindMetaData<spa_meta_header>(spa_meta_type.SPA_META_Header);
        header = new SpaMetaHeader(value);
        return value is not null;
    }

    public bool TryGetVideoCrop(out SpaMetaRegion crop)
    {
        var value = FindMetaData<spa_meta_region>(spa_meta_type.SPA_META_VideoCrop);
        crop = new SpaMetaRegion(value);
        return value is not null;
    }

    public bool TryGetVideoDamage(out SpaMetaRegionArray damage)
    {
        spa_meta* meta = FindMeta(spa_meta_type.SPA_META_VideoDamage);
        if (meta is null || meta->data is null || meta->size < sizeof(spa_meta_region))
        {
            damage = default;
            return false;
        }

        damage = new SpaMetaRegionArray((spa_meta_region*)meta->data, (int)(meta->size / sizeof(spa_meta_region)));
        return true;
    }

    public bool TryGetCursor(out SpaMetaCursor cursor)
    {
        spa_meta* meta = FindMeta(spa_meta_type.SPA_META_Cursor);
        if (meta is null || meta->data is null || meta->size < sizeof(spa_meta_cursor))
        {
            cursor = default;
            return false;
        }

        cursor = new SpaMetaCursor((spa_meta_cursor*)meta->data, meta->size);
        return true;
    }

    public bool TryGetBitmap(out SpaMetaBitmap bitmap)
    {
        spa_meta* meta = FindMeta(spa_meta_type.SPA_META_Bitmap);
        if (meta is null || meta->data is null || meta->size < sizeof(spa_meta_bitmap))
        {
            bitmap = default;
            return false;
        }

        bitmap = new SpaMetaBitmap((spa_meta_bitmap*)meta->data, meta->size);
        return true;
    }

    public bool TryGetBusy(out SpaMetaBusy busy)
    {
        var value = FindMetaData<spa_meta_busy>(spa_meta_type.SPA_META_Busy);
        busy = new SpaMetaBusy(value);
        return value is not null;
    }

    public bool TryGetVideoTransform(out SpaMetaVideoTransform transform)
    {
        var value = FindMetaData<spa_meta_videotransform>(spa_meta_type.SPA_META_VideoTransform);
        transform = new SpaMetaVideoTransform(value);
        return value is not null;
    }

    public bool TryGetSyncTimeline(out SpaMetaSyncTimeline timeline)
    {
        var value = FindMetaData<spa_meta_sync_timeline>(spa_meta_type.SPA_META_SyncTimeline);
        timeline = new SpaMetaSyncTimeline(value);
        return value is not null;
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
