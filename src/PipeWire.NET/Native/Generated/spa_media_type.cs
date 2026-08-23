#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_media_type : uint
{
    SPA_MEDIA_TYPE_unknown,
    SPA_MEDIA_TYPE_audio,
    SPA_MEDIA_TYPE_video,
    SPA_MEDIA_TYPE_image,
    SPA_MEDIA_TYPE_binary,
    SPA_MEDIA_TYPE_stream,
    SPA_MEDIA_TYPE_application,
}
