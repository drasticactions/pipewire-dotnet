#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum pw_memblock_flags : uint
{
    PW_MEMBLOCK_FLAG_NONE = 0,
    PW_MEMBLOCK_FLAG_READABLE = (1 << 0),
    PW_MEMBLOCK_FLAG_WRITABLE = (1 << 1),
    PW_MEMBLOCK_FLAG_SEAL = (1 << 2),
    PW_MEMBLOCK_FLAG_MAP = (1 << 3),
    PW_MEMBLOCK_FLAG_DONT_CLOSE = (1 << 4),
    PW_MEMBLOCK_FLAG_DONT_NOTIFY = (1 << 5),
    PW_MEMBLOCK_FLAG_UNMAPPABLE = (1 << 6),
    PW_MEMBLOCK_FLAG_READWRITE = PW_MEMBLOCK_FLAG_READABLE | PW_MEMBLOCK_FLAG_WRITABLE,
}
