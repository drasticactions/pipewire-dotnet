#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_pod_pointer_body
{
    [NativeTypeName("uint32_t")]
    public uint type;

    [NativeTypeName("uint32_t")]
    public uint _padding;

    [NativeTypeName("const void *")]
    public void* value;
}
