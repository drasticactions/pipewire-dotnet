#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_link_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *, struct spa_hook *, const struct pw_link_events *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_hook*, pw_link_events*, void*, int> add_listener;
}
