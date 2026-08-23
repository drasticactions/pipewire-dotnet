#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_audio_info_dts
{
    [NativeTypeName("uint32_t")]
    public uint rate;

    [NativeTypeName("uint32_t")]
    public uint channels;

    [NativeTypeName("enum spa_audio_dts_ext_type")]
    public spa_audio_dts_ext_type ext_type;
}
