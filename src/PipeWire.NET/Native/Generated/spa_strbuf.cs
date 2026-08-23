#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_strbuf
{
    [NativeTypeName("char *")]
    public sbyte* buffer;

    [NativeTypeName("size_t")]
    public nuint maxsize;

    [NativeTypeName("size_t")]
    public nuint pos;
}
