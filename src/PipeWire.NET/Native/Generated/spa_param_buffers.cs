#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_buffers : uint
{
    SPA_PARAM_BUFFERS_START,
    SPA_PARAM_BUFFERS_buffers,
    SPA_PARAM_BUFFERS_blocks,
    SPA_PARAM_BUFFERS_size,
    SPA_PARAM_BUFFERS_stride,
    SPA_PARAM_BUFFERS_align,
    SPA_PARAM_BUFFERS_dataType,
    SPA_PARAM_BUFFERS_metaType,
}
