using System.Runtime.InteropServices;
using System.Text;

namespace PipeWire;

internal static unsafe class Utf8
{
    internal static byte[] ToNative(string value) => Encoding.UTF8.GetBytes(value + '\0');

    internal static byte[]? ToNativeOrNull(string? value) => value is null ? null : ToNative(value);

    internal static string? FromNative(sbyte* value) => Marshal.PtrToStringUTF8((IntPtr)value);
}
