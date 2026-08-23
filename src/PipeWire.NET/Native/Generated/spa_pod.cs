#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_pod
{
    [NativeTypeName("uint32_t")]
    public uint size;

    [NativeTypeName("uint32_t")]
    public uint type;
}
