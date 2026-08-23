#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_io_range
{
    [NativeTypeName("uint64_t")]
    public ulong offset;

    [NativeTypeName("uint32_t")]
    public uint min_size;

    [NativeTypeName("uint32_t")]
    public uint max_size;
}
