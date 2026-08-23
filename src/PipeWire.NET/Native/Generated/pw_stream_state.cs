#pragma warning disable CS1591

namespace PipeWire.Native;

public enum pw_stream_state
{
    PW_STREAM_STATE_ERROR = -1,
    PW_STREAM_STATE_UNCONNECTED = 0,
    PW_STREAM_STATE_CONNECTING = 1,
    PW_STREAM_STATE_PAUSED = 2,
    PW_STREAM_STATE_STREAMING = 3,
}
