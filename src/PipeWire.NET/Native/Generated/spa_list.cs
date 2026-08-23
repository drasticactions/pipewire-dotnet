#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_list
{
    [NativeTypeName("struct spa_list *")]
    public spa_list* next;

    [NativeTypeName("struct spa_list *")]
    public spa_list* prev;
}
