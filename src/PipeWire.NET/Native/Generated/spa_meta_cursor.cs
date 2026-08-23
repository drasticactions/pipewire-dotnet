#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_meta_cursor
{
    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("struct spa_point")]
    public spa_point position;

    [NativeTypeName("struct spa_point")]
    public spa_point hotspot;

    [NativeTypeName("uint32_t")]
    public uint bitmap_offset;
}
