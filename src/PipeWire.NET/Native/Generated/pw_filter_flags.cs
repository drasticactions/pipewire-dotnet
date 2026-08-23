#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum pw_filter_flags : uint
{
    PW_FILTER_FLAG_NONE = 0,
    PW_FILTER_FLAG_INACTIVE = (1 << 0),
    PW_FILTER_FLAG_DRIVER = (1 << 1),
    PW_FILTER_FLAG_RT_PROCESS = (1 << 2),
    PW_FILTER_FLAG_CUSTOM_LATENCY = (1 << 3),
    PW_FILTER_FLAG_TRIGGER = (1 << 4),
    PW_FILTER_FLAG_ASYNC = (1 << 5),
}
