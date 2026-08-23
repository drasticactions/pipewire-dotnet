using System.Runtime.InteropServices;
using PipeWire.Native;

namespace PipeWire;

public sealed class PipeWireException : Exception
{
    public PipeWireException(string message, int errno = 0)
        : base(errno == 0 ? message : $"{message}: {Marshal.GetPInvokeErrorMessage(errno)} (errno {errno})")
        => Errno = errno;

    public int Errno { get; }

    internal static PipeWireException FromErrno(string message) => new(message, Libc.Errno);

    internal static int ThrowIfNegative(int result, string message)
        => result >= 0 ? result : throw new PipeWireException(message, result == -1 ? Libc.Errno : -result);

    internal static unsafe T* ThrowIfNull<T>(T* result, string message)
        where T : unmanaged
        => result is null ? throw FromErrno(message) : result;
}
