#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_audio_dts_ext_type : uint
{
    SPA_AUDIO_DTS_EXT_UNKNOWN,
    SPA_AUDIO_DTS_EXT_NONE,
    SPA_AUDIO_DTS_EXT_HD_HRA,
    SPA_AUDIO_DTS_EXT_HD_MA,
}
