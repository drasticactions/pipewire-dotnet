#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_export_type
{
    [NativeTypeName("struct spa_list")]
    public spa_list link;

    [NativeTypeName("const char *")]
    public sbyte* type;

    [NativeTypeName("struct pw_proxy *(*)(struct pw_core *, const char *, const struct spa_dict *, void *, size_t)")]
    public delegate* unmanaged[Cdecl]<pw_core*, sbyte*, spa_dict*, void*, nuint, pw_proxy*> func;
}
