#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_client_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *, struct spa_hook *, const struct pw_client_events *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_hook*, pw_client_events*, void*, int> add_listener;

    [NativeTypeName("int (*)(void *, uint32_t, int, const char *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, int, sbyte*, int> error;

    [NativeTypeName("int (*)(void *, const struct spa_dict *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_dict*, int> update_properties;

    [NativeTypeName("int (*)(void *, uint32_t, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, uint, int> get_permissions;

    [NativeTypeName("int (*)(void *, uint32_t, const struct pw_permission *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, pw_permission*, int> update_permissions;
}
