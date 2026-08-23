#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_video_color_matrix : uint
{
    SPA_VIDEO_COLOR_MATRIX_UNKNOWN = 0,
    SPA_VIDEO_COLOR_MATRIX_RGB,
    SPA_VIDEO_COLOR_MATRIX_FCC,
    SPA_VIDEO_COLOR_MATRIX_BT709,
    SPA_VIDEO_COLOR_MATRIX_BT601,
    SPA_VIDEO_COLOR_MATRIX_SMPTE240M,
    SPA_VIDEO_COLOR_MATRIX_BT2020,
}
