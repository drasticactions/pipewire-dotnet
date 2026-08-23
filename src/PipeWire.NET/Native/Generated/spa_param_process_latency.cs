#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_process_latency : uint
{
    SPA_PARAM_PROCESS_LATENCY_START,
    SPA_PARAM_PROCESS_LATENCY_quantum,
    SPA_PARAM_PROCESS_LATENCY_rate,
    SPA_PARAM_PROCESS_LATENCY_ns,
}
