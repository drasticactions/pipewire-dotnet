#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_pod_choice_body
{
    [NativeTypeName("uint32_t")]
    public uint type;

    [NativeTypeName("uint32_t")]
    public uint flags;

    [NativeTypeName("struct spa_pod")]
    public spa_pod child;
}
