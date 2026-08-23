#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_audio_info_iec958
{
    [NativeTypeName("enum spa_audio_iec958_codec")]
    public spa_audio_iec958_codec codec;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint rate;
}
