#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public partial struct spa_param_info
{
    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint user;

    [NativeTypeName("int32_t")]
    public int seq;

    [NativeTypeName("uint32_t[4]")]
    public _padding_e__FixedBuffer padding;

    [InlineArray(4)]
    public partial struct _padding_e__FixedBuffer
    {
        public uint e0;
    }
}
