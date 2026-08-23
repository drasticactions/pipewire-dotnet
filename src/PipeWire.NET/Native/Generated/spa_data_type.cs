#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_data_type : uint
{
    SPA_DATA_Invalid,
    SPA_DATA_MemPtr,
    SPA_DATA_MemFd,
    SPA_DATA_DmaBuf,
    SPA_DATA_MemId,
    SPA_DATA_SyncObj,
    _SPA_DATA_LAST,
}
