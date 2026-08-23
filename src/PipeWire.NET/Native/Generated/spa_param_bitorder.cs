#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_bitorder : uint
{
    SPA_PARAM_BITORDER_unknown,
    SPA_PARAM_BITORDER_msb,
    SPA_PARAM_BITORDER_lsb,
}
