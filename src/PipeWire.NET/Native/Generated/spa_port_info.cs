#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_port_info
{
    [NativeTypeName("uint64_t")]
    public ulong change_mask;

    [NativeTypeName("uint64_t")]
    public ulong flags;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction rate;

    [NativeTypeName("const struct spa_dict *")]
    public spa_dict* props;

    [NativeTypeName("struct spa_param_info *")]
    public spa_param_info* @params;

    [NativeTypeName("uint32_t")]
    public uint n_params;
}
