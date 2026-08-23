using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWirePort : PipeWireProxy
{
    private readonly NativeListener<pw_port_events> _events;

    internal PipeWirePort(pw_proxy* proxy)
        : base(proxy)
    {
        _events = new NativeListener<pw_port_events>(this);
        _events.Events->version = PW_VERSION_PORT_EVENTS;
        _events.Events->info = &OnInfo;
        _events.Events->param = &OnParam;

        AddObjectListener(_events);
    }

    public event EventHandler<PipeWirePortInfo>? Info;

    public event EventHandler<PipeWireParamEventArgs>? ParamChanged;

    public PipeWirePortInfo? PortInfo { get; private set; }

    public int EnumParams(
        spa_param_type id,
        uint index = 0,
        uint count = uint.MaxValue,
        ReadOnlySpan<byte> filter = default,
        int sequence = 0)
    {
        pw_port_methods* methods = GetMethods<pw_port_methods>(PW_VERSION_PORT_METHODS, "enum_params", out void* data);
        return ParamOperations.EnumParams(methods->enum_params, data, "port", sequence, id, index, count, filter);
    }

    public void SubscribeParams(params spa_param_type[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids);
        pw_port_methods* methods = GetMethods<pw_port_methods>(PW_VERSION_PORT_METHODS, "subscribe_params", out void* data);
        ParamOperations.SubscribeParams(methods->subscribe_params, data, "port", ids);
    }

    private protected override void DisposeObjectListener() => _events.Dispose();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnInfo(void* data, pw_port_info* info)
    {
        PipeWirePort? port = NativeListener<pw_port_events>.GetOwner<PipeWirePort>(data);
        if (port is null)
        {
            return;
        }

        var snapshot = PipeWirePortInfo.From(info);
        CallbackGuard.Run(() =>
        {
            port.PortInfo = snapshot;
            port.Info?.Invoke(port, snapshot);
        });
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnParam(void* data, int seq, uint id, uint index, uint next, spa_pod* param)
    {
        PipeWirePort? port = NativeListener<pw_port_events>.GetOwner<PipeWirePort>(data);
        if (port is not null)
        {
            CallbackGuard.Run(() => port.ParamChanged?.Invoke(port, new PipeWireParamEventArgs(seq, id, index, next, param)));
        }
    }
}

public sealed record PipeWirePortInfo(
    uint Id,
    spa_direction Direction,
    IReadOnlyDictionary<string, string?> Properties,
    IReadOnlyList<PipeWireParamInfo> Params)
{
    internal static unsafe PipeWirePortInfo From(pw_port_info* info) => new(
        info->id,
        info->direction,
        SpaDictionary.ToDictionary(info->props),
        PipeWireParamInfo.From(info->@params, info->n_params));
}
