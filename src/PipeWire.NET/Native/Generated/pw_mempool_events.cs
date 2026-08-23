#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_mempool_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> destroy;

    [NativeTypeName("void (*)(void *, struct pw_memblock *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_memblock*, void> added;

    [NativeTypeName("void (*)(void *, struct pw_memblock *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_memblock*, void> removed;
}
