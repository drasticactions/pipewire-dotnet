#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_node_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *, struct spa_hook *, const struct spa_node_events *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_hook*, spa_node_events*, void*, int> add_listener;

    [NativeTypeName("int (*)(void *, const struct spa_node_callbacks *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_node_callbacks*, void*, int> set_callbacks;

    [NativeTypeName("int (*)(void *, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, int> sync;

    [NativeTypeName("int (*)(void *, int, uint32_t, uint32_t, uint32_t, const struct spa_pod *)")]
    public delegate* unmanaged[Cdecl]<void*, int, uint, uint, uint, spa_pod*, int> enum_params;

    [NativeTypeName("int (*)(void *, uint32_t, uint32_t, const struct spa_pod *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, uint, spa_pod*, int> set_param;

    [NativeTypeName("int (*)(void *, uint32_t, void *, size_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, void*, nuint, int> set_io;

    [NativeTypeName("int (*)(void *, const struct spa_command *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_command*, int> send_command;

    [NativeTypeName("int (*)(void *, enum spa_direction, uint32_t, const struct spa_dict *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_direction, uint, spa_dict*, int> add_port;

    [NativeTypeName("int (*)(void *, enum spa_direction, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, spa_direction, uint, int> remove_port;

    [NativeTypeName("int (*)(void *, int, enum spa_direction, uint32_t, uint32_t, uint32_t, uint32_t, const struct spa_pod *)")]
    public delegate* unmanaged[Cdecl]<void*, int, spa_direction, uint, uint, uint, uint, spa_pod*, int> port_enum_params;

    [NativeTypeName("int (*)(void *, enum spa_direction, uint32_t, uint32_t, uint32_t, const struct spa_pod *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_direction, uint, uint, uint, spa_pod*, int> port_set_param;

    [NativeTypeName("int (*)(void *, enum spa_direction, uint32_t, uint32_t, struct spa_buffer **, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, spa_direction, uint, uint, spa_buffer**, uint, int> port_use_buffers;

    [NativeTypeName("int (*)(void *, enum spa_direction, uint32_t, uint32_t, void *, size_t)")]
    public delegate* unmanaged[Cdecl]<void*, spa_direction, uint, uint, void*, nuint, int> port_set_io;

    [NativeTypeName("int (*)(void *, uint32_t, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, uint, int> port_reuse_buffer;

    [NativeTypeName("int (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, int> process;
}
