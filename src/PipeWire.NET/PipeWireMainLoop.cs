using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireMainLoop : IDisposable
{
    private pw_main_loop* _loop;

    public PipeWireMainLoop(SpaDictionary? properties = null)
    {
        PipeWireLibrary.EnsureInitialized();

        _loop = PipeWireException.ThrowIfNull(
            pw_main_loop_new(properties is null ? null : properties.Handle),
            "Could not create the PipeWire main loop");

        Loop = new PipeWireLoop(pw_main_loop_get_loop(_loop));
    }

    public pw_main_loop* Handle => _loop;

    public PipeWireLoop Loop { get; }

    public void Run()
    {
        ObjectDisposedException.ThrowIf(_loop is null, this);
        PipeWireException.ThrowIfNegative(pw_main_loop_run(_loop), "The PipeWire main loop failed");
    }

    public void Quit()
    {
        if (_loop is not null)
        {
            PipeWireException.ThrowIfNegative(pw_main_loop_quit(_loop), "Could not quit the PipeWire main loop");
        }
    }

    public void Dispose()
    {
        if (_loop is not null)
        {
            pw_main_loop_destroy(_loop);
            _loop = null;
        }

        GC.SuppressFinalize(this);
    }
}
