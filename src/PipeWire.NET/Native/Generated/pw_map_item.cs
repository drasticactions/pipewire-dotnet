#pragma warning disable CS1591

using System.Runtime.InteropServices;

namespace PipeWire.Native;

[StructLayout(LayoutKind.Explicit)]
public unsafe partial struct pw_map_item
{
    [FieldOffset(0)]
    [NativeTypeName("uintptr_t")]
    public nuint next;

    [FieldOffset(0)]
    public void* data;
}
