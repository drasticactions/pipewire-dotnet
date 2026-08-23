using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireRegistry : PipeWireProxy
{
    private readonly NativeListener<pw_registry_events> _events;

    internal PipeWireRegistry(pw_registry* registry)
        : base((pw_proxy*)registry)
    {
        _events = new NativeListener<pw_registry_events>(this);
        _events.Events->version = PW_VERSION_REGISTRY_EVENTS;
        _events.Events->global = &OnGlobal;
        _events.Events->global_remove = &OnGlobalRemove;

        AddObjectListener(_events);
    }

    public event EventHandler<PipeWireGlobal>? GlobalAdded;

    public event EventHandler<uint>? GlobalRemoved;

    public new pw_registry* Handle => (pw_registry*)base.Handle;

    public PipeWireProxy Bind(uint id, string type, uint version)
    {
        ArgumentNullException.ThrowIfNull(type);
        return new PipeWireProxy(BindRaw(id, type, version));
    }

    public PipeWireNode BindNode(uint id)
        => new(BindRaw(id, PipeWireInterfaces.Node, (uint)PW_VERSION_NODE));

    public PipeWireDevice BindDevice(uint id)
        => new(BindRaw(id, PipeWireInterfaces.Device, (uint)PW_VERSION_DEVICE));

    public PipeWirePort BindPort(uint id)
        => new(BindRaw(id, PipeWireInterfaces.Port, (uint)PW_VERSION_PORT));

    public PipeWireLink BindLink(uint id)
        => new(BindRaw(id, PipeWireInterfaces.Link, (uint)PW_VERSION_LINK));

    public PipeWireClient BindClient(uint id)
        => new(BindRaw(id, PipeWireInterfaces.Client, (uint)PW_VERSION_CLIENT));

    public PipeWireMetadata BindMetadata(uint id)
        => new(BindRaw(id, PipeWireInterfaces.Metadata, (uint)PW_VERSION_METADATA));

    public void Destroy(uint id)
    {
        pw_registry_methods* methods = GetMethods<pw_registry_methods>(PW_VERSION_REGISTRY_METHODS, "destroy", out void* data);

        if (methods->destroy is null)
        {
            throw new NotSupportedException("The registry does not implement destroy.");
        }

        PipeWireException.ThrowIfNegative(methods->destroy(data, id), $"Could not destroy the global {id}");
    }

    private protected override void DisposeObjectListener() => _events.Dispose();

    private pw_proxy* BindRaw(uint id, string type, uint version)
    {
        pw_registry_methods* methods = GetMethods<pw_registry_methods>(PW_VERSION_REGISTRY_METHODS, "bind", out void* data);

        if (methods->bind is null)
        {
            throw new NotSupportedException("The registry does not implement bind.");
        }

        byte[] typeUtf8 = Utf8.ToNative(type);
        fixed (byte* t = typeUtf8)
        {
            void* proxy = methods->bind(data, id, (sbyte*)t, version, 0);
            return PipeWireException.ThrowIfNull((pw_proxy*)proxy, $"Could not bind the global {id} as {type}");
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnGlobal(void* data, uint id, uint permissions, sbyte* type, uint version, spa_dict* props)
    {
        PipeWireRegistry? registry = NativeListener<pw_registry_events>.GetOwner<PipeWireRegistry>(data);
        if (registry is null)
        {
            return;
        }

        var global = new PipeWireGlobal(
            id,
            permissions,
            Utf8.FromNative(type) ?? string.Empty,
            version,
            SpaDictionary.ToDictionary(props));

        CallbackGuard.Run(() => registry.GlobalAdded?.Invoke(registry, global));
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnGlobalRemove(void* data, uint id)
    {
        PipeWireRegistry? registry = NativeListener<pw_registry_events>.GetOwner<PipeWireRegistry>(data);
        if (registry is not null)
        {
            CallbackGuard.Run(() => registry.GlobalRemoved?.Invoke(registry, id));
        }
    }
}

public sealed record PipeWireGlobal(
    uint Id,
    uint Permissions,
    string Type,
    uint Version,
    IReadOnlyDictionary<string, string?> Properties)
{
    public bool IsNode => Type == PipeWireInterfaces.Node;

    public bool IsPort => Type == PipeWireInterfaces.Port;

    public bool IsLink => Type == PipeWireInterfaces.Link;

    public bool IsDevice => Type == PipeWireInterfaces.Device;

    public string? GetProperty(string key) => Properties.TryGetValue(key, out string? value) ? value : null;
}
