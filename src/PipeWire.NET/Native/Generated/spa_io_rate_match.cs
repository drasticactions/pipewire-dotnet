#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public partial struct spa_io_rate_match
{
    [NativeTypeName("uint32_t")]
    public uint delay;

    [NativeTypeName("uint32_t")]
    public uint size;

    public double rate;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("int32_t")]
    public int delay_frac;

    [NativeTypeName("uint32_t[6]")]
    public _padding_e__FixedBuffer padding;

    [InlineArray(6)]
    public partial struct _padding_e__FixedBuffer
    {
        public uint e0;
    }
}
