#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_node_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *, const struct spa_node_info *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_node_info*, void> info;

    [NativeTypeName("void (*)(void *, enum spa_direction, uint32_t, const struct spa_port_info *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_direction, uint, spa_port_info*, void> port_info;

    [NativeTypeName("void (*)(void *, int, int, uint32_t, const void *)")]
    public delegate* unmanaged[Cdecl]<void*, int, int, uint, void*, void> result;

    [NativeTypeName("void (*)(void *, const struct spa_event *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_event*, void> @event;
}
