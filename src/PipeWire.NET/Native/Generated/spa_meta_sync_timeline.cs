#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_meta_sync_timeline
{
    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint padding;

    [NativeTypeName("uint64_t")]
    public ulong acquire_point;

    [NativeTypeName("uint64_t")]
    public ulong release_point;
}
