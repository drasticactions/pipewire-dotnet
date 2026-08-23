#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_audio_info_wma
{
    [NativeTypeName("uint32_t")]
    public uint rate;

    [NativeTypeName("uint32_t")]
    public uint channels;

    [NativeTypeName("uint32_t")]
    public uint bitrate;

    [NativeTypeName("uint32_t")]
    public uint block_align;

    [NativeTypeName("enum spa_audio_wma_profile")]
    public spa_audio_wma_profile profile;
}
