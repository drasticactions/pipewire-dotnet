#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_loop_utils_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("struct spa_source *(*)(void *, int, uint32_t, _Bool, spa_source_io_func_t, void *)")]
    public delegate* unmanaged[Cdecl]<void*, int, uint, byte, delegate* unmanaged[Cdecl]<void*, int, uint, void>, void*, spa_source*> add_io;

    [NativeTypeName("int (*)(void *, struct spa_source *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, spa_source*, uint, int> update_io;

    [NativeTypeName("struct spa_source *(*)(void *, _Bool, spa_source_idle_func_t, void *)")]
    public delegate* unmanaged[Cdecl]<void*, byte, delegate* unmanaged[Cdecl]<void*, void>, void*, spa_source*> add_idle;

    [NativeTypeName("int (*)(void *, struct spa_source *, _Bool)")]
    public delegate* unmanaged[Cdecl]<void*, spa_source*, byte, int> enable_idle;

    [NativeTypeName("struct spa_source *(*)(void *, spa_source_event_func_t, void *)")]
    public delegate* unmanaged[Cdecl]<void*, delegate* unmanaged[Cdecl]<void*, ulong, void>, void*, spa_source*> add_event;

    [NativeTypeName("int (*)(void *, struct spa_source *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_source*, int> signal_event;

    [NativeTypeName("struct spa_source *(*)(void *, spa_source_timer_func_t, void *)")]
    public delegate* unmanaged[Cdecl]<void*, delegate* unmanaged[Cdecl]<void*, ulong, void>, void*, spa_source*> add_timer;

    [NativeTypeName("int (*)(void *, struct spa_source *, struct timespec *, struct timespec *, _Bool)")]
    public delegate* unmanaged[Cdecl]<void*, spa_source*, timespec*, timespec*, byte, int> update_timer;

    [NativeTypeName("struct spa_source *(*)(void *, int, spa_source_signal_func_t, void *)")]
    public delegate* unmanaged[Cdecl]<void*, int, delegate* unmanaged[Cdecl]<void*, int, void>, void*, spa_source*> add_signal;

    [NativeTypeName("void (*)(void *, struct spa_source *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_source*, void> destroy_source;
}
