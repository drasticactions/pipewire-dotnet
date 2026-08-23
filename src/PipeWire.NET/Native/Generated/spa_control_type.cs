#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_control_type : uint
{
    SPA_CONTROL_Invalid,
    SPA_CONTROL_Properties,
    SPA_CONTROL_Midi,
    SPA_CONTROL_OSC,
    SPA_CONTROL_UMP,
    _SPA_CONTROL_LAST,
}
