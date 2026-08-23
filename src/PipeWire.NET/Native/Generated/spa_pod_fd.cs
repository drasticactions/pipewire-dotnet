#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_pod_fd
{
    [NativeTypeName("struct spa_pod")]
    public spa_pod pod;

    [NativeTypeName("int64_t")]
    public long value;
}
