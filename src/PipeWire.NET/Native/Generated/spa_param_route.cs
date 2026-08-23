#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_route : uint
{
    SPA_PARAM_ROUTE_START,
    SPA_PARAM_ROUTE_index,
    SPA_PARAM_ROUTE_direction,
    SPA_PARAM_ROUTE_device,
    SPA_PARAM_ROUTE_name,
    SPA_PARAM_ROUTE_description,
    SPA_PARAM_ROUTE_priority,
    SPA_PARAM_ROUTE_available,
    SPA_PARAM_ROUTE_info,
    SPA_PARAM_ROUTE_profiles,
    SPA_PARAM_ROUTE_props,
    SPA_PARAM_ROUTE_devices,
    SPA_PARAM_ROUTE_profile,
    SPA_PARAM_ROUTE_save,
}
