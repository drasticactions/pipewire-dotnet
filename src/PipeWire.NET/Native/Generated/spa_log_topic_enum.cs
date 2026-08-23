#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_log_topic_enum
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("struct spa_log_topic *const *const")]
    public spa_log_topic** topics;

    [NativeTypeName("struct spa_log_topic *const *const")]
    public spa_log_topic** topics_end;
}
