#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_protocol_implementation
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("struct pw_protocol_client *(*)(struct pw_protocol *, struct pw_core *, const struct spa_dict *)")]
    public delegate* unmanaged[Cdecl]<pw_protocol*, pw_core*, spa_dict*, pw_protocol_client*> new_client;

    [NativeTypeName("struct pw_protocol_server *(*)(struct pw_protocol *, struct pw_impl_core *, const struct spa_dict *)")]
    public delegate* unmanaged[Cdecl]<pw_protocol*, pw_impl_core*, spa_dict*, pw_protocol_server*> add_server;

    [NativeTypeName("struct pw_protocol_server *(*)(struct pw_protocol *, struct pw_impl_core *, int, int, const struct spa_dict *)")]
    public delegate* unmanaged[Cdecl]<pw_protocol*, pw_impl_core*, int, int, spa_dict*, pw_protocol_server*> add_fd_server;
}
