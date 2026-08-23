#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_loop_control_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, int> get_fd;

    [NativeTypeName("void (*)(void *, struct spa_hook *, const struct spa_loop_control_hooks *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_hook*, spa_loop_control_hooks*, void*, void> add_hook;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> enter;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> leave;

    [NativeTypeName("int (*)(void *, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, int> iterate;

    [NativeTypeName("int (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, int> check;

    [NativeTypeName("int (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, int> @lock;

    [NativeTypeName("int (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, int> unlock;

    [NativeTypeName("int (*)(void *, struct timespec *, int64_t)")]
    public delegate* unmanaged[Cdecl]<void*, timespec*, long, int> get_time;

    [NativeTypeName("int (*)(void *, const struct timespec *)")]
    public delegate* unmanaged[Cdecl]<void*, timespec*, int> wait;

    [NativeTypeName("int (*)(void *, _Bool)")]
    public delegate* unmanaged[Cdecl]<void*, byte, int> signal;

    [NativeTypeName("int (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, int> accept;
}
