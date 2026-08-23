using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireCore : IDisposable
{
    private readonly PipeWireContext _context;
    private readonly NativeListener<pw_core_events> _listener;
    private pw_core* _core;

    internal PipeWireCore(PipeWireContext context, pw_core* core)
    {
        _context = context;
        _core = core;

        _listener = new NativeListener<pw_core_events>(this);
        _listener.Events->version = PW_VERSION_CORE_EVENTS;
        _listener.Events->info = &OnInfo;
        _listener.Events->done = &OnDone;
        _listener.Events->ping = &OnPing;
        _listener.Events->error = &OnError;
        _listener.Events->remove_id = &OnRemoveId;
        _listener.Events->bound_id = &OnBoundId;
        _listener.Events->bound_props = &OnBoundProps;

        pw_core_methods* methods = SpaInterface.GetMethods<pw_core_methods>(
            (spa_interface*)_core, PW_VERSION_CORE_METHODS, "add_listener", out void* data);

        if (methods->add_listener is null)
        {
            throw new NotSupportedException("The core does not implement add_listener.");
        }

        methods->add_listener(data, _listener.Hook, _listener.Events, _listener.UserData);
    }

    public event EventHandler<PipeWireCoreInfo>? Info;

    public event EventHandler<PipeWireDoneEventArgs>? Done;

    public event EventHandler<PipeWireDoneEventArgs>? Ping;

    public event EventHandler<PipeWireErrorEventArgs>? Error;

    public event EventHandler<uint>? RemoveId;

    public event EventHandler<PipeWireBoundEventArgs>? Bound;

    public pw_core* Handle => _core;

    public PipeWireContext Context => _context;

    public PipeWireCoreInfo? ServerInfo { get; private set; }

    public PipeWireProperties GetProperties()
    {
        ObjectDisposedException.ThrowIf(_core is null, this);
        return PipeWireProperties.Copy(pw_core_get_properties(_core));
    }

    public int Sync(uint id = PW_ID_CORE, int sequence = 0)
    {
        ObjectDisposedException.ThrowIf(_core is null, this);

        pw_core_methods* methods = SpaInterface.GetMethods<pw_core_methods>(
            (spa_interface*)_core, PW_VERSION_CORE_METHODS, "sync", out void* data);

        return methods->sync is null
            ? throw new NotSupportedException("The core does not implement sync.")
            : PipeWireException.ThrowIfNegative(methods->sync(data, id, sequence), "Could not sync with the PipeWire daemon");
    }

    public void Pong(uint id, int sequence)
    {
        ObjectDisposedException.ThrowIf(_core is null, this);

        pw_core_methods* methods = SpaInterface.GetMethods<pw_core_methods>(
            (spa_interface*)_core, PW_VERSION_CORE_METHODS, "pong", out void* data);

        if (methods->pong is not null)
        {
            PipeWireException.ThrowIfNegative(methods->pong(data, id, sequence), "Could not reply to a ping");
        }
    }

    public PipeWireRegistry GetRegistry()
    {
        ObjectDisposedException.ThrowIf(_core is null, this);

        pw_core_methods* methods = SpaInterface.GetMethods<pw_core_methods>(
            (spa_interface*)_core, PW_VERSION_CORE_METHODS, "get_registry", out void* data);

        if (methods->get_registry is null)
        {
            throw new NotSupportedException("The core does not implement get_registry.");
        }

        pw_registry* registry = PipeWireException.ThrowIfNull(
            methods->get_registry(data, PW_VERSION_REGISTRY, 0),
            "Could not get the PipeWire registry");

        return new PipeWireRegistry(registry);
    }

    public PipeWireProxy CreateObject(
        string factoryName,
        string type,
        uint version,
        IEnumerable<KeyValuePair<string, string?>>? properties = null)
    {
        ArgumentNullException.ThrowIfNull(factoryName);
        ArgumentNullException.ThrowIfNull(type);
        ObjectDisposedException.ThrowIf(_core is null, this);

        pw_core_methods* methods = SpaInterface.GetMethods<pw_core_methods>(
            (spa_interface*)_core, PW_VERSION_CORE_METHODS, "create_object", out void* data);

        if (methods->create_object is null)
        {
            throw new NotSupportedException("The core does not implement create_object.");
        }

        using var dict = new SpaDictionary(properties ?? []);
        byte[] factoryUtf8 = Utf8.ToNative(factoryName);
        byte[] typeUtf8 = Utf8.ToNative(type);

        fixed (byte* f = factoryUtf8)
        fixed (byte* t = typeUtf8)
        {
            void* proxy = methods->create_object(data, (sbyte*)f, (sbyte*)t, version, dict.Handle, 0);
            return new PipeWireProxy(PipeWireException.ThrowIfNull((pw_proxy*)proxy, $"Could not create an object from the '{factoryName}' factory"));
        }
    }

    public void Dispose()
    {
        if (_core is not null)
        {
            _listener.Dispose();
            pw_core_disconnect(_core);
            _core = null;
        }

        GC.SuppressFinalize(this);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnInfo(void* data, pw_core_info* info)
    {
        PipeWireCore? core = NativeListener<pw_core_events>.GetOwner<PipeWireCore>(data);
        if (core is null)
        {
            return;
        }

        CallbackGuard.Run(() =>
        {
            var snapshot = PipeWireCoreInfo.From(info);
            core.ServerInfo = snapshot;
            core.Info?.Invoke(core, snapshot);
        });
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnDone(void* data, uint id, int seq)
    {
        PipeWireCore? core = NativeListener<pw_core_events>.GetOwner<PipeWireCore>(data);
        if (core is not null)
        {
            CallbackGuard.Run(() => core.Done?.Invoke(core, new PipeWireDoneEventArgs(id, seq)));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnPing(void* data, uint id, int seq)
    {
        PipeWireCore? core = NativeListener<pw_core_events>.GetOwner<PipeWireCore>(data);
        if (core is null)
        {
            return;
        }

        CallbackGuard.Run(() =>
        {
            core.Ping?.Invoke(core, new PipeWireDoneEventArgs(id, seq));

            core.Pong(id, seq);
        });
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnError(void* data, uint id, int seq, int res, sbyte* message)
    {
        PipeWireCore? core = NativeListener<pw_core_events>.GetOwner<PipeWireCore>(data);
        if (core is null)
        {
            return;
        }

        string? text = Utf8.FromNative(message);
        CallbackGuard.Run(() => core.Error?.Invoke(core, new PipeWireErrorEventArgs(id, seq, res, text)));
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnRemoveId(void* data, uint id)
    {
        PipeWireCore? core = NativeListener<pw_core_events>.GetOwner<PipeWireCore>(data);
        if (core is not null)
        {
            CallbackGuard.Run(() => core.RemoveId?.Invoke(core, id));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnBoundId(void* data, uint id, uint globalId)
    {
        PipeWireCore? core = NativeListener<pw_core_events>.GetOwner<PipeWireCore>(data);
        if (core is not null)
        {
            CallbackGuard.Run(() => core.Bound?.Invoke(core, new PipeWireBoundEventArgs(id, globalId, null)));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnBoundProps(void* data, uint id, uint globalId, spa_dict* props)
    {
        PipeWireCore? core = NativeListener<pw_core_events>.GetOwner<PipeWireCore>(data);
        if (core is null)
        {
            return;
        }

        Dictionary<string, string?> properties = SpaDictionary.ToDictionary(props);
        CallbackGuard.Run(() => core.Bound?.Invoke(core, new PipeWireBoundEventArgs(id, globalId, properties)));
    }
}

public sealed record PipeWireCoreInfo(
    uint Id,
    uint Cookie,
    string? UserName,
    string? HostName,
    string? Version,
    string? Name,
    IReadOnlyDictionary<string, string?> Properties)
{
    internal static unsafe PipeWireCoreInfo From(pw_core_info* info) => new(
        info->id,
        info->cookie,
        Utf8.FromNative(info->user_name),
        Utf8.FromNative(info->host_name),
        Utf8.FromNative(info->version),
        Utf8.FromNative(info->name),
        SpaDictionary.ToDictionary(info->props));
}

public readonly record struct PipeWireDoneEventArgs(uint Id, int Sequence);

public readonly record struct PipeWireErrorEventArgs(uint Id, int Sequence, int Result, string? Message);

public readonly record struct PipeWireBoundEventArgs(uint Id, uint GlobalId, IReadOnlyDictionary<string, string?>? Properties);
