#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_dict_item
{
    [NativeTypeName("const char *")]
    public sbyte* key;

    [NativeTypeName("const char *")]
    public sbyte* value;
}
