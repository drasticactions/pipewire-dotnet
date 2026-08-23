#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_pod_sequence_body
{
    [NativeTypeName("uint32_t")]
    public uint unit;

    [NativeTypeName("uint32_t")]
    public uint pad;
}
