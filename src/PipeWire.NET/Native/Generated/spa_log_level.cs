#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_log_level : uint
{
    SPA_LOG_LEVEL_NONE = 0,
    SPA_LOG_LEVEL_ERROR,
    SPA_LOG_LEVEL_WARN,
    SPA_LOG_LEVEL_INFO,
    SPA_LOG_LEVEL_DEBUG,
    SPA_LOG_LEVEL_TRACE,
}
