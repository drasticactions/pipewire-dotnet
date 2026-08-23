#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_log_methods
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("void (*)(void *, enum spa_log_level, const char *, int, const char *, const char *, ...)")]
    public delegate* unmanaged[Cdecl]<void*, spa_log_level, sbyte*, int, sbyte*, sbyte*, void> log;

    [NativeTypeName("void (*)(void *, enum spa_log_level, const char *, int, const char *, const char *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_log_level, sbyte*, int, sbyte*, sbyte*, void*, void> logv;

    [NativeTypeName("void (*)(void *, enum spa_log_level, const struct spa_log_topic *, const char *, int, const char *, const char *, ...)")]
    public delegate* unmanaged[Cdecl]<void*, spa_log_level, spa_log_topic*, sbyte*, int, sbyte*, sbyte*, void> logt;

    [NativeTypeName("void (*)(void *, enum spa_log_level, const struct spa_log_topic *, const char *, int, const char *, const char *, void *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_log_level, spa_log_topic*, sbyte*, int, sbyte*, sbyte*, void*, void> logtv;

    [NativeTypeName("void (*)(void *, struct spa_log_topic *)")]
    public delegate* unmanaged[Cdecl]<void*, spa_log_topic*, void> topic_init;
}
