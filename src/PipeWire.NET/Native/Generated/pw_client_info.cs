#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_client_info
{
    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("uint64_t")]
    public ulong change_mask;

    [NativeTypeName("struct spa_dict *")]
    public spa_dict* props;
}
