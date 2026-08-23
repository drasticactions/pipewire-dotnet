using PipeWire.Native;

namespace PipeWire.Spa;

public static unsafe class SpaMeta
{
    public static spa_meta* Find(spa_buffer* buffer, uint type)
    {
        if (buffer is null || buffer->metas is null)
        {
            return null;
        }

        for (uint i = 0; i < buffer->n_metas; i++)
        {
            if (buffer->metas[i].type == type)
            {
                return &buffer->metas[i];
            }
        }

        return null;
    }

    public static spa_meta* Find(spa_buffer* buffer, spa_meta_type type) => Find(buffer, (uint)type);

    public static void* FindData(spa_buffer* buffer, uint type, nuint size)
    {
        spa_meta* meta = Find(buffer, type);
        return meta is not null && meta->size >= size ? meta->data : null;
    }

    public static void* FindData(spa_buffer* buffer, spa_meta_type type, nuint size)
        => FindData(buffer, (uint)type, size);

    public static T* FindData<T>(spa_buffer* buffer, uint type)
        where T : unmanaged
        => (T*)FindData(buffer, type, (nuint)sizeof(T));

    public static T* FindData<T>(spa_buffer* buffer, spa_meta_type type)
        where T : unmanaged
        => (T*)FindData(buffer, (uint)type, (nuint)sizeof(T));

    public static bool HasFeatures(spa_buffer* buffer, uint type, uint features)
    {
        if (buffer is null || buffer->metas is null)
        {
            return false;
        }

        for (uint i = 0; i < buffer->n_metas; i++)
        {
            uint candidate = buffer->metas[i].type;
            if ((candidate >> 16) == type && (candidate & features) == features)
            {
                return true;
            }
        }

        return false;
    }

    public static uint TypeWithFeatures(uint type, uint features) => (type << 16) | features;
}

public readonly unsafe struct SpaMetaHeader
{
    private readonly spa_meta_header* _header;

    internal SpaMetaHeader(spa_meta_header* header) => _header = header;

    public spa_meta_header* Handle => _header;

    public bool IsNull => _header is null;

    public uint Flags
    {
        get => _header is null ? 0 : _header->flags;
        set
        {
            if (_header is not null)
            {
                _header->flags = value;
            }
        }
    }

    public uint Offset
    {
        get => _header is null ? 0 : _header->offset;
        set
        {
            if (_header is not null)
            {
                _header->offset = value;
            }
        }
    }

    public long Pts
    {
        get => _header is null ? 0 : _header->pts;
        set
        {
            if (_header is not null)
            {
                _header->pts = value;
            }
        }
    }

    public long DtsOffset
    {
        get => _header is null ? 0 : _header->dts_offset;
        set
        {
            if (_header is not null)
            {
                _header->dts_offset = value;
            }
        }
    }

    public ulong Seq
    {
        get => _header is null ? 0 : _header->seq;
        set
        {
            if (_header is not null)
            {
                _header->seq = value;
            }
        }
    }

    public bool HasFlag(uint flag) => (Flags & flag) != 0;
}

public readonly unsafe struct SpaMetaRegion
{
    private readonly spa_meta_region* _region;

    internal SpaMetaRegion(spa_meta_region* region) => _region = region;

    public spa_meta_region* Handle => _region;

    public bool IsNull => _region is null;

    public bool IsValid => _region is not null && _region->region.size.width != 0 && _region->region.size.height != 0;

    public spa_region Region
    {
        get => _region is null ? default : _region->region;
        set
        {
            if (_region is not null)
            {
                _region->region = value;
            }
        }
    }

    public spa_point Position
    {
        get => _region is null ? default : _region->region.position;
        set
        {
            if (_region is not null)
            {
                _region->region.position = value;
            }
        }
    }

    public spa_rectangle Size
    {
        get => _region is null ? default : _region->region.size;
        set
        {
            if (_region is not null)
            {
                _region->region.size = value;
            }
        }
    }

    public int X => _region is null ? 0 : _region->region.position.x;

    public int Y => _region is null ? 0 : _region->region.position.y;

    public uint Width => _region is null ? 0 : _region->region.size.width;

    public uint Height => _region is null ? 0 : _region->region.size.height;

    public void Set(int x, int y, uint width, uint height)
    {
        if (_region is null)
        {
            return;
        }

        _region->region.position.x = x;
        _region->region.position.y = y;
        _region->region.size.width = width;
        _region->region.size.height = height;
    }
}

public readonly unsafe struct SpaMetaRegionArray
{
    private readonly spa_meta_region* _regions;
    private readonly int _capacity;

    internal SpaMetaRegionArray(spa_meta_region* regions, int capacity)
    {
        _regions = regions;
        _capacity = capacity;
    }

    public spa_meta_region* Handle => _regions;

    public bool IsNull => _regions is null;

    public int Capacity => _regions is null ? 0 : _capacity;

    public Span<spa_meta_region> Span
        => _regions is null ? default : new Span<spa_meta_region>(_regions, _capacity);

    public int Count
    {
        get
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (_regions[i].region.size.width == 0 || _regions[i].region.size.height == 0)
                {
                    return i;
                }
            }

            return Capacity;
        }
    }

    public ReadOnlySpan<spa_meta_region> Regions => Span[..Count];

    public SpaMetaRegion this[int index]
    {
        get
        {
            if (_regions is null || index < 0 || index >= _capacity)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, $"The damage array holds {Capacity} region(s).");
            }

            return new SpaMetaRegion(&_regions[index]);
        }
    }

    public void Terminate(int count)
    {
        if (_regions is null || count < 0 || count >= _capacity)
        {
            return;
        }

        _regions[count].region = default;
    }

    public Enumerator GetEnumerator() => new(_regions, Count);

    public struct Enumerator
    {
        private readonly spa_meta_region* _regions;
        private readonly int _count;
        private int _index;

        internal Enumerator(spa_meta_region* regions, int count)
        {
            _regions = regions;
            _count = count;
            _index = -1;
        }

        public readonly SpaMetaRegion Current => new(&_regions[_index]);

        public bool MoveNext() => ++_index < _count;
    }
}

public readonly unsafe struct SpaMetaBitmap
{
    private readonly spa_meta_bitmap* _bitmap;
    private readonly uint _available;

    internal SpaMetaBitmap(spa_meta_bitmap* bitmap, uint available)
    {
        _bitmap = bitmap;
        _available = available;
    }

    public spa_meta_bitmap* Handle => _bitmap;

    public bool IsNull => _bitmap is null;

    public bool IsValid => _bitmap is not null && _bitmap->format != 0;

    public spa_video_format Format
    {
        get => _bitmap is null ? 0 : (spa_video_format)_bitmap->format;
        set
        {
            if (_bitmap is not null)
            {
                _bitmap->format = (uint)value;
            }
        }
    }

    public spa_rectangle Size
    {
        get => _bitmap is null ? default : _bitmap->size;
        set
        {
            if (_bitmap is not null)
            {
                _bitmap->size = value;
            }
        }
    }

    public uint Width => _bitmap is null ? 0 : _bitmap->size.width;

    public uint Height => _bitmap is null ? 0 : _bitmap->size.height;

    public int Stride
    {
        get => _bitmap is null ? 0 : _bitmap->stride;
        set
        {
            if (_bitmap is not null)
            {
                _bitmap->stride = value;
            }
        }
    }

    public uint Offset
    {
        get => _bitmap is null ? 0 : _bitmap->offset;
        set
        {
            if (_bitmap is not null)
            {
                _bitmap->offset = value;
            }
        }
    }

    public Span<byte> Pixels
    {
        get
        {
            if (!IsValid)
            {
                return default;
            }

            uint offset = _bitmap->offset;
            if (offset < (uint)sizeof(spa_meta_bitmap) || offset >= _available)
            {
                return default;
            }

            int stride = _bitmap->stride;
            uint height = _bitmap->size.height;
            if (stride <= 0 || height == 0)
            {
                return default;
            }

            ulong wanted = (ulong)(uint)stride * height;
            ulong room = _available - offset;
            return new Span<byte>((byte*)_bitmap + offset, (int)Math.Min(wanted, room));
        }
    }
}

public readonly unsafe struct SpaMetaCursor
{
    private readonly spa_meta_cursor* _cursor;
    private readonly uint _available;

    internal SpaMetaCursor(spa_meta_cursor* cursor, uint available)
    {
        _cursor = cursor;
        _available = available;
    }

    public spa_meta_cursor* Handle => _cursor;

    public bool IsNull => _cursor is null;

    public bool IsValid => _cursor is not null && _cursor->id != 0;

    public uint Id
    {
        get => _cursor is null ? 0 : _cursor->id;
        set
        {
            if (_cursor is not null)
            {
                _cursor->id = value;
            }
        }
    }

    public uint Flags
    {
        get => _cursor is null ? 0 : _cursor->flags;
        set
        {
            if (_cursor is not null)
            {
                _cursor->flags = value;
            }
        }
    }

    public spa_point Position
    {
        get => _cursor is null ? default : _cursor->position;
        set
        {
            if (_cursor is not null)
            {
                _cursor->position = value;
            }
        }
    }

    public spa_point Hotspot
    {
        get => _cursor is null ? default : _cursor->hotspot;
        set
        {
            if (_cursor is not null)
            {
                _cursor->hotspot = value;
            }
        }
    }

    public uint BitmapOffset
    {
        get => _cursor is null ? 0 : _cursor->bitmap_offset;
        set
        {
            if (_cursor is not null)
            {
                _cursor->bitmap_offset = value;
            }
        }
    }

    public bool HasBitmap
    {
        get
        {
            if (_cursor is null)
            {
                return false;
            }

            uint offset = _cursor->bitmap_offset;
            return offset >= (uint)sizeof(spa_meta_cursor) &&
                   offset + (uint)sizeof(spa_meta_bitmap) <= _available;
        }
    }

    public SpaMetaBitmap Bitmap
        => HasBitmap
            ? new SpaMetaBitmap((spa_meta_bitmap*)((byte*)_cursor + _cursor->bitmap_offset), _available - _cursor->bitmap_offset)
            : default;
}

public readonly unsafe struct SpaMetaBusy
{
    private readonly spa_meta_busy* _busy;

    internal SpaMetaBusy(spa_meta_busy* busy) => _busy = busy;

    public spa_meta_busy* Handle => _busy;

    public bool IsNull => _busy is null;

    public bool IsBusy => _busy is not null && _busy->count > 0;

    public uint Flags
    {
        get => _busy is null ? 0 : _busy->flags;
        set
        {
            if (_busy is not null)
            {
                _busy->flags = value;
            }
        }
    }

    public uint Count
    {
        get => _busy is null ? 0 : _busy->count;
        set
        {
            if (_busy is not null)
            {
                _busy->count = value;
            }
        }
    }
}

public readonly unsafe struct SpaMetaVideoTransform
{
    private readonly spa_meta_videotransform* _transform;

    internal SpaMetaVideoTransform(spa_meta_videotransform* transform) => _transform = transform;

    public spa_meta_videotransform* Handle => _transform;

    public bool IsNull => _transform is null;

    public spa_meta_videotransform_value Transform
    {
        get => _transform is null ? spa_meta_videotransform_value.SPA_META_TRANSFORMATION_None : (spa_meta_videotransform_value)_transform->transform;
        set
        {
            if (_transform is not null)
            {
                _transform->transform = (uint)value;
            }
        }
    }
}

public readonly unsafe struct SpaMetaSyncTimeline
{
    private readonly spa_meta_sync_timeline* _timeline;

    internal SpaMetaSyncTimeline(spa_meta_sync_timeline* timeline) => _timeline = timeline;

    public spa_meta_sync_timeline* Handle => _timeline;

    public bool IsNull => _timeline is null;

    public uint Flags
    {
        get => _timeline is null ? 0 : _timeline->flags;
        set
        {
            if (_timeline is not null)
            {
                _timeline->flags = value;
            }
        }
    }

    public ulong AcquirePoint
    {
        get => _timeline is null ? 0 : _timeline->acquire_point;
        set
        {
            if (_timeline is not null)
            {
                _timeline->acquire_point = value;
            }
        }
    }

    public ulong ReleasePoint
    {
        get => _timeline is null ? 0 : _timeline->release_point;
        set
        {
            if (_timeline is not null)
            {
                _timeline->release_point = value;
            }
        }
    }
}
