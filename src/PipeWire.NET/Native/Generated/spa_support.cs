#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_support
{
    [NativeTypeName("const char *")]
    public sbyte* type;

    public void* data;
}
