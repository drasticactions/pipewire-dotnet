#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_loop_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(void *, struct spa_source *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_source*, int> add_source;

    [NativeTypeName("int (*)(void *, struct spa_source *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_source*, int> update_source;

    [NativeTypeName("int (*)(void *, struct spa_source *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_source*, int> remove_source;

    [NativeTypeName("int (*)(void *, spa_invoke_func_t, uint32_t, const void *, size_t, _Bool, void *)")]
    public delegate* unmanaged[Cdecl]<void*, delegate* unmanaged[Cdecl]<spa_loop*, byte, uint, void*, nuint, void*, int>, uint, void*, nuint, byte, void*, int> invoke;

    [NativeTypeName("int (*)(void *, spa_invoke_func_t, uint32_t, const void *, size_t, void *)")]
    public delegate* unmanaged[Cdecl]<void*, delegate* unmanaged[Cdecl]<spa_loop*, byte, uint, void*, nuint, void*, int>, uint, void*, nuint, void*, int> locked;
}
