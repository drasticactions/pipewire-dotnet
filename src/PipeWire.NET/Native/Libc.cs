using System.Runtime.InteropServices;

namespace PipeWire.Native;

internal static unsafe class Libc
{
    private static readonly delegate* unmanaged[Cdecl]<int*> _errnoLocation;
    private static readonly delegate* unmanaged[Cdecl]<PollFd*, nuint, int, int> _poll;

    static Libc()
    {
        var self = NativeLibrary.GetMainProgramHandle();

        _errnoLocation = (delegate* unmanaged[Cdecl]<int*>)NativeLibrary.GetExport(self, "__errno_location");
        _poll = (delegate* unmanaged[Cdecl]<PollFd*, nuint, int, int>)NativeLibrary.GetExport(self, "poll");
    }

    internal const short POLLIN = 0x1;

    internal const int EINTR = 4;

    internal static int Errno => *_errnoLocation();

    internal static int poll(PollFd* fds, nuint nfds, int timeout) => _poll(fds, nfds, timeout);

    [StructLayout(LayoutKind.Sequential)]
    internal struct PollFd
    {
        internal int fd;
        internal short events;
        internal short revents;
    }
}
