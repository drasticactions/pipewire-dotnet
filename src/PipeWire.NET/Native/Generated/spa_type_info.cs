#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_type_info
{
    [NativeTypeName("uint32_t")]
    public uint type;

    [NativeTypeName("uint32_t")]
    public uint parent;

    [NativeTypeName("const char *")]
    public sbyte* name;

    [NativeTypeName("const struct spa_type_info *")]
    public spa_type_info* values;
}
