using System.Runtime.InteropServices;

namespace PipeWire.Native;

[StructLayout(LayoutKind.Sequential)]
public struct timespec
{
    public nint tv_sec;

    public nint tv_nsec;
}

[StructLayout(LayoutKind.Sequential)]
public struct itimerspec
{
    public timespec it_interval;

    public timespec it_value;
}

public partial struct _IO_FILE
{
}

public partial struct pw_impl_core
{
}
