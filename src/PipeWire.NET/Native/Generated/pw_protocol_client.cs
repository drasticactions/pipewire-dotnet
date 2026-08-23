#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_protocol_client
{
    [NativeTypeName("struct spa_list")]
    public spa_list link;

    [NativeTypeName("struct pw_protocol *")]
    public pw_protocol* protocol;

    [NativeTypeName("struct pw_core *")]
    public pw_core* core;

    [NativeTypeName("int (*)(struct pw_protocol_client *, const struct spa_dict *, void (*)(void *, int), void *)")]
    public delegate* unmanaged[Cdecl]<pw_protocol_client*, spa_dict*, delegate* unmanaged[Cdecl]<void*, int, void>, void*, int> connect;

    [NativeTypeName("int (*)(struct pw_protocol_client *, int, _Bool)")]
    public delegate* unmanaged[Cdecl]<pw_protocol_client*, int, byte, int> connect_fd;

    [NativeTypeName("int (*)(struct pw_protocol_client *)")]
    public delegate* unmanaged[Cdecl]<pw_protocol_client*, int> steal_fd;

    [NativeTypeName("void (*)(struct pw_protocol_client *)")]
    public delegate* unmanaged[Cdecl]<pw_protocol_client*, void> disconnect;

    [NativeTypeName("void (*)(struct pw_protocol_client *)")]
    public delegate* unmanaged[Cdecl]<pw_protocol_client*, void> destroy;

    [NativeTypeName("int (*)(struct pw_protocol_client *, _Bool)")]
    public delegate* unmanaged[Cdecl]<pw_protocol_client*, byte, int> set_paused;
}
