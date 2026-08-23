#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_pod_prop
{
    [NativeTypeName("uint32_t")]
    public uint key;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("struct spa_pod")]
    public spa_pod value;
}
