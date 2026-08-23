#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_link_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *, const struct pw_link_info *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_link_info*, void> info;
}
