#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_command_node : uint
{
    SPA_COMMAND_NODE_START,
    SPA_COMMAND_NODE_START_User = 0x1000,
    SPA_COMMAND_NODE_extra,
    SPA_COMMAND_NODE_START_CUSTOM = 0x1000000,
}
