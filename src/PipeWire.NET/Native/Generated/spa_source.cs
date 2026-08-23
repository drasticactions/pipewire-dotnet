#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_source
{
    [NativeTypeName("struct spa_loop *")]
    public spa_loop* loop;

    [NativeTypeName("spa_source_func_t")]
    public delegate* unmanaged[Cdecl]<spa_source*, void> func;

    public void* data;

    public int fd;

    [NativeTypeName("uint32_t")]
    public uint mask;

    [NativeTypeName("uint32_t")]
    public uint rmask;

    public void* priv;
}
