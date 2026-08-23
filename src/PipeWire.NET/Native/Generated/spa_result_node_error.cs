#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_result_node_error
{
    [NativeTypeName("const char *")]
    public sbyte* message;
}
