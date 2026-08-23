#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_video_interlace_mode : uint
{
    SPA_VIDEO_INTERLACE_MODE_PROGRESSIVE = 0,
    SPA_VIDEO_INTERLACE_MODE_INTERLEAVED,
    SPA_VIDEO_INTERLACE_MODE_MIXED,
    SPA_VIDEO_INTERLACE_MODE_FIELDS,
}
