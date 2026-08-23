#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_prop_info : uint
{
    SPA_PROP_INFO_START,
    SPA_PROP_INFO_id,
    SPA_PROP_INFO_name,
    SPA_PROP_INFO_type,
    SPA_PROP_INFO_labels,
    SPA_PROP_INFO_container,
    SPA_PROP_INFO_params,
    SPA_PROP_INFO_description,
}
