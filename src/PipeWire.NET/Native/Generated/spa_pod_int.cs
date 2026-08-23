#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_pod_int
{
    [NativeTypeName("struct spa_pod")]
    public spa_pod pod;

    [NativeTypeName("int32_t")]
    public int value;

    [NativeTypeName("int32_t")]
    public int _padding;
}
