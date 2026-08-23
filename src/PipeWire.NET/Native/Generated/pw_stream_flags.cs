#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum pw_stream_flags : uint
{
    PW_STREAM_FLAG_NONE = 0,
    PW_STREAM_FLAG_AUTOCONNECT = (1 << 0),
    PW_STREAM_FLAG_INACTIVE = (1 << 1),
    PW_STREAM_FLAG_MAP_BUFFERS = (1 << 2),
    PW_STREAM_FLAG_DRIVER = (1 << 3),
    PW_STREAM_FLAG_RT_PROCESS = (1 << 4),
    PW_STREAM_FLAG_NO_CONVERT = (1 << 5),
    PW_STREAM_FLAG_EXCLUSIVE = (1 << 6),
    PW_STREAM_FLAG_DONT_RECONNECT = (1 << 7),
    PW_STREAM_FLAG_ALLOC_BUFFERS = (1 << 8),
    PW_STREAM_FLAG_TRIGGER = (1 << 9),
    PW_STREAM_FLAG_ASYNC = (1 << 10),
    PW_STREAM_FLAG_EARLY_PROCESS = (1 << 11),
    PW_STREAM_FLAG_RT_TRIGGER_DONE = (1 << 12),
}
