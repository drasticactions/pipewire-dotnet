using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireClient : PipeWireProxy
{
    private readonly NativeListener<pw_client_events> _events;

    internal PipeWireClient(pw_proxy* proxy)
        : base(proxy)
    {
        _events = new NativeListener<pw_client_events>(this);
        _events.Events->version = PW_VERSION_CLIENT_EVENTS;
        _events.Events->info = &OnInfo;

        AddObjectListener(_events);
    }

    public event EventHandler<PipeWireClientInfo>? Info;

    public PipeWireClientInfo? ClientInfo { get; private set; }

    private protected override void DisposeObjectListener() => _events.Dispose();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnInfo(void* data, pw_client_info* info)
    {
        PipeWireClient? client = NativeListener<pw_client_events>.GetOwner<PipeWireClient>(data);
        if (client is null)
        {
            return;
        }

        var snapshot = PipeWireClientInfo.From(info);
        CallbackGuard.Run(() =>
        {
            client.ClientInfo = snapshot;
            client.Info?.Invoke(client, snapshot);
        });
    }
}

public sealed record PipeWireClientInfo(uint Id, IReadOnlyDictionary<string, string?> Properties)
{
    internal static unsafe PipeWireClientInfo From(pw_client_info* info)
        => new(info->id, SpaDictionary.ToDictionary(info->props));
}
