#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_callbacks
{
    [NativeTypeName("const void *")]
    public void* funcs;

    public void* data;
}
