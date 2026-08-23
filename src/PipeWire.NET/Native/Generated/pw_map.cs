#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct pw_map
{
    [NativeTypeName("struct pw_array")]
    public pw_array items;

    [NativeTypeName("uint32_t")]
    public uint free_list;
}
