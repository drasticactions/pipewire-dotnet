#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_registry_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *, uint32_t, uint32_t, const char *, uint32_t, const struct spa_dict *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, uint, sbyte*, uint, spa_dict*, void> global;

    [NativeTypeName("void (*)(void *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, void> global_remove;
}
