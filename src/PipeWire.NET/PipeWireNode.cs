using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireNode : PipeWireProxy
{
    private readonly NativeListener<pw_node_events> _events;

    internal PipeWireNode(pw_proxy* proxy)
        : base(proxy)
    {
        _events = new NativeListener<pw_node_events>(this);
        _events.Events->version = PW_VERSION_NODE_EVENTS;
        _events.Events->info = &OnInfo;
        _events.Events->param = &OnParam;

        AddObjectListener(_events);
    }

    public event EventHandler<PipeWireNodeInfo>? Info;

    public event EventHandler<PipeWireParamEventArgs>? ParamChanged;

    public PipeWireNodeInfo? NodeInfo { get; private set; }

    public int EnumParams(
        spa_param_type id,
        uint index = 0,
        uint count = uint.MaxValue,
        ReadOnlySpan<byte> filter = default,
        int sequence = 0)
    {
        pw_node_methods* methods = GetMethods<pw_node_methods>(PW_VERSION_NODE_METHODS, "enum_params", out void* data);
        return ParamOperations.EnumParams(methods->enum_params, data, "node", sequence, id, index, count, filter);
    }

    public void SubscribeParams(params spa_param_type[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids);
        pw_node_methods* methods = GetMethods<pw_node_methods>(PW_VERSION_NODE_METHODS, "subscribe_params", out void* data);
        ParamOperations.SubscribeParams(methods->subscribe_params, data, "node", ids);
    }

    public void SetParam(spa_param_type id, ReadOnlySpan<byte> param, uint flags = 0)
    {
        pw_node_methods* methods = GetMethods<pw_node_methods>(PW_VERSION_NODE_METHODS, "set_param", out void* data);
        ParamOperations.SetParam(methods->set_param, data, "node", id, flags, param);
    }

    public void SendCommand(ReadOnlySpan<byte> command)
    {
        pw_node_methods* methods = GetMethods<pw_node_methods>(PW_VERSION_NODE_METHODS, "send_command", out void* data);

        if (methods->send_command is null)
        {
            throw new NotSupportedException("The node does not implement send_command.");
        }

        fixed (byte* c = command)
        {
            PipeWireException.ThrowIfNegative(methods->send_command(data, (spa_command*)c), "Could not send a command to the node");
        }
    }

    private protected override void DisposeObjectListener() => _events.Dispose();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnInfo(void* data, pw_node_info* info)
    {
        PipeWireNode? node = NativeListener<pw_node_events>.GetOwner<PipeWireNode>(data);
        if (node is null)
        {
            return;
        }

        var snapshot = PipeWireNodeInfo.From(info);
        CallbackGuard.Run(() =>
        {
            node.NodeInfo = snapshot;
            node.Info?.Invoke(node, snapshot);
        });
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnParam(void* data, int seq, uint id, uint index, uint next, spa_pod* param)
    {
        PipeWireNode? node = NativeListener<pw_node_events>.GetOwner<PipeWireNode>(data);
        if (node is not null)
        {
            CallbackGuard.Run(() => node.ParamChanged?.Invoke(node, new PipeWireParamEventArgs(seq, id, index, next, param)));
        }
    }
}

public sealed record PipeWireNodeInfo(
    uint Id,
    uint MaxInputPorts,
    uint MaxOutputPorts,
    uint InputPortCount,
    uint OutputPortCount,
    pw_node_state State,
    string? Error,
    IReadOnlyDictionary<string, string?> Properties,
    IReadOnlyList<PipeWireParamInfo> Params)
{
    internal static unsafe PipeWireNodeInfo From(pw_node_info* info) => new(
        info->id,
        info->max_input_ports,
        info->max_output_ports,
        info->n_input_ports,
        info->n_output_ports,
        info->state,
        Utf8.FromNative(info->error),
        SpaDictionary.ToDictionary(info->props),
        PipeWireParamInfo.From(info->@params, info->n_params));
}

public readonly record struct PipeWireParamInfo(spa_param_type Id, uint Flags)
{
    public bool CanRead => (Flags & SPA_PARAM_INFO_READ) != 0;

    public bool CanWrite => (Flags & SPA_PARAM_INFO_WRITE) != 0;

    internal static unsafe IReadOnlyList<PipeWireParamInfo> From(spa_param_info* infos, uint count)
    {
        if (infos is null || count == 0)
        {
            return [];
        }

        var result = new PipeWireParamInfo[count];
        for (uint i = 0; i < count; i++)
        {
            result[i] = new PipeWireParamInfo((spa_param_type)infos[i].id, infos[i].flags);
        }

        return result;
    }
}
