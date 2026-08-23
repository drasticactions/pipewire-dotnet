#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_point
{
    [NativeTypeName("int32_t")]
    public int x;

    [NativeTypeName("int32_t")]
    public int y;
}
