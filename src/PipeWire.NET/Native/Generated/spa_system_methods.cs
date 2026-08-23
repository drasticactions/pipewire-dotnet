#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_system_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("ssize_t (*)(void *, int, void *, size_t)")]
    public delegate* unmanaged[Cdecl]<void*, int, void*, nuint, nint> read;

    [NativeTypeName("ssize_t (*)(void *, int, const void *, size_t)")]
    public delegate* unmanaged[Cdecl]<void*, int, void*, nuint, nint> write;

    [NativeTypeName("int (*)(void *, int, unsigned long, ...)")]
    public delegate* unmanaged[Cdecl]<void*, int, nuint, int> ioctl;

    [NativeTypeName("int (*)(void *, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, int> close;

    [NativeTypeName("int (*)(void *, int, struct timespec *)")]
    public delegate* unmanaged[Cdecl]<void*, int, timespec*, int> clock_gettime;

    [NativeTypeName("int (*)(void *, int, struct timespec *)")]
    public delegate* unmanaged[Cdecl]<void*, int, timespec*, int> clock_getres;

    [NativeTypeName("int (*)(void *, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, int> pollfd_create;

    [NativeTypeName("int (*)(void *, int, int, uint32_t, void *)")]
    public delegate* unmanaged[Cdecl]<void*, int, int, uint, void*, int> pollfd_add;

    [NativeTypeName("int (*)(void *, int, int, uint32_t, void *)")]
    public delegate* unmanaged[Cdecl]<void*, int, int, uint, void*, int> pollfd_mod;

    [NativeTypeName("int (*)(void *, int, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, int, int> pollfd_del;

    [NativeTypeName("int (*)(void *, int, struct spa_poll_event *, int, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, spa_poll_event*, int, int, int> pollfd_wait;

    [NativeTypeName("int (*)(void *, int, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, int, int> timerfd_create;

    [NativeTypeName("int (*)(void *, int, int, const struct itimerspec *, struct itimerspec *)")]
    public delegate* unmanaged[Cdecl]<void*, int, int, itimerspec*, itimerspec*, int> timerfd_settime;

    [NativeTypeName("int (*)(void *, int, struct itimerspec *)")]
    public delegate* unmanaged[Cdecl]<void*, int, itimerspec*, int> timerfd_gettime;

    [NativeTypeName("int (*)(void *, int, uint64_t *)")]
    public delegate* unmanaged[Cdecl]<void*, int, ulong*, int> timerfd_read;

    [NativeTypeName("int (*)(void *, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, int> eventfd_create;

    [NativeTypeName("int (*)(void *, int, uint64_t)")]
    public delegate* unmanaged[Cdecl]<void*, int, ulong, int> eventfd_write;

    [NativeTypeName("int (*)(void *, int, uint64_t *)")]
    public delegate* unmanaged[Cdecl]<void*, int, ulong*, int> eventfd_read;

    [NativeTypeName("int (*)(void *, int, int)")]
    public delegate* unmanaged[Cdecl]<void*, int, int, int> signalfd_create;

    [NativeTypeName("int (*)(void *, int, int *)")]
    public delegate* unmanaged[Cdecl]<void*, int, int*, int> signalfd_read;
}
