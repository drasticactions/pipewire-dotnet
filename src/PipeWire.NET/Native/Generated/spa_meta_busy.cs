#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_meta_busy
{
    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint count;
}
