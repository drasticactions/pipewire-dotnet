#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_hook
{
    [NativeTypeName("struct spa_list")]
    public spa_list link;

    [NativeTypeName("struct spa_callbacks")]
    public spa_callbacks cb;

    [NativeTypeName("void (*)(struct spa_hook *)")]
    public delegate* unmanaged[Cdecl]<spa_hook*, void> removed;

    public void* priv;
}
