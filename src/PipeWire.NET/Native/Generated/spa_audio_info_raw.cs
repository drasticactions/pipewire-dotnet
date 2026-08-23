#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public partial struct spa_audio_info_raw
{
    [NativeTypeName("enum spa_audio_format")]
    public spa_audio_format format;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint rate;

    [NativeTypeName("uint32_t")]
    public uint channels;

    [NativeTypeName("uint32_t[64]")]
    public _position_e__FixedBuffer position;

    [InlineArray(64)]
    public partial struct _position_e__FixedBuffer
    {
        public uint e0;
    }
}
