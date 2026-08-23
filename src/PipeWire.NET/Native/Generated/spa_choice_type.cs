#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_choice_type : uint
{
    SPA_CHOICE_None,
    SPA_CHOICE_Range,
    SPA_CHOICE_Step,
    SPA_CHOICE_Enum,
    SPA_CHOICE_Flags,
}
