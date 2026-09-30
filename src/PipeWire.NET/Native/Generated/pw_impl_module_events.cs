#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_impl_module_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> destroy;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> free;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> initialized;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> registered;
}
