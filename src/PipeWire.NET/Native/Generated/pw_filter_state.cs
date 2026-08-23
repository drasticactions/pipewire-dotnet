#pragma warning disable CS1591

namespace PipeWire.Native;

public enum pw_filter_state
{
    PW_FILTER_STATE_ERROR = -1,
    PW_FILTER_STATE_UNCONNECTED = 0,
    PW_FILTER_STATE_CONNECTING = 1,
    PW_FILTER_STATE_PAUSED = 2,
    PW_FILTER_STATE_STREAMING = 3,
}
