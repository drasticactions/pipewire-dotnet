#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_node_event : uint
{
    SPA_NODE_EVENT_Error,
    SPA_NODE_EVENT_Buffering,
    SPA_NODE_EVENT_RequestRefresh,
    SPA_NODE_EVENT_RequestProcess,
    SPA_NODE_EVENT_User,
}
