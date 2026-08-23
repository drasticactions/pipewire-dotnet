#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_event_body
{
    [NativeTypeName("struct spa_pod_object_body")]
    public spa_pod_object_body body;
}
