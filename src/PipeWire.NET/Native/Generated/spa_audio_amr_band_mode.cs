#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_audio_amr_band_mode : uint
{
    SPA_AUDIO_AMR_BAND_MODE_UNKNOWN,
    SPA_AUDIO_AMR_BAND_MODE_NB,
    SPA_AUDIO_AMR_BAND_MODE_WB,
}
