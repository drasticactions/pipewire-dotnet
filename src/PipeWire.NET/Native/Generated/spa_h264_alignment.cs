#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_h264_alignment : uint
{
    SPA_H264_ALIGNMENT_UNKNOWN = 0,
    SPA_H264_ALIGNMENT_AU,
    SPA_H264_ALIGNMENT_NAL,
}
