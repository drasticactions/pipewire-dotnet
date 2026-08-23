#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_event_node : uint
{
    SPA_EVENT_NODE_START,
    SPA_EVENT_NODE_START_User = 0x1000,
    SPA_EVENT_NODE_extra,
    SPA_EVENT_NODE_START_CUSTOM = 0x1000000,
}
