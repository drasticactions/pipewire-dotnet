#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct pw_map_range
{
    [NativeTypeName("uint32_t")]
    public uint start;

    [NativeTypeName("uint32_t")]
    public uint offset;

    [NativeTypeName("uint32_t")]
    public uint size;
}
