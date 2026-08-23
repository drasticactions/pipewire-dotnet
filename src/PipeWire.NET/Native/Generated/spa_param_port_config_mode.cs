#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_port_config_mode : uint
{
    SPA_PARAM_PORT_CONFIG_MODE_none,
    SPA_PARAM_PORT_CONFIG_MODE_passthrough,
    SPA_PARAM_PORT_CONFIG_MODE_convert,
    SPA_PARAM_PORT_CONFIG_MODE_dsp,
}
