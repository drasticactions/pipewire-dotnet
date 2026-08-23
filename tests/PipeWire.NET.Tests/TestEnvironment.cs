using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PipeWire.Tests;

internal static partial class TestEnvironment
{
    [ModuleInitializer]
    internal static void Initialize() => setenv("PIPEWIRE_DLCLOSE", "false", 1);

    [LibraryImport("libc", StringMarshalling = StringMarshalling.Utf8)]
    private static partial int setenv(string name, string value, int overwrite);
}
