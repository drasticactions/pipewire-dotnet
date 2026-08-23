#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_node_command : uint
{
    SPA_NODE_COMMAND_Suspend,
    SPA_NODE_COMMAND_Pause,
    SPA_NODE_COMMAND_Start,
    SPA_NODE_COMMAND_Enable,
    SPA_NODE_COMMAND_Disable,
    SPA_NODE_COMMAND_Flush,
    SPA_NODE_COMMAND_Drain,
    SPA_NODE_COMMAND_Marker,
    SPA_NODE_COMMAND_ParamBegin,
    SPA_NODE_COMMAND_ParamEnd,
    SPA_NODE_COMMAND_RequestProcess,
    SPA_NODE_COMMAND_User,
}
