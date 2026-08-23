#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_factory_info
{
    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("const char *")]
    public sbyte* name;

    [NativeTypeName("const char *")]
    public sbyte* type;

    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("uint64_t")]
    public ulong change_mask;

    [NativeTypeName("struct spa_dict *")]
    public spa_dict* props;
}
