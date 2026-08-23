#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_latency_info
{
    [NativeTypeName("enum spa_direction")]
    public spa_direction direction;

    public float min_quantum;

    public float max_quantum;

    [NativeTypeName("int32_t")]
    public int min_rate;

    [NativeTypeName("int32_t")]
    public int max_rate;

    [NativeTypeName("int64_t")]
    public long min_ns;

    [NativeTypeName("int64_t")]
    public long max_ns;
}
