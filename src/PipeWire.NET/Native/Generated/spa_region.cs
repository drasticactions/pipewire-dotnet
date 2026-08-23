#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_region
{
    [NativeTypeName("struct spa_point")]
    public spa_point position;

    [NativeTypeName("struct spa_rectangle")]
    public spa_rectangle size;
}
