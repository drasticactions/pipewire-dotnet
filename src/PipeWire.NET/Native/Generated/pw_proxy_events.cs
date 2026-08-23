#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_proxy_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> destroy;

    [NativeTypeName("void (*)(void *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, void> bound;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> removed;

    [NativeTypeName("void (*)(void *, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, void> done;

    [NativeTypeName("void (*)(void *, int, int, const char *)")]
    public delegate* unmanaged[Cdecl]<void*, int, int, sbyte*, void> error;

    [NativeTypeName("void (*)(void *, uint32_t, const struct spa_dict *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, spa_dict*, void> bound_props;
}
