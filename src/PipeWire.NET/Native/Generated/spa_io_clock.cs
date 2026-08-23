#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public partial struct spa_io_clock
{
    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("char[64]")]
    public _name_e__FixedBuffer name;

    [NativeTypeName("uint64_t")]
    public ulong nsec;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction rate;

    [NativeTypeName("uint64_t")]
    public ulong position;

    [NativeTypeName("uint64_t")]
    public ulong duration;

    [NativeTypeName("int64_t")]
    public long delay;

    public double rate_diff;

    [NativeTypeName("uint64_t")]
    public ulong next_nsec;

    [NativeTypeName("struct spa_fraction")]
    public spa_fraction target_rate;

    [NativeTypeName("uint64_t")]
    public ulong target_duration;

    [NativeTypeName("uint32_t")]
    public uint target_seq;

    [NativeTypeName("uint32_t")]
    public uint cycle;

    [NativeTypeName("uint64_t")]
    public ulong xrun;

    [InlineArray(64)]
    public partial struct _name_e__FixedBuffer
    {
        public sbyte e0;
    }
}
