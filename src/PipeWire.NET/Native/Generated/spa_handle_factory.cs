#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_handle_factory
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("const char *")]
    public sbyte* name;

    [NativeTypeName("const struct spa_dict *")]
    public spa_dict* info;

    [NativeTypeName("size_t (*)(const struct spa_handle_factory *, const struct spa_dict *)")]
    public delegate* unmanaged[Cdecl]<spa_handle_factory*, spa_dict*, nuint> get_size;

    [NativeTypeName("int (*)(const struct spa_handle_factory *, struct spa_handle *, const struct spa_dict *, const struct spa_support *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<spa_handle_factory*, spa_handle*, spa_dict*, spa_support*, uint, int> init;

    [NativeTypeName("int (*)(const struct spa_handle_factory *, const struct spa_interface_info **, uint32_t *)")]
    public delegate* unmanaged[Cdecl]<spa_handle_factory*, spa_interface_info**, uint*, int> enum_interface_info;
}
