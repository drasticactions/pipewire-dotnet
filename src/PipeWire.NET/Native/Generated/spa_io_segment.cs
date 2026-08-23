#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_io_segment
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint64_t")]
    public ulong start;

    [NativeTypeName("uint64_t")]
    public ulong duration;

    public double rate;

    [NativeTypeName("uint64_t")]
    public ulong position;

    [NativeTypeName("struct spa_io_segment_bar")]
    public spa_io_segment_bar bar;

    [NativeTypeName("struct spa_io_segment_video")]
    public spa_io_segment_video video;
}
