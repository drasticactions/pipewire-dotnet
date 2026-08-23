#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_meta
{
    [NativeTypeName("uint32_t")]
    public uint type;

    [NativeTypeName("uint32_t")]
    public uint size;

    public void* data;
}
