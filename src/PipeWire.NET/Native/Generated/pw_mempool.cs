#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct pw_mempool
{
    [NativeTypeName("struct pw_properties *")]
    public pw_properties* props;
}
