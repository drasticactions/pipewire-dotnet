#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_interface
{
    [NativeTypeName("const char *")]
    public sbyte* type;

    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("struct spa_callbacks")]
    public spa_callbacks cb;
}
