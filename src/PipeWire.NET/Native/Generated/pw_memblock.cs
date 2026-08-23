#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_memblock
{
    [NativeTypeName("struct pw_mempool *")]
    public pw_mempool* pool;

    [NativeTypeName("uint32_t")]
    public uint id;

    public int @ref;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint type;

    public int fd;

    [NativeTypeName("uint32_t")]
    public uint size;

    [NativeTypeName("struct pw_memmap *")]
    public pw_memmap* map;
}
