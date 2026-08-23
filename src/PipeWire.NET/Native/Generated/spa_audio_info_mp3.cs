#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_audio_info_mp3
{
    [NativeTypeName("uint32_t")]
    public uint rate;

    [NativeTypeName("uint32_t")]
    public uint channels;

    [NativeTypeName("enum spa_audio_mp3_channel_mode")]
    public spa_audio_mp3_channel_mode channel_mode;
}
