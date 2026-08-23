#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_availability : uint
{
    SPA_PARAM_AVAILABILITY_unknown,
    SPA_PARAM_AVAILABILITY_no,
    SPA_PARAM_AVAILABILITY_yes,
}
