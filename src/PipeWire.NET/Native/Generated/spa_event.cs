#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_event
{
    [NativeTypeName("struct spa_pod")]
    public spa_pod pod;

    [NativeTypeName("struct spa_event_body")]
    public spa_event_body body;
}
