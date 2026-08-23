#pragma warning disable CS1591

using System.Runtime.InteropServices;

namespace PipeWire.Native;

public partial struct spa_audio_info
{
    [NativeTypeName("uint32_t")]
    public uint media_type;

    [NativeTypeName("uint32_t")]
    public uint media_subtype;

    [NativeTypeName("__AnonymousRecord_format_L41_C2")]
    public _info_e__Union info;

    [StructLayout(LayoutKind.Explicit)]
    public partial struct _info_e__Union
    {
        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_raw")]
        public spa_audio_info_raw raw;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_dsp")]
        public spa_audio_info_dsp dsp;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_iec958")]
        public spa_audio_info_iec958 iec958;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_dsd")]
        public spa_audio_info_dsd dsd;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_mp3")]
        public spa_audio_info_mp3 mp3;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_aac")]
        public spa_audio_info_aac aac;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_vorbis")]
        public spa_audio_info_vorbis vorbis;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_wma")]
        public spa_audio_info_wma wma;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_ra")]
        public spa_audio_info_ra ra;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_amr")]
        public spa_audio_info_amr amr;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_alac")]
        public spa_audio_info_alac alac;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_flac")]
        public spa_audio_info_flac flac;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_ape")]
        public spa_audio_info_ape ape;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_ape")]
        public spa_audio_info_ape opus;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_ac3")]
        public spa_audio_info_ac3 ac3;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_eac3")]
        public spa_audio_info_eac3 eac3;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_truehd")]
        public spa_audio_info_truehd truehd;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_dts")]
        public spa_audio_info_dts dts;

        [FieldOffset(0)]
        [NativeTypeName("struct spa_audio_info_mpegh")]
        public spa_audio_info_mpegh mpegh;
    }
}
