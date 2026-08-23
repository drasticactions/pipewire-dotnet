#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_meta_type : uint
{
    SPA_META_Invalid,
    SPA_META_Header,
    SPA_META_VideoCrop,
    SPA_META_VideoDamage,
    SPA_META_Bitmap,
    SPA_META_Cursor,
    SPA_META_Control,
    SPA_META_Busy,
    SPA_META_VideoTransform,
    SPA_META_SyncTimeline,
    _SPA_META_LAST,
    SPA_META_START_custom = 0x200,
    SPA_META_START_features = 0x10000,
}
