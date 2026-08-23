#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_node_callbacks
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, int> ready;

    [NativeTypeName("int (*)(void *, uint32_t, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, uint, int> reuse_buffer;

    [NativeTypeName("int (*)(void *, uint64_t, uint64_t, struct spa_pod *)")]
    public delegate* unmanaged[Cdecl]<void*, ulong, ulong, spa_pod*, int> xrun;
}
