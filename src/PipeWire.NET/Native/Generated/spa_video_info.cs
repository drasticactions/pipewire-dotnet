#pragma warning disable CS1591

using System.Runtime.InteropServices;

namespace PipeWire.Native;

public partial struct spa_video_info
{
    [NativeTypeName("uint32_t")]
    public uint media_type;

    [NativeTypeName("uint32_t")]
    public uint media_subtype;

    [NativeTypeName("__AnonymousRecord_format_L25_C2")]
    public _info_e__Union info;

    [StructLayout(LayoutKind.Explicit)]
    public partial struct _info_e__Union
    {
        [FieldOffset(0)]
        [NativeTypeName("struct spa_video_info_raw")]
        public spa_video_info_raw raw;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_video_info_dsp")]
        public spa_video_info_dsp dsp;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_video_info_h264")]
        public spa_video_info_h264 h264;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_video_info_mjpg")]
        public spa_video_info_mjpg mjpg;
    }
}
