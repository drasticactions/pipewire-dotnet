#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_loop_control_hooks
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> before;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> after;
}
