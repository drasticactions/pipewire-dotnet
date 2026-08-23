#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_error_location
{
    public int line;

    public int col;

    [NativeTypeName("size_t")]
    public nuint len;

    [NativeTypeName("const char *")]
    public sbyte* location;

    [NativeTypeName("const char *")]
    public sbyte* reason;
}
