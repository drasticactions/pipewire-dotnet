#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_core_info
{
    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("uint32_t")]
    public uint cookie;

    [NativeTypeName("const char *")]
    public sbyte* user_name;

    [NativeTypeName("const char *")]
    public sbyte* host_name;

    [NativeTypeName("const char *")]
    public sbyte* version;

    [NativeTypeName("const char *")]
    public sbyte* name;

    [NativeTypeName("uint64_t")]
    public ulong change_mask;

    [NativeTypeName("struct spa_dict *")]
    public spa_dict* props;
}
