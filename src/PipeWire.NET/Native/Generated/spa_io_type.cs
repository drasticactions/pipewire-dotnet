#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_io_type : uint
{
    SPA_IO_Invalid,
    SPA_IO_Buffers,
    SPA_IO_Range,
    SPA_IO_Clock,
    SPA_IO_Latency,
    SPA_IO_Control,
    SPA_IO_Notify,
    SPA_IO_Position,
    SPA_IO_RateMatch,
    SPA_IO_Memory,
    SPA_IO_AsyncBuffers,
}
