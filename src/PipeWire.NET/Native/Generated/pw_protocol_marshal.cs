#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_protocol_marshal
{
    [NativeTypeName("const char *")]
    public sbyte* type;

    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint n_client_methods;

    [NativeTypeName("uint32_t")]
    public uint n_server_methods;

    [NativeTypeName("const void *")]
    public void* client_marshal;

    [NativeTypeName("const void *")]
    public void* server_demarshal;

    [NativeTypeName("const void *")]
    public void* server_marshal;

    [NativeTypeName("const void *")]
    public void* client_demarshal;
}
