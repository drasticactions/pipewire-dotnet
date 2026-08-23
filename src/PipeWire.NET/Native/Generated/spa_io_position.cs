#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public partial struct spa_io_position
{
    [NativeTypeName("struct spa_io_clock")]
    public spa_io_clock clock;

    [NativeTypeName("struct spa_io_video_size")]
    public spa_io_video_size video;

    [NativeTypeName("int64_t")]
    public long offset;

    [NativeTypeName("uint32_t")]
    public uint state;

    [NativeTypeName("uint32_t")]
    public uint n_segments;

    [NativeTypeName("struct spa_io_segment[8]")]
    public _segments_e__FixedBuffer segments;

    [InlineArray(8)]
    public partial struct _segments_e__FixedBuffer
    {
        public spa_io_segment e0;
    }
}
