#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_pod_control
{
    [NativeTypeName("uint32_t")]
    public uint offset;

    [NativeTypeName("uint32_t")]
    public uint type;

    [NativeTypeName("struct spa_pod")]
    public spa_pod value;
}
