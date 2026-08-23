#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_tag_info
{
    [NativeTypeName("enum spa_direction")]
    public spa_direction direction;

    [NativeTypeName("const struct spa_pod *")]
    public spa_pod* info;
}
