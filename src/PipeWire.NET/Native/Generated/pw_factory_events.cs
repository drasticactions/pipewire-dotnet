#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_factory_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *, const struct pw_factory_info *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_factory_info*, void> info;
}
