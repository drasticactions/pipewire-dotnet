#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_audio_info_amr
{
    [NativeTypeName("uint32_t")]
    public uint rate;

    [NativeTypeName("uint32_t")]
    public uint channels;

    [NativeTypeName("enum spa_audio_amr_band_mode")]
    public spa_audio_amr_band_mode band_mode;
}
