#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_video_flags : uint
{
    SPA_VIDEO_FLAG_NONE = 0,
    SPA_VIDEO_FLAG_VARIABLE_FPS = (1 << 0),
    SPA_VIDEO_FLAG_PREMULTIPLIED_ALPHA = (1 << 1),
    SPA_VIDEO_FLAG_MODIFIER = (1 << 2),
    SPA_VIDEO_FLAG_MODIFIER_FIXATION_REQUIRED = (1 << 3),
}
