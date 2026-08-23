#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_meta_videotransform_value : uint
{
    SPA_META_TRANSFORMATION_None = 0,
    SPA_META_TRANSFORMATION_90,
    SPA_META_TRANSFORMATION_180,
    SPA_META_TRANSFORMATION_270,
    SPA_META_TRANSFORMATION_Flipped,
    SPA_META_TRANSFORMATION_Flipped90,
    SPA_META_TRANSFORMATION_Flipped180,
    SPA_META_TRANSFORMATION_Flipped270,
}
