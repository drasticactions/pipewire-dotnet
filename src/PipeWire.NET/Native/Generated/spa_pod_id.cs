#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_pod_id
{
    [NativeTypeName("struct spa_pod")]
    public spa_pod pod;

    [NativeTypeName("uint32_t")]
    public uint value;

    [NativeTypeName("int32_t")]
    public int _padding;
}
