#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_video_info_h264
{
    [NativeTypeName("struct spa_rectangle")]
    public spa_rectangle size;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction framerate;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction max_framerate;

    [NativeTypeName("enum spa_h264_stream_format")]
    public spa_h264_stream_format stream_format;

    [NativeTypeName("enum spa_h264_alignment")]
    public spa_h264_alignment alignment;
}
