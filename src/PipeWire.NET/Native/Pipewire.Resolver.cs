using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PipeWire.Native;

public static unsafe partial class Pipewire
{
    public const string LibraryName = "pipewire-0.3";

#pragma warning disable CA2255
    [ModuleInitializer]
    internal static void Initialize()
    {
        NativeLibrary.SetDllImportResolver(typeof(Pipewire).Assembly, static (name, assembly, searchPath) =>
        {
            if (name != LibraryName)
            {
                return IntPtr.Zero;
            }

            foreach (var candidate in (ReadOnlySpan<string>)["libpipewire-0.3.so.0", "libpipewire-0.3.so", "libpipewire-0.3"])
            {
                if (NativeLibrary.TryLoad(candidate, assembly, searchPath, out var handle))
                {
                    return handle;
                }
            }

            return IntPtr.Zero;
        });
    }
#pragma warning restore CA2255
}
