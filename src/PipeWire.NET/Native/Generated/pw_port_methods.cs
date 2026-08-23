#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_port_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *, struct spa_hook *, const struct pw_port_events *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_hook*, pw_port_events*, void*, int> add_listener;

    [NativeTypeName("int (*)(void *, uint32_t *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint*, uint, int> subscribe_params;

    [NativeTypeName("int (*)(void *, int, uint32_t, uint32_t, uint32_t, const struct spa_pod *)")]
    public delegate* unmanaged[Cdecl]<void*, int, uint, uint, uint, spa_pod*, int> enum_params;
}
