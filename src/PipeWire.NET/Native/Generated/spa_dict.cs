#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_dict
{
    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("uint32_t")]
    public uint n_items;

    [NativeTypeName("const struct spa_dict_item *")]
    public spa_dict_item* items;
}
