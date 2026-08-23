using System.Runtime.InteropServices;
using PipeWire.Native;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public static unsafe class PipeWireLibrary
{
    private static readonly object _sync = new();
    private static int _initCount;

    public static bool IsInitialized
    {
        get
        {
            lock (_sync)
            {
                return _initCount > 0;
            }
        }
    }

    public static event EventHandler<PipeWireCallbackExceptionEventArgs>? UnhandledCallbackException;

    public static string LibraryVersion => Utf8.FromNative(pw_get_library_version()) ?? string.Empty;

    public static string HeadersVersion => $"{PW_MAJOR}.{PW_MINOR}.{PW_MICRO}";

    public static void Init(params string[]? args)
    {
        lock (_sync)
        {
            if (args is null || args.Length == 0)
            {
                pw_init(null, null);
            }
            else
            {
                InitWithArguments(args);
            }

            _initCount++;
        }
    }

    public static void Deinit()
    {
        lock (_sync)
        {
            if (_initCount == 0)
            {
                return;
            }

            _initCount--;
            pw_deinit();
        }
    }

    public static bool CheckLibraryVersion(int major, int minor, int micro)
        => pw_check_library_version(major, minor, micro) != 0;

    public static void SetLogLevel(spa_log_level level) => pw_log_set_level(level);

    internal static bool RaiseUnhandledCallbackException(Exception exception)
    {
        EventHandler<PipeWireCallbackExceptionEventArgs>? handlers = UnhandledCallbackException;
        if (handlers is null)
        {
            return false;
        }

        var args = new PipeWireCallbackExceptionEventArgs(exception);
        handlers(null, args);
        return args.Handled;
    }

    internal static void EnsureInitialized()
    {
        if (!IsInitialized)
        {
            throw new InvalidOperationException(
                $"{nameof(PipeWireLibrary)}.{nameof(Init)}() must be called before using PipeWire.");
        }
    }

    private static void InitWithArguments(string[] args)
    {
        int argc = args.Length;
        byte** argv = (byte**)NativeMemory.Alloc((nuint)((argc + 1) * sizeof(byte*)));

        try
        {
            for (int i = 0; i < argc; i++)
            {
                argv[i] = (byte*)Marshal.StringToCoTaskMemUTF8(args[i] ?? string.Empty);
            }

            argv[argc] = null;

            byte** argvCopy = argv;
            pw_init(&argc, (sbyte***)&argvCopy);
        }
        finally
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (argv[i] is not null)
                {
                    Marshal.FreeCoTaskMem((IntPtr)argv[i]);
                }
            }

            NativeMemory.Free(argv);
        }
    }
}
