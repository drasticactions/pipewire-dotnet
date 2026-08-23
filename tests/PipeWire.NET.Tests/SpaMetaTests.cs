using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using Xunit;

namespace PipeWire.Tests;

public unsafe class SpaMetaTests
{
    [Fact]
    public void HeaderParamMatchesTheCBuilder()
        => Assert.Equal(
            "380000000f00000005000400060000000100000000000000040000000300000001000000000000000200000000000000" +
            "04000000040000002000000000000000",
            Hex(SpaMetaParams.BuildHeader()));

    [Fact]
    public void VideoDamageParamMatchesTheCBuilder()
        => Assert.Equal(
            "500000000f00000005000400060000000100000000000000040000000300000003000000000000000200000000000000" +
            "1c000000130000000100000000000000040000000400000040000000100000004000000000000000",
            Hex(SpaMetaParams.BuildVideoDamage(4)));

    [Fact]
    public void BufferParamMatchesTheCBuilder()
        => Assert.Equal(
            "a80000000f00000004000400050000000100000000000000040000000400000008000000000000000200000000000000" +
            "040000000400000001000000000000000300000000000000040000000400000000907e00000000000400000000000000" +
            "0400000004000000001e0000000000000500000000000000040000000400000010000000000000000600000000000000" +
            "1400000013000000040000000000000004000000040000000c00000000000000",
            Hex(SpaBufferParams.Build(
                buffers: 8,
                blocks: 1,
                size: 1920 * 1080 * 4,
                stride: 1920 * 4,
                align: 16,
                dataTypes: SpaBufferParams.DataTypeMask(spa_data_type.SPA_DATA_MemFd, spa_data_type.SPA_DATA_DmaBuf))));

    [Fact]
    public void DataTypeMaskSetsOneBitPerType()
        => Assert.Equal(
            (1u << (int)spa_data_type.SPA_DATA_MemFd) | (1u << (int)spa_data_type.SPA_DATA_DmaBuf),
            SpaBufferParams.DataTypeMask(spa_data_type.SPA_DATA_MemFd, spa_data_type.SPA_DATA_DmaBuf));

    [Fact]
    public void FindMetaDataWalksTheMetaArray()
    {
        using var buffer = new FakeBuffer(
            (spa_meta_type.SPA_META_Busy, sizeof(spa_meta_busy)),
            (spa_meta_type.SPA_META_Header, sizeof(spa_meta_header)),
            (spa_meta_type.SPA_META_VideoDamage, sizeof(spa_meta_region) * 4));

        PipeWireBuffer view = buffer.View;

        Assert.Equal(3, view.MetaCount);
        Assert.True(view.FindMetaData<spa_meta_header>(spa_meta_type.SPA_META_Header) is not null);
        Assert.True(view.FindMetaData<spa_meta_busy>(spa_meta_type.SPA_META_Busy) is not null);
        Assert.True(view.FindMetaData<spa_meta_cursor>(spa_meta_type.SPA_META_Cursor) is null);
    }

    [Fact]
    public void FindMetaDataRejectsAnUndersizedMeta()
    {
        using var buffer = new FakeBuffer((spa_meta_type.SPA_META_Header, sizeof(spa_meta_header) - 1));

        Assert.True(buffer.View.FindMeta(spa_meta_type.SPA_META_Header) is not null);
        Assert.True(buffer.View.FindMetaData<spa_meta_header>(spa_meta_type.SPA_META_Header) is null);
        Assert.False(buffer.View.TryGetHeader(out _));
    }

    [Fact]
    public void MetaLookupOnAnEmptyBufferIsSafe()
    {
        var view = default(PipeWireBuffer);

        Assert.Equal(0, view.MetaCount);
        Assert.True(view.FindMeta(spa_meta_type.SPA_META_Header) is null);
        Assert.False(view.TryGetHeader(out SpaMetaHeader header));
        Assert.True(header.IsNull);
        Assert.Equal(0, header.Pts);

        header.Pts = 42;
        Assert.Equal(0, header.Pts);
    }

    [Fact]
    public void TryGetMetaExposesTheWholeMetaBlock()
    {
        using var buffer = new FakeBuffer((spa_meta_type.SPA_META_VideoDamage, sizeof(spa_meta_region) * 3));

        Assert.True(buffer.View.TryGetMeta(spa_meta_type.SPA_META_VideoDamage, out Span<byte> data));
        Assert.Equal(sizeof(spa_meta_region) * 3, data.Length);
    }

    [Fact]
    public void TheHeaderIsWritableThroughTheWrapper()
    {
        using var buffer = new FakeBuffer((spa_meta_type.SPA_META_Header, sizeof(spa_meta_header)));

        Assert.True(buffer.View.TryGetHeader(out SpaMetaHeader header));

        header.Pts = 1_234_567_890;
        header.Seq = 7;
        header.Flags = 1u << 0;

        spa_meta_header* raw = buffer.View.FindMetaData<spa_meta_header>(spa_meta_type.SPA_META_Header);

        Assert.Equal(1_234_567_890, raw->pts);
        Assert.Equal(7ul, raw->seq);
        Assert.True(header.HasFlag(1u << 0));
    }

    [Fact]
    public void VideoDamageStopsAtTheFirstInvalidRegion()
    {
        using var buffer = new FakeBuffer((spa_meta_type.SPA_META_VideoDamage, sizeof(spa_meta_region) * 4));

        Assert.True(buffer.View.TryGetVideoDamage(out SpaMetaRegionArray damage));
        Assert.Equal(4, damage.Capacity);
        Assert.Equal(0, damage.Count);

        damage[0].Set(0, 0, 640, 480);
        damage[1].Set(10, 20, 32, 32);
        damage.Terminate(2);

        Assert.Equal(2, damage.Count);
        Assert.Equal(2, damage.Regions.Length);

        var widths = new List<uint>();
        foreach (SpaMetaRegion region in damage)
        {
            widths.Add(region.Width);
        }

        Assert.Equal(new uint[] { 640, 32 }, widths);
        Assert.Equal(10, damage[1].X);
        Assert.True(damage[1].IsValid);
        Assert.False(damage[2].IsValid);
    }

    [Fact]
    public void VideoCropReadsAndWritesTheRegion()
    {
        using var buffer = new FakeBuffer((spa_meta_type.SPA_META_VideoCrop, sizeof(spa_meta_region)));

        Assert.True(buffer.View.TryGetVideoCrop(out SpaMetaRegion crop));

        crop.Set(4, 8, 800, 600);

        Assert.Equal(4, crop.X);
        Assert.Equal(8, crop.Y);
        Assert.Equal(800u, crop.Width);
        Assert.Equal(600u, crop.Height);
        Assert.True(crop.IsValid);
    }

    [Fact]
    public void TheCursorResolvesItsBitmapAndPixels()
    {
        const uint Width = 4;
        const uint Height = 2;
        int metaSize = SpaMetaParams.CursorSize(Width, Height);

        using var buffer = new FakeBuffer((spa_meta_type.SPA_META_Cursor, metaSize));

        Assert.True(buffer.View.TryGetCursor(out SpaMetaCursor cursor));
        Assert.False(cursor.IsValid);
        Assert.False(cursor.HasBitmap);

        cursor.Id = 1;
        cursor.Position = new spa_point { x = 100, y = 200 };
        cursor.Hotspot = new spa_point { x = 2, y = 3 };
        cursor.BitmapOffset = (uint)sizeof(spa_meta_cursor);

        Assert.True(cursor.IsValid);
        Assert.True(cursor.HasBitmap);
        Assert.Equal(100, cursor.Position.x);
        Assert.Equal(3, cursor.Hotspot.y);

        SpaMetaBitmap bitmap = cursor.Bitmap;
        Assert.False(bitmap.IsValid);

        bitmap.Format = spa_video_format.SPA_VIDEO_FORMAT_BGRA;
        bitmap.Size = new spa_rectangle { width = Width, height = Height };
        bitmap.Stride = (int)(Width * 4);
        bitmap.Offset = (uint)sizeof(spa_meta_bitmap);

        Assert.True(bitmap.IsValid);
        Assert.Equal((int)(Width * Height * 4), bitmap.Pixels.Length);

        bitmap.Pixels[0] = 0xAB;
        Assert.Equal(0xAB, ((byte*)bitmap.Handle)[sizeof(spa_meta_bitmap)]);
    }

    [Fact]
    public void ThePixelSpanIsClampedToTheMetaBlock()
    {
        using var buffer = new FakeBuffer((spa_meta_type.SPA_META_Bitmap, sizeof(spa_meta_bitmap) + 16));

        Assert.True(buffer.View.TryGetBitmap(out SpaMetaBitmap bitmap));

        bitmap.Format = spa_video_format.SPA_VIDEO_FORMAT_BGRA;
        bitmap.Size = new spa_rectangle { width = 64, height = 64 };
        bitmap.Stride = 64 * 4;
        bitmap.Offset = (uint)sizeof(spa_meta_bitmap);

        Assert.Equal(16, bitmap.Pixels.Length);
    }

    [Fact]
    public void BusyAndTransformAndTimelineRoundTrip()
    {
        using var buffer = new FakeBuffer(
            (spa_meta_type.SPA_META_Busy, sizeof(spa_meta_busy)),
            (spa_meta_type.SPA_META_VideoTransform, sizeof(spa_meta_videotransform)),
            (spa_meta_type.SPA_META_SyncTimeline, sizeof(spa_meta_sync_timeline)));

        Assert.True(buffer.View.TryGetBusy(out SpaMetaBusy busy));
        Assert.False(busy.IsBusy);
        busy.Count = 2;
        Assert.True(busy.IsBusy);

        Assert.True(buffer.View.TryGetVideoTransform(out SpaMetaVideoTransform transform));
        transform.Transform = spa_meta_videotransform_value.SPA_META_TRANSFORMATION_Flipped90;
        Assert.Equal(spa_meta_videotransform_value.SPA_META_TRANSFORMATION_Flipped90, transform.Transform);

        Assert.True(buffer.View.TryGetSyncTimeline(out SpaMetaSyncTimeline timeline));
        timeline.AcquirePoint = 11;
        timeline.ReleasePoint = 12;
        Assert.Equal(11ul, timeline.AcquirePoint);
        Assert.Equal(12ul, timeline.ReleasePoint);
    }

    [Fact]
    public void FeatureMetasAreMatchedOnTheirHighHalf()
    {
        uint type = SpaMeta.TypeWithFeatures(0x10, 0b11);
        using var buffer = new FakeBuffer((type, 0));

        Assert.True(buffer.View.HasMetaFeatures(0x10, 0b01));
        Assert.True(buffer.View.HasMetaFeatures(0x10, 0b11));
        Assert.False(buffer.View.HasMetaFeatures(0x10, 0b100));
        Assert.False(buffer.View.HasMetaFeatures(0x11, 0b01));
    }

    private static string Hex(byte[] pod) => Convert.ToHexString(pod).ToLowerInvariant();

    private sealed class FakeBuffer : IDisposable
    {
        private readonly pw_buffer* _pwBuffer;
        private readonly spa_buffer* _buffer;
        private readonly spa_meta* _metas;
        private readonly void*[] _blocks;

        public FakeBuffer(params (uint Type, int Size)[] metas)
        {
            _pwBuffer = (pw_buffer*)NativeMemory.AllocZeroed((nuint)sizeof(pw_buffer));
            _buffer = (spa_buffer*)NativeMemory.AllocZeroed((nuint)sizeof(spa_buffer));
            _metas = (spa_meta*)NativeMemory.AllocZeroed((nuint)metas.Length, (nuint)sizeof(spa_meta));
            _blocks = new void*[metas.Length];

            for (int i = 0; i < metas.Length; i++)
            {
                _blocks[i] = metas[i].Size > 0 ? NativeMemory.AllocZeroed((nuint)metas[i].Size) : null;
                _metas[i].type = metas[i].Type;
                _metas[i].size = (uint)metas[i].Size;
                _metas[i].data = _blocks[i];
            }

            _buffer->n_metas = (uint)metas.Length;
            _buffer->metas = _metas;
            _pwBuffer->buffer = _buffer;
        }

        public FakeBuffer(params (spa_meta_type Type, int Size)[] metas)
            : this(Array.ConvertAll(metas, m => ((uint)m.Type, m.Size)))
        {
        }

        public PipeWireBuffer View => new(_pwBuffer);

        public void Dispose()
        {
            foreach (void* block in _blocks)
            {
                if (block is not null)
                {
                    NativeMemory.Free(block);
                }
            }

            NativeMemory.Free(_metas);
            NativeMemory.Free(_buffer);
            NativeMemory.Free(_pwBuffer);
        }
    }
}
