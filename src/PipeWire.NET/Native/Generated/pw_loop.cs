#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_loop
{
    [NativeTypeName("struct spa_system *")]
    public spa_system* system;

    [NativeTypeName("struct spa_loop *")]
    public spa_loop* loop;

    [NativeTypeName("struct spa_loop_control *")]
    public spa_loop_control* control;

    [NativeTypeName("struct spa_loop_utils *")]
    public spa_loop_utils* utils;

    [NativeTypeName("const char *")]
    public sbyte* name;
}
