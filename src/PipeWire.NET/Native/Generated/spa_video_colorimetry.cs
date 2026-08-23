#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_video_colorimetry
{
    [NativeTypeName("enum spa_video_color_range")]
    public spa_video_color_range range;

    [NativeTypeName("enum spa_video_color_matrix")]
    public spa_video_color_matrix matrix;

    [NativeTypeName("enum spa_video_transfer_function")]
    public spa_video_transfer_function transfer;

    [NativeTypeName("enum spa_video_color_primaries")]
    public spa_video_color_primaries primaries;
}
