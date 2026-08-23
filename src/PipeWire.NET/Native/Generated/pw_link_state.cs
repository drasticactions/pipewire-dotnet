#pragma warning disable CS1591

namespace PipeWire.Native;

public enum pw_link_state
{
    PW_LINK_STATE_ERROR = -2,
    PW_LINK_STATE_UNLINKED = -1,
    PW_LINK_STATE_INIT = 0,
    PW_LINK_STATE_NEGOTIATING = 1,
    PW_LINK_STATE_ALLOCATING = 2,
    PW_LINK_STATE_PAUSED = 3,
    PW_LINK_STATE_ACTIVE = 4,
}
