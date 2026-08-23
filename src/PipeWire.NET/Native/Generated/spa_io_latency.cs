#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_io_latency
{
    [NativeTypeName("struct spa_fraction")]
    public spa_fraction rate;

    [NativeTypeName("uint64_t")]
    public ulong min;

    [NativeTypeName("uint64_t")]
    public ulong max;
}
