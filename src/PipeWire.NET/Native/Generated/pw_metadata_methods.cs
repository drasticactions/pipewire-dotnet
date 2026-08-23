#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_metadata_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *, struct spa_hook *, const struct pw_metadata_events *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_hook*, pw_metadata_events*, void*, int> add_listener;

    [NativeTypeName("int (*)(void *, uint32_t, const char *, const char *, const char *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, sbyte*, sbyte*, sbyte*, int> set_property;

    [NativeTypeName("int (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, int> clear;
}
