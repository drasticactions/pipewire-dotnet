#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_buffers
{
    [NativeTypeName("struct pw_memblock *")]
    public pw_memblock* mem;

    [NativeTypeName("struct spa_buffer **")]
    public spa_buffer** buffers;

    [NativeTypeName("uint32_t")]
    public uint n_buffers;

    [NativeTypeName("uint32_t")]
    public uint flags;
}
