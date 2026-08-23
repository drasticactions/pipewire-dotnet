#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_port_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *, const struct pw_port_info *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_port_info*, void> info;

    [NativeTypeName("void (*)(void *, int, uint32_t, uint32_t, uint32_t, const struct spa_pod *)")]
    public delegate* unmanaged[Cdecl]<void*, int, uint, uint, uint, spa_pod*, void> param;
}
