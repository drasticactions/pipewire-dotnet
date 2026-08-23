using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireThreadLoop : IDisposable
{
    private pw_thread_loop* _loop;
    private bool _started;

    public PipeWireThreadLoop(string? name = null, SpaDictionary? properties = null)
    {
        PipeWireLibrary.EnsureInitialized();

        byte[]? utf8 = Utf8.ToNativeOrNull(name);
        fixed (byte* p = utf8)
        {
            _loop = PipeWireException.ThrowIfNull(
                pw_thread_loop_new((sbyte*)p, properties is null ? null : properties.Handle),
                "Could not create the PipeWire thread loop");
        }

        Loop = new PipeWireLoop(pw_thread_loop_get_loop(_loop));
    }

    public pw_thread_loop* Handle => _loop;

    public PipeWireLoop Loop { get; }

    public bool IsRunning => _started;

    public void Start()
    {
        ObjectDisposedException.ThrowIf(_loop is null, this);

        if (_started)
        {
            return;
        }

        PipeWireException.ThrowIfNegative(pw_thread_loop_start(_loop), "Could not start the PipeWire thread loop");
        _started = true;
    }

    public void Stop()
    {
        if (_loop is null || !_started)
        {
            return;
        }

        if (pw_thread_loop_in_thread(_loop) != 0)
        {
            throw new InvalidOperationException(
                "A PipeWire thread loop cannot be stopped from its own thread. This usually means an " +
                "'await' resumed on the loop thread because a TaskCompletionSource was completed from a " +
                "PipeWire callback; create it with TaskCreationOptions.RunContinuationsAsynchronously so " +
                "the continuation runs elsewhere.");
        }

        pw_thread_loop_stop(_loop);
        _started = false;
    }

    public LockScope Lock()
    {
        ObjectDisposedException.ThrowIf(_loop is null, this);
        pw_thread_loop_lock(_loop);
        return new LockScope(this);
    }

    public void Wait()
    {
        ObjectDisposedException.ThrowIf(_loop is null, this);
        pw_thread_loop_wait(_loop);
    }

    public bool TimedWait(TimeSpan timeout)
    {
        ObjectDisposedException.ThrowIf(_loop is null, this);

        int seconds = (int)Math.Ceiling(timeout.TotalSeconds);
        return pw_thread_loop_timed_wait(_loop, seconds) == 0;
    }

    public void Signal(bool waitForAccept = false)
    {
        ObjectDisposedException.ThrowIf(_loop is null, this);
        pw_thread_loop_signal(_loop, waitForAccept ? (byte)1 : (byte)0);
    }

    public void Accept()
    {
        ObjectDisposedException.ThrowIf(_loop is null, this);
        pw_thread_loop_accept(_loop);
    }

    public bool IsCallerOnLoopThread => _loop is not null && pw_thread_loop_in_thread(_loop) != 0;

    public void Dispose()
    {
        if (_loop is not null)
        {
            Stop();
            pw_thread_loop_destroy(_loop);
            _loop = null;
        }

        GC.SuppressFinalize(this);
    }

    private void Unlock()
    {
        if (_loop is not null)
        {
            pw_thread_loop_unlock(_loop);
        }
    }

    public readonly struct LockScope : IDisposable
    {
        private readonly PipeWireThreadLoop? _loop;

        internal LockScope(PipeWireThreadLoop loop) => _loop = loop;

        public void Dispose() => _loop?.Unlock();
    }
}
