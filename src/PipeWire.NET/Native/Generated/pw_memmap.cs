#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public unsafe partial struct pw_memmap
{
    [NativeTypeName("struct pw_memblock *")]
    public pw_memblock* block;

    public void* ptr;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint offset;

    [NativeTypeName("uint32_t")]
    public uint size;

    [NativeTypeName("uint32_t[5]")]
    public _tag_e__FixedBuffer tag;

    [InlineArray(5)]
    public partial struct _tag_e__FixedBuffer
    {
        public uint e0;
    }
}
