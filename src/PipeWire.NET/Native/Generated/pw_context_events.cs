#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_context_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> destroy;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> free;

    [NativeTypeName("void (*)(void *, struct pw_impl_client *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_impl_client*, void> check_access;

    [NativeTypeName("void (*)(void *, struct pw_global *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_global*, void> global_added;

    [NativeTypeName("void (*)(void *, struct pw_global *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_global*, void> global_removed;

    [NativeTypeName("void (*)(void *, struct pw_impl_node *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_impl_node*, void> driver_added;

    [NativeTypeName("void (*)(void *, struct pw_impl_node *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_impl_node*, void> driver_removed;
}
