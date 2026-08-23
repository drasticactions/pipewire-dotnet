#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_link_info
{
    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("uint32_t")]
    public uint output_node_id;

    [NativeTypeName("uint32_t")]
    public uint output_port_id;

    [NativeTypeName("uint32_t")]
    public uint input_node_id;

    [NativeTypeName("uint32_t")]
    public uint input_port_id;

    [NativeTypeName("uint64_t")]
    public ulong change_mask;

    [NativeTypeName("enum pw_link_state")]
    public pw_link_state state;

    [NativeTypeName("const char *")]
    public sbyte* error;

    [NativeTypeName("struct spa_pod *")]
    public spa_pod* format;

    [NativeTypeName("struct spa_dict *")]
    public spa_dict* props;
}
