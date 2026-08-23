#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_io_position_state : uint
{
    SPA_IO_POSITION_STATE_STOPPED,
    SPA_IO_POSITION_STATE_STARTING,
    SPA_IO_POSITION_STATE_RUNNING,
}
