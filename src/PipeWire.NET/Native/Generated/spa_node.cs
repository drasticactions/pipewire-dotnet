#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_node
{
    [NativeTypeName("struct spa_interface")]
    public spa_interface iface;
}
