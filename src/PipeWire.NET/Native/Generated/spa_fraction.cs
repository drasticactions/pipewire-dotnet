#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_fraction
{
    [NativeTypeName("uint32_t")]
    public uint num;

    [NativeTypeName("uint32_t")]
    public uint denom;
}
