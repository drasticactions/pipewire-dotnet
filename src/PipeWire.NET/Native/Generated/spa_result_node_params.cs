#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_result_node_params
{
    [NativeTypeName("uint32_t")]
    public uint id;

    [NativeTypeName("uint32_t")]
    public uint index;

    [NativeTypeName("uint32_t")]
    public uint next;

    [NativeTypeName("struct spa_pod *")]
    public spa_pod* param;
}
