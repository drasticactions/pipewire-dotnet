#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_poll_event
{
    [NativeTypeName("uint32_t")]
    public uint events;

    public void* data;
}
