#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_node_info
{
    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("uint32_t")]
    public uint max_input_ports;

    [NativeTypeName("uint32_t")]
    public uint max_output_ports;

    [NativeTypeName("uint64_t")]
    public ulong change_mask;

    [NativeTypeName("uint32_t")]
    public uint n_input_ports;

    [NativeTypeName("uint32_t")]
    public uint n_output_ports;

    [NativeTypeName("enum pw_node_state")]
    public pw_node_state state;

    [NativeTypeName("const char *")]
    public sbyte* error;

    [NativeTypeName("struct spa_dict *")]
    public spa_dict* props;

    [NativeTypeName("struct spa_param_info *")]
    public spa_param_info* @params;

    [NativeTypeName("uint32_t")]
    public uint n_params;
}
