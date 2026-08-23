#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_profile : uint
{
    SPA_PARAM_PROFILE_START,
    SPA_PARAM_PROFILE_index,
    SPA_PARAM_PROFILE_name,
    SPA_PARAM_PROFILE_description,
    SPA_PARAM_PROFILE_priority,
    SPA_PARAM_PROFILE_available,
    SPA_PARAM_PROFILE_info,
    SPA_PARAM_PROFILE_classes,
    SPA_PARAM_PROFILE_save,
}
