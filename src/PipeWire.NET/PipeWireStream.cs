using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireStream : IDisposable
{
    private readonly NativeListener<pw_stream_events> _listener;
    private pw_stream* _stream;

    public PipeWireStream(PipeWireCore core, string name, PipeWireProperties? properties = null)
    {
        ArgumentNullException.ThrowIfNull(core);
        ArgumentNullException.ThrowIfNull(name);

        byte[] nameUtf8 = Utf8.ToNative(name);
        fixed (byte* n = nameUtf8)
        {
            _stream = PipeWireException.ThrowIfNull(
                pw_stream_new(core.Handle, (sbyte*)n, properties is null ? null : properties.Release()),
                $"Could not create the stream '{name}'");
        }

        _listener = new NativeListener<pw_stream_events>(this);
        _listener.Events->version = PW_VERSION_STREAM_EVENTS;
        _listener.Events->state_changed = &OnStateChanged;
        _listener.Events->param_changed = &OnParamChanged;
        _listener.Events->add_buffer = &OnAddBuffer;
        _listener.Events->remove_buffer = &OnRemoveBuffer;
        _listener.Events->process = &OnProcess;
        _listener.Events->drained = &OnDrained;
        _listener.Events->control_info = &OnControlInfo;

        pw_stream_add_listener(_stream, _listener.Hook, _listener.Events, _listener.UserData);
    }

    public Action<PipeWireStream>? Process { get; set; }

    public event EventHandler<PipeWireStreamStateEventArgs>? StateChanged;

    public event EventHandler<PipeWireParamEventArgs>? ParamChanged;

    public event EventHandler<PipeWireBufferEventArgs>? BufferAdded;

    public event EventHandler<PipeWireBufferEventArgs>? BufferRemoved;

    public event EventHandler? Drained;

    public event EventHandler<uint>? ControlInfo;

    public pw_stream* Handle => _stream;

    public string? Name => _stream is null ? null : Utf8.FromNative(pw_stream_get_name(_stream));

    public pw_stream_state State
    {
        get
        {
            if (_stream is null)
            {
                return pw_stream_state.PW_STREAM_STATE_UNCONNECTED;
            }

            sbyte* error = null;
            return pw_stream_get_state(_stream, &error);
        }
    }

    public uint NodeId => _stream is null ? PW_ID_ANY : pw_stream_get_node_id(_stream);

    public void Connect(
        spa_direction direction,
        uint targetId,
        pw_stream_flags flags,
        params byte[][] parameters)
    {
        ObjectDisposedException.ThrowIf(_stream is null, this);
        ArgumentNullException.ThrowIfNull(parameters);

        int count = parameters.Length;
        spa_pod** pods = stackalloc spa_pod*[Math.Max(count, 1)];
        GCHandle[] pins = PinPods(parameters, pods);

        try
        {
            PipeWireException.ThrowIfNegative(
                pw_stream_connect(_stream, direction, targetId, flags, count == 0 ? null : pods, (uint)count),
                "Could not connect the stream");
        }
        finally
        {
            UnpinPods(pins);
        }
    }

    public void Disconnect()
    {
        if (_stream is not null)
        {
            PipeWireException.ThrowIfNegative(pw_stream_disconnect(_stream), "Could not disconnect the stream");
        }
    }

    public void UpdateParams(params byte[][] parameters)
    {
        ObjectDisposedException.ThrowIf(_stream is null, this);
        ArgumentNullException.ThrowIfNull(parameters);

        int count = parameters.Length;
        spa_pod** pods = stackalloc spa_pod*[Math.Max(count, 1)];
        GCHandle[] pins = PinPods(parameters, pods);

        try
        {
            PipeWireException.ThrowIfNegative(
                pw_stream_update_params(_stream, count == 0 ? null : pods, (uint)count),
                "Could not update the stream params");
        }
        finally
        {
            UnpinPods(pins);
        }
    }

    public void SetActive(bool active)
    {
        ObjectDisposedException.ThrowIf(_stream is null, this);
        PipeWireException.ThrowIfNegative(
            pw_stream_set_active(_stream, active ? (byte)1 : (byte)0),
            $"Could not {(active ? "activate" : "deactivate")} the stream");
    }

    public void Flush(bool drain = false)
    {
        ObjectDisposedException.ThrowIf(_stream is null, this);
        PipeWireException.ThrowIfNegative(pw_stream_flush(_stream, drain ? (byte)1 : (byte)0), "Could not flush the stream");
    }

    public PipeWireBuffer DequeueBuffer()
        => _stream is null ? default : new PipeWireBuffer(pw_stream_dequeue_buffer(_stream));

    public bool QueueBuffer(PipeWireBuffer buffer)
        => _stream is not null && !buffer.IsNull && pw_stream_queue_buffer(_stream, buffer.Handle) >= 0;

    public bool TryGetTime(out pw_time time)
    {
        time = default;
        if (_stream is null)
        {
            return false;
        }

        fixed (pw_time* t = &time)
        {
            return pw_stream_get_time_n(_stream, t, (nuint)sizeof(pw_time)) >= 0;
        }
    }

    public void SetControl(uint id, params float[] values)
    {
        ObjectDisposedException.ThrowIf(_stream is null, this);
        ArgumentNullException.ThrowIfNull(values);

        fixed (float* v = values)
        {
            PipeWireException.ThrowIfNegative(
                pw_stream_set_control(_stream, id, (uint)values.Length, v, __arglist()),
                $"Could not set the stream control {id}");
        }
    }

    public void Dispose()
    {
        if (_stream is not null)
        {
            pw_stream* stream = _stream;
            _stream = null;

            _listener.Dispose();
            pw_stream_destroy(stream);
        }

        GC.SuppressFinalize(this);
    }

    private static GCHandle[] PinPods(byte[][] parameters, spa_pod** pods)
    {
        if (parameters.Length == 0)
        {
            return [];
        }

        var pins = new GCHandle[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            pins[i] = GCHandle.Alloc(parameters[i], GCHandleType.Pinned);
            pods[i] = (spa_pod*)pins[i].AddrOfPinnedObject();
        }

        return pins;
    }

    private static void UnpinPods(GCHandle[] pins)
    {
        foreach (GCHandle pin in pins)
        {
            if (pin.IsAllocated)
            {
                pin.Free();
            }
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnProcess(void* data)
    {
        PipeWireStream? stream = NativeListener<pw_stream_events>.GetOwner<PipeWireStream>(data);
        if (stream is null)
        {
            return;
        }

        try
        {
            stream.Process?.Invoke(stream);
        }
        catch (Exception ex)
        {
            CallbackGuard.Report(ex);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnStateChanged(void* data, pw_stream_state old, pw_stream_state state, sbyte* error)
    {
        PipeWireStream? stream = NativeListener<pw_stream_events>.GetOwner<PipeWireStream>(data);
        if (stream is null)
        {
            return;
        }

        string? message = Utf8.FromNative(error);
        CallbackGuard.Run(() => stream.StateChanged?.Invoke(stream, new PipeWireStreamStateEventArgs(old, state, message)));
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnParamChanged(void* data, uint id, spa_pod* param)
    {
        PipeWireStream? stream = NativeListener<pw_stream_events>.GetOwner<PipeWireStream>(data);
        if (stream is not null)
        {
            CallbackGuard.Run(() => stream.ParamChanged?.Invoke(stream, new PipeWireParamEventArgs(0, id, 0, 0, param)));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnAddBuffer(void* data, pw_buffer* buffer)
    {
        PipeWireStream? stream = NativeListener<pw_stream_events>.GetOwner<PipeWireStream>(data);
        if (stream is not null)
        {
            CallbackGuard.Run(() => stream.BufferAdded?.Invoke(stream, new PipeWireBufferEventArgs(buffer)));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnRemoveBuffer(void* data, pw_buffer* buffer)
    {
        PipeWireStream? stream = NativeListener<pw_stream_events>.GetOwner<PipeWireStream>(data);
        if (stream is not null)
        {
            CallbackGuard.Run(() => stream.BufferRemoved?.Invoke(stream, new PipeWireBufferEventArgs(buffer)));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnDrained(void* data)
    {
        PipeWireStream? stream = NativeListener<pw_stream_events>.GetOwner<PipeWireStream>(data);
        if (stream is not null)
        {
            CallbackGuard.Run(() => stream.Drained?.Invoke(stream, EventArgs.Empty));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnControlInfo(void* data, uint id, pw_stream_control* control)
    {
        PipeWireStream? stream = NativeListener<pw_stream_events>.GetOwner<PipeWireStream>(data);
        if (stream is not null)
        {
            CallbackGuard.Run(() => stream.ControlInfo?.Invoke(stream, id));
        }
    }
}

public readonly record struct PipeWireStreamStateEventArgs(
    pw_stream_state OldState,
    pw_stream_state NewState,
    string? Error);

public sealed unsafe class PipeWireBufferEventArgs : EventArgs
{
    private readonly pw_buffer* _buffer;

    internal PipeWireBufferEventArgs(pw_buffer* buffer) => _buffer = buffer;

    public PipeWireBuffer Buffer => new(_buffer);
}
