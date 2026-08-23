#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_interface_info
{
    [NativeTypeName("const char *")]
    public sbyte* type;
}
