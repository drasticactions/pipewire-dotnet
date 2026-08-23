#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum spa_h264_stream_format : uint
{
    SPA_H264_STREAM_FORMAT_UNKNOWN = 0,
    SPA_H264_STREAM_FORMAT_AVC,
    SPA_H264_STREAM_FORMAT_AVC3,
    SPA_H264_STREAM_FORMAT_BYTESTREAM,
}
