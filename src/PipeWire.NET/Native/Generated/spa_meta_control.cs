#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_meta_control
{
    [NativeTypeName("struct spa_pod_sequence")]
    public spa_pod_sequence sequence;
}
