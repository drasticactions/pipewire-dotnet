#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_video_color_range : uint
{
    SPA_VIDEO_COLOR_RANGE_UNKNOWN = 0,
    SPA_VIDEO_COLOR_RANGE_0_255,
    SPA_VIDEO_COLOR_RANGE_16_235,
}
