#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public partial struct spa_io_video_size
{
    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint stride;

    [NativeTypeName("struct spa_rectangle")]
    public spa_rectangle size;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction framerate;

    [NativeTypeName("uint32_t[4]")]
    public _padding_e__FixedBuffer padding;

    [InlineArray(4)]
    public partial struct _padding_e__FixedBuffer
    {
        public uint e0;
    }
}
