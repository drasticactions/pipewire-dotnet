#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_audio_info_ra
{
    [NativeTypeName("uint32_t")]
    public uint rate;

    [NativeTypeName("uint32_t")]
    public uint channels;
}
