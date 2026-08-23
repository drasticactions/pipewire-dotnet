#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_client_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *, const struct pw_client_info *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_client_info*, void> info;

    [NativeTypeName("void (*)(void *, uint32_t, uint32_t, const struct pw_permission *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, uint, pw_permission*, void> permissions;
}
