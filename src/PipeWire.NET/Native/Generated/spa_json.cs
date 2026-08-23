#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_json
{
    [NativeTypeName("const char *")]
    public sbyte* cur;

    [NativeTypeName("const char *")]
    public sbyte* end;

    [NativeTypeName("struct spa_json *")]
    public spa_json* parent;

    [NativeTypeName("uint32_t")]
    public uint state;

    [NativeTypeName("uint32_t")]
    public uint depth;
}
