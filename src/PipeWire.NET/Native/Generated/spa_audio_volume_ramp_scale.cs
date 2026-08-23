#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_audio_volume_ramp_scale : uint
{
    SPA_AUDIO_VOLUME_RAMP_INVALID,
    SPA_AUDIO_VOLUME_RAMP_LINEAR,
    SPA_AUDIO_VOLUME_RAMP_CUBIC,
}
