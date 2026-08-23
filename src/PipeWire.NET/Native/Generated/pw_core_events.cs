#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_core_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *, const struct pw_core_info *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_core_info*, void> info;

    [NativeTypeName("void (*)(void *, uint32_t, int)")]
    public delegate* unmanaged[Cdecl]<void*, uint, int, void> done;

    [NativeTypeName("void (*)(void *, uint32_t, int)")]
    public delegate* unmanaged[Cdecl]<void*, uint, int, void> ping;

    [NativeTypeName("void (*)(void *, uint32_t, int, int, const char *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, int, int, sbyte*, void> error;

    [NativeTypeName("void (*)(void *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, void> remove_id;

    [NativeTypeName("void (*)(void *, uint32_t, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, uint, void> bound_id;

    [NativeTypeName("void (*)(void *, uint32_t, uint32_t, int, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, uint, int, uint, void> add_mem;

    [NativeTypeName("void (*)(void *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, uint, void> remove_mem;

    [NativeTypeName("void (*)(void *, uint32_t, uint32_t, const struct spa_dict *)")]
    public delegate* unmanaged[Cdecl]<void*, uint, uint, spa_dict*, void> bound_props;
}
