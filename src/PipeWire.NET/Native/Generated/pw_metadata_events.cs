#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_metadata_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *, uint32_t, const char *, const char *, const char *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, sbyte*, sbyte*, sbyte*, int> property;
}
