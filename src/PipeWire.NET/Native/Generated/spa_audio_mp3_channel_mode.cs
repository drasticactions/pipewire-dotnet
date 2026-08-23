#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_audio_mp3_channel_mode : uint
{
    SPA_AUDIO_MP3_CHANNEL_MODE_UNKNOWN,
    SPA_AUDIO_MP3_CHANNEL_MODE_MONO,
    SPA_AUDIO_MP3_CHANNEL_MODE_STEREO,
    SPA_AUDIO_MP3_CHANNEL_MODE_JOINTSTEREO,
    SPA_AUDIO_MP3_CHANNEL_MODE_DUAL,
}
