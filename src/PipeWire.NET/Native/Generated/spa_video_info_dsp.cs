#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_video_info_dsp
{
    [NativeTypeName("enum spa_video_format")]
    public spa_video_format format;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint64_t")]
    public ulong modifier;
}
