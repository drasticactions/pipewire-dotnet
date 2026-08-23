#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_audio_info_dsp
{
    [NativeTypeName("enum spa_audio_format")]
    public spa_audio_format format;
}
