#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public unsafe partial struct pw_timer
{
    [NativeTypeName("struct spa_list")]
    public spa_list link;

    [NativeTypeName("struct pw_timer_queue *")]
    public pw_timer_queue* queue;

    [NativeTypeName("struct timespec")]
    public timespec timeout;

    [NativeTypeName("pw_timer_callback")]
    public delegate* unmanaged[Cdecl]<void*, void> callback;

    public void* data;

    [NativeTypeName("uint32_t[16]")]
    public _padding_e__FixedBuffer padding;

    [InlineArray(16)]
    public partial struct _padding_e__FixedBuffer
    {
        public uint e0;
    }
}
