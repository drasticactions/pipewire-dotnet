#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public partial struct spa_io_segment_bar
{
    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint offset;

    public float signature_num;

    public float signature_denom;

    public double bpm;

    public double beat;

    public double bar_start_tick;

    public double ticks_per_beat;

    [NativeTypeName("uint32_t[4]")]
    public _padding_e__FixedBuffer padding;

    [InlineArray(4)]
    public partial struct _padding_e__FixedBuffer
    {
        public uint e0;
    }
}
