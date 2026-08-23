#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_rectangle
{
    [NativeTypeName("uint32_t")]
    public uint width;

    [NativeTypeName("uint32_t")]
    public uint height;
}
