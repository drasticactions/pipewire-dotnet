#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_meta_header
{
    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint offset;

    [NativeTypeName("int64_t")]
    public long pts;

    [NativeTypeName("int64_t")]
    public long dts_offset;

    [NativeTypeName("uint64_t")]
    public ulong seq;
}
