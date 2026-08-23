#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct pw_properties
{
    [NativeTypeName("struct spa_dict")]
    public spa_dict dict;

    [NativeTypeName("uint32_t")]
    public uint flags;
}
