#pragma warning disable CS1591

namespace PipeWire.Native;

[NativeTypeName("unsigned int")]
public enum pw_memmap_flags : uint
{
    PW_MEMMAP_FLAG_NONE = 0,
    PW_MEMMAP_FLAG_READ = (1 << 0),
    PW_MEMMAP_FLAG_WRITE = (1 << 1),
    PW_MEMMAP_FLAG_TWICE = (1 << 2),
    PW_MEMMAP_FLAG_PRIVATE = (1 << 3),
    PW_MEMMAP_FLAG_LOCKED = (1 << 4),
    PW_MEMMAP_FLAG_READWRITE = PW_MEMMAP_FLAG_READ | PW_MEMMAP_FLAG_WRITE,
}
