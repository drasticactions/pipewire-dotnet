#pragma warning disable CS1591

namespace PipeWire.Native;

public partial struct spa_command
{
    [NativeTypeName("struct spa_pod")]
    public spa_pod pod;

    [NativeTypeName("struct spa_command_body")]
    public spa_command_body body;
}
