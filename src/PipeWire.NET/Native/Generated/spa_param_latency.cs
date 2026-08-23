#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_latency : uint
{
    SPA_PARAM_LATENCY_START,
    SPA_PARAM_LATENCY_direction,
    SPA_PARAM_LATENCY_minQuantum,
    SPA_PARAM_LATENCY_maxQuantum,
    SPA_PARAM_LATENCY_minRate,
    SPA_PARAM_LATENCY_maxRate,
    SPA_PARAM_LATENCY_minNs,
    SPA_PARAM_LATENCY_maxNs,
}
