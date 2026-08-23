#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_stream_control
{
    [NativeTypeName("const char *")]
    public sbyte* name;

    [NativeTypeName("uint32_t")]
    public uint flags;

    public float def;

    public float min;

    public float max;

    public float* values;

    [NativeTypeName("uint32_t")]
    public uint n_values;

    [NativeTypeName("uint32_t")]
    public uint max_values;
}
