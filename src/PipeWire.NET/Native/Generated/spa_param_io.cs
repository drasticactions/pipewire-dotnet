#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_io : uint
{
    SPA_PARAM_IO_START,
    SPA_PARAM_IO_id,
    SPA_PARAM_IO_size,
}
