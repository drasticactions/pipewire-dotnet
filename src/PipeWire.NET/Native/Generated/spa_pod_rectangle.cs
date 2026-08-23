#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_pod_rectangle
{
    [NativeTypeName("struct spa_pod")]
    public spa_pod pod;

    [NativeTypeName("struct spa_rectangle")]
    public spa_rectangle value;
}
