#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_handle
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("int (*)(struct spa_handle *, const char *, void **)")]
    public delegate* unmanaged[Cdecl]<spa_handle*, sbyte*, void**, int> get_interface;

    [NativeTypeName("int (*)(struct spa_handle *)")]
    public delegate* unmanaged[Cdecl]<spa_handle*, int> clear;
}
