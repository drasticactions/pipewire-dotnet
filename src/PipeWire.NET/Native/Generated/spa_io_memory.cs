#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_io_memory
{
    [NativeTypeName("int32_t")]
    public int status;

    [NativeTypeName("uint32_t")]
    public uint size;

    public void* data;
}
