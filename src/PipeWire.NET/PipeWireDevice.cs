using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireDevice : PipeWireProxy
{
    private readonly NativeListener<pw_device_events> _events;

    internal PipeWireDevice(pw_proxy* proxy)
        : base(proxy)
    {
        _events = new NativeListener<pw_device_events>(this);
        _events.Events->version = PW_VERSION_DEVICE_EVENTS;
        _events.Events->info = &OnInfo;
        _events.Events->param = &OnParam;

        AddObjectListener(_events);
    }

    public event EventHandler<PipeWireDeviceInfo>? Info;

    public event EventHandler<PipeWireParamEventArgs>? ParamChanged;

    public PipeWireDeviceInfo? DeviceInfo { get; private set; }

    public int EnumParams(
        spa_param_type id,
        uint index = 0,
        uint count = uint.MaxValue,
        ReadOnlySpan<byte> filter = default,
        int sequence = 0)
    {
        pw_device_methods* methods = GetMethods<pw_device_methods>(PW_VERSION_DEVICE_METHODS, "enum_params", out void* data);
        return ParamOperations.EnumParams(methods->enum_params, data, "device", sequence, id, index, count, filter);
    }

    public void SubscribeParams(params spa_param_type[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids);
        pw_device_methods* methods = GetMethods<pw_device_methods>(PW_VERSION_DEVICE_METHODS, "subscribe_params", out void* data);
        ParamOperations.SubscribeParams(methods->subscribe_params, data, "device", ids);
    }

    public void SetParam(spa_param_type id, ReadOnlySpan<byte> param, uint flags = 0)
    {
        pw_device_methods* methods = GetMethods<pw_device_methods>(PW_VERSION_DEVICE_METHODS, "set_param", out void* data);
        ParamOperations.SetParam(methods->set_param, data, "device", id, flags, param);
    }

    private protected override void DisposeObjectListener() => _events.Dispose();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnInfo(void* data, pw_device_info* info)
    {
        PipeWireDevice? device = NativeListener<pw_device_events>.GetOwner<PipeWireDevice>(data);
        if (device is null)
        {
            return;
        }

        var snapshot = PipeWireDeviceInfo.From(info);
        CallbackGuard.Run(() =>
        {
            device.DeviceInfo = snapshot;
            device.Info?.Invoke(device, snapshot);
        });
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnParam(void* data, int seq, uint id, uint index, uint next, spa_pod* param)
    {
        PipeWireDevice? device = NativeListener<pw_device_events>.GetOwner<PipeWireDevice>(data);
        if (device is not null)
        {
            CallbackGuard.Run(() => device.ParamChanged?.Invoke(device, new PipeWireParamEventArgs(seq, id, index, next, param)));
        }
    }
}

public sealed record PipeWireDeviceInfo(
    uint Id,
    IReadOnlyDictionary<string, string?> Properties,
    IReadOnlyList<PipeWireParamInfo> Params)
{
    internal static unsafe PipeWireDeviceInfo From(pw_device_info* info) => new(
        info->id,
        SpaDictionary.ToDictionary(info->props),
        PipeWireParamInfo.From(info->@params, info->n_params));
}
