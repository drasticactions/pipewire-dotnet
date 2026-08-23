using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireLink : PipeWireProxy
{
    private readonly NativeListener<pw_link_events> _events;

    internal PipeWireLink(pw_proxy* proxy)
        : base(proxy)
    {
        _events = new NativeListener<pw_link_events>(this);
        _events.Events->version = PW_VERSION_LINK_EVENTS;
        _events.Events->info = &OnInfo;

        AddObjectListener(_events);
    }

    public event EventHandler<PipeWireLinkInfo>? Info;

    public PipeWireLinkInfo? LinkInfo { get; private set; }

    private protected override void DisposeObjectListener() => _events.Dispose();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnInfo(void* data, pw_link_info* info)
    {
        PipeWireLink? link = NativeListener<pw_link_events>.GetOwner<PipeWireLink>(data);
        if (link is null)
        {
            return;
        }

        var snapshot = PipeWireLinkInfo.From(info);
        CallbackGuard.Run(() =>
        {
            link.LinkInfo = snapshot;
            link.Info?.Invoke(link, snapshot);
        });
    }
}

public sealed record PipeWireLinkInfo(
    uint Id,
    uint OutputNodeId,
    uint OutputPortId,
    uint InputNodeId,
    uint InputPortId,
    pw_link_state State,
    string? Error,
    byte[]? Format,
    IReadOnlyDictionary<string, string?> Properties)
{
    internal static unsafe PipeWireLinkInfo From(pw_link_info* info) => new(
        info->id,
        info->output_node_id,
        info->output_port_id,
        info->input_node_id,
        info->input_port_id,
        info->state,
        Utf8.FromNative(info->error),

        info->format is null ? null : new SpaPod(info->format).Bytes.ToArray(),
        SpaDictionary.ToDictionary(info->props));
}
