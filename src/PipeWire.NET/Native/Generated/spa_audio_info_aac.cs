#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_audio_info_aac
{
    [NativeTypeName("uint32_t")]
    public uint rate;

    [NativeTypeName("uint32_t")]
    public uint channels;

    [NativeTypeName("uint32_t")]
    public uint bitrate;

    [NativeTypeName("enum spa_audio_aac_stream_format")]
    public spa_audio_aac_stream_format stream_format;
}
