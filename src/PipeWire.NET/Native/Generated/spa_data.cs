#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_data
{
    [NativeTypeName("uint32_t")]
    public uint type;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("int64_t")]
    public long fd;

    [NativeTypeName("uint32_t")]
    public uint mapoffset;

    [NativeTypeName("uint32_t")]
    public uint maxsize;

    public void* data;

    [NativeTypeName("struct spa_chunk *")]
    public spa_chunk* chunk;
}
