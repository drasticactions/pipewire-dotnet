#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_meta_bitmap
{
    [NativeTypeName("uint32_t")]
    public uint format;

    [NativeTypeName("struct spa_rectangle")]
    public spa_rectangle size;

    [NativeTypeName("int32_t")]
    public int stride;

    [NativeTypeName("uint32_t")]
    public uint offset;
}
