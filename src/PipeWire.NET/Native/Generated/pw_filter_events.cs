#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_filter_events
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> destroy;

    [NativeTypeName("void (*)(void *, enum pw_filter_state, enum pw_filter_state, const char *)")]
    public delegate* unmanaged[Cdecl]<void*, pw_filter_state, pw_filter_state, sbyte*, void> state_changed;

    [NativeTypeName("void (*)(void *, void *, uint32_t, void *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, void*, uint, void*, uint, void> io_changed;

    [NativeTypeName("void (*)(void *, void *, uint32_t, const struct spa_pod *)")]
    public delegate* unmanaged[Cdecl]<void*, void*, uint, spa_pod*, void> param_changed;

    [NativeTypeName("void (*)(void *, void *, struct pw_buffer *)")]
    public delegate* unmanaged[Cdecl]<void*, void*, pw_buffer*, void> add_buffer;

    [NativeTypeName("void (*)(void *, void *, struct pw_buffer *)")]
    public delegate* unmanaged[Cdecl]<void*, void*, pw_buffer*, void> remove_buffer;

    [NativeTypeName("void (*)(void *, struct spa_io_position *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_io_position*, void> process;

    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> drained;

    [NativeTypeName("void (*)(void *, const struct spa_command *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_command*, void> command;
}
