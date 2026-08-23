#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_param_meta : uint
{
    SPA_PARAM_META_START,
    SPA_PARAM_META_type,
    SPA_PARAM_META_size,
    SPA_PARAM_META_features,
}
