#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_direction : uint
{
    SPA_DIRECTION_INPUT = 0,
    SPA_DIRECTION_OUTPUT = 1,
}
