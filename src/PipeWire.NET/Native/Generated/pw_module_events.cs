#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_module_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *, const struct pw_module_info *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_module_info*, void> info;
}
