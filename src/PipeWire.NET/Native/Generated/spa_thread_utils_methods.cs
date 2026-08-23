#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_thread_utils_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("struct spa_thread *(*)(void *, const struct spa_dict *, void *(*)(void *), void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_dict*, delegate* unmanaged[Cdecl]<void*, void*>, void*, spa_thread*> create;

    [NativeTypeName("int (*)(void *, struct spa_thread *, void **)")]
    public delegate* unmanaged[Cdecl]<void*, spa_thread*, void**, int> join;

    [NativeTypeName("int (*)(void *, const struct spa_dict *, int *, int *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_dict*, int*, int*, int> get_rt_range;

    [NativeTypeName("int (*)(void *, struct spa_thread *, int)")]
    public delegate* unmanaged[Cdecl]<void*, spa_thread*, int, int> acquire_rt;

    [NativeTypeName("int (*)(void *, struct spa_thread *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_thread*, int> drop_rt;
}
