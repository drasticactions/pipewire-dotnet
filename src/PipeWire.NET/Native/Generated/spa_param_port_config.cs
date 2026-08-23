#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_port_config : uint
{
    SPA_PARAM_PORT_CONFIG_START,
    SPA_PARAM_PORT_CONFIG_direction,
    SPA_PARAM_PORT_CONFIG_mode,
    SPA_PARAM_PORT_CONFIG_monitor,
    SPA_PARAM_PORT_CONFIG_control,
    SPA_PARAM_PORT_CONFIG_format,
}
