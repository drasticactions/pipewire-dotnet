#pragma warning disable CS1591

using System.Runtime.CompilerServices;

namespace PipeWire.Native;

public partial struct spa_io_async_buffers
{
    [NativeTypeName("struct spa_io_buffers[2]")]
    public _buffers_e__FixedBuffer buffers;

    [InlineArray(2)]
    public partial struct _buffers_e__FixedBuffer
    {
        public spa_io_buffers e0;
    }
}
