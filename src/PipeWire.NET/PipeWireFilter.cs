using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireFilter : IDisposable
{
    private readonly NativeListener<pw_filter_events> _listener;
    private readonly List<PipeWireFilterPort> _ports = [];
    private pw_filter* _filter;
    private PipeWireProperties? _propertiesView;

    public PipeWireFilter(PipeWireCore core, string name, PipeWireProperties? properties = null)
    {
        ArgumentNullException.ThrowIfNull(core);
        ArgumentNullException.ThrowIfNull(name);

        byte[] nameUtf8 = Utf8.ToNative(name);
        fixed (byte* n = nameUtf8)
        {
            _filter = PipeWireException.ThrowIfNull(
                pw_filter_new(core.Handle, (sbyte*)n, properties is null ? null : properties.Release()),
                $"Could not create the filter '{name}'");
        }

        _listener = new NativeListener<pw_filter_events>(this);
        _listener.Events->version = PW_VERSION_FILTER_EVENTS;
        _listener.Events->state_changed = &OnStateChanged;
        _listener.Events->param_changed = &OnParamChanged;
        _listener.Events->process = &OnProcess;
        _listener.Events->drained = &OnDrained;

        pw_filter_add_listener(_filter, _listener.Hook, _listener.Events, _listener.UserData);
    }

    public Action<PipeWireFilter, uint>? Process { get; set; }

    public event EventHandler<PipeWireFilterStateEventArgs>? StateChanged;

    public event EventHandler<PipeWireParamEventArgs>? ParamChanged;

    public event EventHandler? Drained;

    public pw_filter* Handle => _filter;

    public string? Name => _filter is null ? null : Utf8.FromNative(pw_filter_get_name(_filter));

    public pw_filter_state State
    {
        get
        {
            if (_filter is null)
            {
                return pw_filter_state.PW_FILTER_STATE_UNCONNECTED;
            }

            sbyte* error = null;
            return pw_filter_get_state(_filter, &error);
        }
    }

    public uint NodeId => _filter is null ? PW_ID_ANY : pw_filter_get_node_id(_filter);

    public PipeWireProperties Properties
    {
        get
        {
            ObjectDisposedException.ThrowIf(_filter is null, this);
            pw_properties* properties = pw_filter_get_properties(_filter, null);

            if (_propertiesView is null || _propertiesView.Handle != properties)
            {
                _propertiesView = PipeWireProperties.Borrow(properties);
            }

            return _propertiesView;
        }
    }

    public PipeWireProperties CopyProperties()
    {
        ObjectDisposedException.ThrowIf(_filter is null, this);
        return PipeWireProperties.Copy(pw_filter_get_properties(_filter, null));
    }

    public int UpdateProperties(IEnumerable<KeyValuePair<string, string?>> properties)
    {
        ArgumentNullException.ThrowIfNull(properties);
        ObjectDisposedException.ThrowIf(_filter is null, this);

        using var dict = new SpaDictionary(properties);
        return PipeWireException.ThrowIfNegative(
            pw_filter_update_properties(_filter, null, dict.Handle),
            "Could not update the filter properties");
    }

    public int UpdateProperties(params SpaDictionaryEntry[] properties)
    {
        ArgumentNullException.ThrowIfNull(properties);
        ObjectDisposedException.ThrowIf(_filter is null, this);

        using SpaDictionary dict = SpaDictionary.FromEntries(properties);
        return PipeWireException.ThrowIfNegative(
            pw_filter_update_properties(_filter, null, dict.Handle),
            "Could not update the filter properties");
    }

    public IReadOnlyList<PipeWireFilterPort> Ports => _ports;

    public PipeWireFilterPort AddPort(
        spa_direction direction,
        pw_filter_port_flags flags,
        PipeWireProperties? properties = null,
        params byte[][] parameters)
    {
        ObjectDisposedException.ThrowIf(_filter is null, this);
        ArgumentNullException.ThrowIfNull(parameters);

        int count = parameters.Length;
        spa_pod** pods = stackalloc spa_pod*[Math.Max(count, 1)];
        GCHandle[] pins = PinPods(parameters, pods);

        try
        {
            void* portData = pw_filter_add_port(
                _filter,
                direction,
                flags,
                0,
                properties is null ? null : properties.Release(),
                count == 0 ? null : pods,
                (uint)count);

            if (portData is null)
            {
                throw PipeWireException.FromErrno("Could not add a port to the filter");
            }

            var port = new PipeWireFilterPort(this, portData, direction);
            _ports.Add(port);
            return port;
        }
        finally
        {
            UnpinPods(pins);
        }
    }

    public void Connect(pw_filter_flags flags, params byte[][] parameters)
    {
        ObjectDisposedException.ThrowIf(_filter is null, this);
        ArgumentNullException.ThrowIfNull(parameters);

        int count = parameters.Length;
        spa_pod** pods = stackalloc spa_pod*[Math.Max(count, 1)];
        GCHandle[] pins = PinPods(parameters, pods);

        try
        {
            PipeWireException.ThrowIfNegative(
                pw_filter_connect(_filter, flags, count == 0 ? null : pods, (uint)count),
                "Could not connect the filter");
        }
        finally
        {
            UnpinPods(pins);
        }
    }

    public void Disconnect()
    {
        if (_filter is not null)
        {
            PipeWireException.ThrowIfNegative(pw_filter_disconnect(_filter), "Could not disconnect the filter");
        }
    }

    public void SetActive(bool active)
    {
        ObjectDisposedException.ThrowIf(_filter is null, this);
        PipeWireException.ThrowIfNegative(
            pw_filter_set_active(_filter, active ? (byte)1 : (byte)0),
            $"Could not {(active ? "activate" : "deactivate")} the filter");
    }

    public void Flush(bool drain = false)
    {
        ObjectDisposedException.ThrowIf(_filter is null, this);
        PipeWireException.ThrowIfNegative(pw_filter_flush(_filter, drain ? (byte)1 : (byte)0), "Could not flush the filter");
    }

    public void Dispose()
    {
        if (_filter is not null)
        {
            pw_filter* filter = _filter;
            _filter = null;

            _ports.Clear();
            _listener.Dispose();
            pw_filter_destroy(filter);
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
    private static void OnProcess(void* data, spa_io_position* position)
    {
        PipeWireFilter? filter = NativeListener<pw_filter_events>.GetOwner<PipeWireFilter>(data);
        if (filter is null)
        {
            return;
        }

        uint samples = position is null ? 0 : (uint)position->clock.duration;

        try
        {
            filter.Process?.Invoke(filter, samples);
        }
        catch (Exception ex)
        {
            CallbackGuard.Report(ex);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnStateChanged(void* data, pw_filter_state old, pw_filter_state state, sbyte* error)
    {
        PipeWireFilter? filter = NativeListener<pw_filter_events>.GetOwner<PipeWireFilter>(data);
        if (filter is null)
        {
            return;
        }

        string? message = Utf8.FromNative(error);
        CallbackGuard.Run(() => filter.StateChanged?.Invoke(filter, new PipeWireFilterStateEventArgs(old, state, message)));
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnParamChanged(void* data, void* portData, uint id, spa_pod* param)
    {
        PipeWireFilter? filter = NativeListener<pw_filter_events>.GetOwner<PipeWireFilter>(data);
        if (filter is not null)
        {
            CallbackGuard.Run(() => filter.ParamChanged?.Invoke(filter, new PipeWireParamEventArgs(0, id, 0, 0, param)));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnDrained(void* data)
    {
        PipeWireFilter? filter = NativeListener<pw_filter_events>.GetOwner<PipeWireFilter>(data);
        if (filter is not null)
        {
            CallbackGuard.Run(() => filter.Drained?.Invoke(filter, EventArgs.Empty));
        }
    }
}

public sealed unsafe class PipeWireFilterPort
{
    private readonly PipeWireFilter _filter;
    private readonly void* _portData;
    private PipeWireProperties? _propertiesView;

    internal PipeWireFilterPort(PipeWireFilter filter, void* portData, spa_direction direction)
    {
        _filter = filter;
        _portData = portData;
        Direction = direction;
    }

    public void* Handle => _portData;

    public spa_direction Direction { get; }

    public PipeWireProperties Properties
    {
        get
        {
            pw_properties* properties = pw_filter_get_properties(_filter.Handle, _portData);

            if (_propertiesView is null || _propertiesView.Handle != properties)
            {
                _propertiesView = PipeWireProperties.Borrow(properties);
            }

            return _propertiesView;
        }
    }

    public PipeWireProperties CopyProperties()
        => PipeWireProperties.Copy(pw_filter_get_properties(_filter.Handle, _portData));

    public int UpdateProperties(IEnumerable<KeyValuePair<string, string?>> properties)
    {
        ArgumentNullException.ThrowIfNull(properties);

        using var dict = new SpaDictionary(properties);
        return PipeWireException.ThrowIfNegative(
            pw_filter_update_properties(_filter.Handle, _portData, dict.Handle),
            "Could not update the filter port properties");
    }

    public int UpdateProperties(params SpaDictionaryEntry[] properties)
    {
        ArgumentNullException.ThrowIfNull(properties);

        using SpaDictionary dict = SpaDictionary.FromEntries(properties);
        return PipeWireException.ThrowIfNegative(
            pw_filter_update_properties(_filter.Handle, _portData, dict.Handle),
            "Could not update the filter port properties");
    }

    public Span<float> GetDspBuffer(uint sampleCount)
    {
        void* buffer = pw_filter_get_dsp_buffer(_portData, sampleCount);
        return buffer is null ? default : new Span<float>(buffer, (int)sampleCount);
    }

    public PipeWireBuffer DequeueBuffer() => new(pw_filter_dequeue_buffer(_portData));

    public bool QueueBuffer(PipeWireBuffer buffer)
        => !buffer.IsNull && pw_filter_queue_buffer(_portData, buffer.Handle) >= 0;
}

public readonly record struct PipeWireFilterStateEventArgs(
    pw_filter_state OldState,
    pw_filter_state NewState,
    string? Error);
