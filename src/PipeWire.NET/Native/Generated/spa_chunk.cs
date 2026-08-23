#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_chunk
{
    [NativeTypeName("uint32_t")]
    public uint offset;

    [NativeTypeName("uint32_t")]
    public uint size;

    [NativeTypeName("int32_t")]
    public int stride;

    [NativeTypeName("int32_t")]
    public int flags;
}
