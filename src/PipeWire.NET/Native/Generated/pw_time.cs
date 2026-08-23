#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct pw_time
{
    [NativeTypeName("int64_t")]
    public long now;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction rate;

    [NativeTypeName("uint64_t")]
    public ulong ticks;

    [NativeTypeName("int64_t")]
    public long delay;

    [NativeTypeName("uint64_t")]
    public ulong queued;

    [NativeTypeName("uint64_t")]
    public ulong buffered;

    [NativeTypeName("uint32_t")]
    public uint queued_buffers;

    [NativeTypeName("uint32_t")]
    public uint avail_buffers;

    [NativeTypeName("uint64_t")]
    public ulong size;
}
