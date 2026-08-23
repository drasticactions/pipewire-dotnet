#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_video_info_raw
{
    [NativeTypeName("enum spa_video_format")]
    public spa_video_format format;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint64_t")]
    public ulong modifier;

    [NativeTypeName("struct spa_rectangle")]
    public spa_rectangle size;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction framerate;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction max_framerate;

    [NativeTypeName("uint32_t")]
    public uint views;

    [NativeTypeName("enum spa_video_interlace_mode")]
    public spa_video_interlace_mode interlace_mode;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction pixel_aspect_ratio;

    [NativeTypeName("enum spa_video_multiview_mode")]
    public spa_video_multiview_mode multiview_mode;

    [NativeTypeName("enum spa_video_multiview_flags")]
    public spa_video_multiview_flags multiview_flags;

    [NativeTypeName("enum spa_video_chroma_site")]
    public spa_video_chroma_site chroma_site;

    [NativeTypeName("enum spa_video_color_range")]
    public spa_video_color_range color_range;

    [NativeTypeName("enum spa_video_color_matrix")]
    public spa_video_color_matrix color_matrix;

    [NativeTypeName("enum spa_video_transfer_function")]
    public spa_video_transfer_function transfer_function;

    [NativeTypeName("enum spa_video_color_primaries")]
    public spa_video_color_primaries color_primaries;
}
