#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_log
{
    [NativeTypeName("struct spa_interface")]
    public spa_interface iface;

    [NativeTypeName("enum spa_log_level")]
    public spa_log_level level;
}
