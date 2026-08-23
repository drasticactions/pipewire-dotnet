#pragma warning disable CS1591

namespace PipeWire.Native;

public enum pw_node_state
{
    PW_NODE_STATE_ERROR = -1,
    PW_NODE_STATE_CREATING = 0,
    PW_NODE_STATE_SUSPENDED = 1,
    PW_NODE_STATE_IDLE = 2,
    PW_NODE_STATE_RUNNING = 3,
}
