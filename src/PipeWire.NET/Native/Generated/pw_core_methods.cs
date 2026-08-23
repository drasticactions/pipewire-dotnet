#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_core_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *, struct spa_hook *, const struct pw_core_events *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_hook*, pw_core_events*, void*, int> add_listener;

    [NativeTypeName("int (*)(void *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, int> hello;

    [NativeTypeName("int (*)(void *, uint32_t, int)")]
    public delegate* unmanaged[Cdecl]<void*, uint, int, int> sync;

    [NativeTypeName("int (*)(void *, uint32_t, int)")]
    public delegate* unmanaged[Cdecl]<void*, uint, int, int> pong;

    [NativeTypeName("int (*)(void *, uint32_t, int, int, const char *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, int, int, sbyte*, int> error;

    [NativeTypeName("struct pw_registry *(*)(void *, uint32_t, size_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, nuint, pw_registry*> get_registry;

    [NativeTypeName("void *(*)(void *, const char *, const char *, uint32_t, const struct spa_dict *, size_t)")]
    public delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, uint, spa_dict*, nuint, void*> create_object;

    [NativeTypeName("int (*)(void *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, void*, int> destroy;
}
