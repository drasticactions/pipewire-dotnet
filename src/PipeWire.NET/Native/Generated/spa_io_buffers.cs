#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_io_buffers
{
    [NativeTypeName("int32_t")]
    public int status;

    [NativeTypeName("uint32_t")]
    public uint buffer_id;
}
