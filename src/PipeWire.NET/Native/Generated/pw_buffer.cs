#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_buffer
{
    [NativeTypeName("struct spa_buffer *")]
    public spa_buffer* buffer;

    public void* user_data;

    [NativeTypeName("uint64_t")]
    public ulong size;

    [NativeTypeName("uint64_t")]
    public ulong requested;

    [NativeTypeName("uint64_t")]
    public ulong time;
}
