#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_registry_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *, struct spa_hook *, const struct pw_registry_events *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_hook*, pw_registry_events*, void*, int> add_listener;

    [NativeTypeName("void *(*)(void *, uint32_t, const char *, uint32_t, size_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, sbyte*, uint, nuint, void*> bind;

    [NativeTypeName("int (*)(void *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, int> destroy;
}
