#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_protocol_server
{
    [NativeTypeName("struct spa_list")]
    public spa_list link;

    [NativeTypeName("struct pw_protocol *")]
    public pw_protocol* protocol;

    [NativeTypeName("struct pw_impl_core *")]
    public pw_impl_core* core;

    [NativeTypeName("struct spa_list")]
    public spa_list client_list;

    [NativeTypeName("void (*)(struct pw_protocol_server *)")]
    public delegate* unmanaged[Cdecl]<pw_protocol_server*, void> destroy;

    public partial struct pw_impl_core
    {
    }
}
