#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_module_info
{
    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("const char *")]
    public sbyte* name;

    [NativeTypeName("const char *")]
    public sbyte* filename;

    [NativeTypeName("const char *")]
    public sbyte* args;

    [NativeTypeName("uint64_t")]
    public ulong change_mask;

    [NativeTypeName("struct spa_dict *")]
    public spa_dict* props;
}
