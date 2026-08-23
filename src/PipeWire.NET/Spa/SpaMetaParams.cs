using PipeWire.Native;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Spa;

public static unsafe class SpaMetaParams
{
    public static byte[] Build(uint type, int size)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);

        using var builder = new SpaPodBuilder(128);

        using (builder.PushObject(SPA_TYPE_OBJECT_ParamMeta, (uint)spa_param_type.SPA_PARAM_Meta))
        {
            builder.AddProperty((uint)spa_param_meta.SPA_PARAM_META_type);
            builder.AddId(type);
            builder.AddProperty((uint)spa_param_meta.SPA_PARAM_META_size);
            builder.AddInt(size);
        }

        return builder.ToArray();
    }

    public static byte[] Build(spa_meta_type type, int size) => Build((uint)type, size);

    public static byte[] Build(uint type, int defaultSize, int minSize, int maxSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(defaultSize);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(minSize);
        ArgumentOutOfRangeException.ThrowIfLessThan(maxSize, minSize);

        using var builder = new SpaPodBuilder(128);

        using (builder.PushObject(SPA_TYPE_OBJECT_ParamMeta, (uint)spa_param_type.SPA_PARAM_Meta))
        {
            builder.AddProperty((uint)spa_param_meta.SPA_PARAM_META_type);
            builder.AddId(type);
            builder.AddProperty((uint)spa_param_meta.SPA_PARAM_META_size);
            builder.AddChoiceRange(defaultSize, minSize, maxSize);
        }

        return builder.ToArray();
    }

    public static byte[] Build(spa_meta_type type, int defaultSize, int minSize, int maxSize)
        => Build((uint)type, defaultSize, minSize, maxSize);

    public static byte[] BuildHeader() => Build(spa_meta_type.SPA_META_Header, sizeof(spa_meta_header));

    public static byte[] BuildVideoCrop() => Build(spa_meta_type.SPA_META_VideoCrop, sizeof(spa_meta_region));

    public static byte[] BuildVideoDamage(int maxRegions = 4)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxRegions);

        int size = sizeof(spa_meta_region);
        return Build(spa_meta_type.SPA_META_VideoDamage, size * maxRegions, size, size * maxRegions);
    }

    public static byte[] BuildCursor(uint maxWidth = 256, uint maxHeight = 256, uint bytesPerPixel = 4)
        => Build(spa_meta_type.SPA_META_Cursor, CursorSize(maxWidth, maxHeight, bytesPerPixel));

    public static byte[] BuildBusy() => Build(spa_meta_type.SPA_META_Busy, sizeof(spa_meta_busy));

    public static byte[] BuildVideoTransform()
        => Build(spa_meta_type.SPA_META_VideoTransform, sizeof(spa_meta_videotransform));

    public static byte[] BuildSyncTimeline()
        => Build(spa_meta_type.SPA_META_SyncTimeline, sizeof(spa_meta_sync_timeline));

    public static int CursorSize(uint maxWidth = 256, uint maxHeight = 256, uint bytesPerPixel = 4)
    {
        ArgumentOutOfRangeException.ThrowIfZero(maxWidth);
        ArgumentOutOfRangeException.ThrowIfZero(maxHeight);
        ArgumentOutOfRangeException.ThrowIfZero(bytesPerPixel);

        return checked(sizeof(spa_meta_cursor) + sizeof(spa_meta_bitmap) + (int)(maxWidth * maxHeight * bytesPerPixel));
    }

    public static int DamageSize(int maxRegions) => checked(sizeof(spa_meta_region) * maxRegions);
}

public static unsafe class SpaBufferParams
{
    public static byte[] Build(
        int buffers = 8,
        int blocks = 1,
        int size = 0,
        int stride = 0,
        int align = 16,
        uint dataTypes = 0,
        int minBuffers = 0,
        int maxBuffers = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(buffers);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(blocks);
        ArgumentOutOfRangeException.ThrowIfNegative(size);
        ArgumentOutOfRangeException.ThrowIfNegative(align);

        using var builder = new SpaPodBuilder(256);

        using (builder.PushObject(SPA_TYPE_OBJECT_ParamBuffers, (uint)spa_param_type.SPA_PARAM_Buffers))
        {
            builder.AddProperty((uint)spa_param_buffers.SPA_PARAM_BUFFERS_buffers);
            if (minBuffers > 0 && maxBuffers >= minBuffers)
            {
                builder.AddChoiceRange(buffers, minBuffers, maxBuffers);
            }
            else
            {
                builder.AddInt(buffers);
            }

            builder.AddProperty((uint)spa_param_buffers.SPA_PARAM_BUFFERS_blocks);
            builder.AddInt(blocks);

            builder.AddProperty((uint)spa_param_buffers.SPA_PARAM_BUFFERS_size);
            builder.AddInt(size);

            builder.AddProperty((uint)spa_param_buffers.SPA_PARAM_BUFFERS_stride);
            builder.AddInt(stride);

            builder.AddProperty((uint)spa_param_buffers.SPA_PARAM_BUFFERS_align);
            builder.AddInt(align);

            if (dataTypes != 0)
            {
                builder.AddProperty((uint)spa_param_buffers.SPA_PARAM_BUFFERS_dataType);
                using (builder.PushChoice(spa_choice_type.SPA_CHOICE_Flags))
                {
                    builder.AddInt(unchecked((int)dataTypes));
                }
            }
        }

        return builder.ToArray();
    }

    public static uint DataTypeMask(params spa_data_type[] types)
    {
        ArgumentNullException.ThrowIfNull(types);

        uint mask = 0;
        foreach (spa_data_type type in types)
        {
            mask |= 1u << (int)type;
        }

        return mask;
    }
}
