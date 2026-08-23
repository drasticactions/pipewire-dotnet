#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public partial struct spa_audio_layout_info
{
    [NativeTypeName("uint32_t")]
    public uint n_channels;

    [NativeTypeName("uint32_t[64]")]
    public _position_e__FixedBuffer position;

    [InlineArray(64)]
    public partial struct _position_e__FixedBuffer
    {
        public uint e0;
    }
}
