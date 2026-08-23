#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_process_latency_info
{
    public float quantum;

    [NativeTypeName("int32_t")]
    public int rate;

    [NativeTypeName("int64_t")]
    public long ns;
}
