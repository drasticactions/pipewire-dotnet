#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_video_info_mjpg
{
    [NativeTypeName("struct spa_rectangle")]
    public spa_rectangle size;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction framerate;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction max_framerate;
}
