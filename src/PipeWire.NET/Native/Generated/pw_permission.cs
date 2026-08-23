#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct pw_permission
{
    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("uint32_t")]
    public uint permissions;
}
