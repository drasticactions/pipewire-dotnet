#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_array
{
    public void* data;

    [NativeTypeName("size_t")]
    public nuint size;

    [NativeTypeName("size_t")]
    public nuint alloc;

    [NativeTypeName("size_t")]
    public nuint extend;
}
