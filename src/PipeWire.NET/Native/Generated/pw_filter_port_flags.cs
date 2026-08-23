#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum pw_filter_port_flags : uint
{
    PW_FILTER_PORT_FLAG_NONE = 0,
    PW_FILTER_PORT_FLAG_MAP_BUFFERS = (1 << 0),
    PW_FILTER_PORT_FLAG_ALLOC_BUFFERS = (1 << 1),
}
