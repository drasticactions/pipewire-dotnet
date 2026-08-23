#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_pod_pointer
{
    [NativeTypeName("struct spa_pod")]
    public spa_pod pod;

    [NativeTypeName("struct spa_pod_pointer_body")]
    public spa_pod_pointer_body body;
}
