#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_log_topic
{
    [NativeTypeName("uint32_t")]
    public uint version;

    [NativeTypeName("const char *")]
    public sbyte* topic;

    [NativeTypeName("enum spa_log_level")]
    public spa_log_level level;

    [NativeTypeName("_Bool")]
    public byte has_custom_level;
}
