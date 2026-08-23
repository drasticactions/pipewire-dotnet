using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public unsafe class PipeWireProxy : IDisposable
{
    private readonly NativeListener<pw_proxy_events> _listener;
    private pw_proxy* _proxy;

    public PipeWireProxy(pw_proxy* proxy)
    {
        _proxy = proxy is null ? throw new ArgumentNullException(nameof(proxy)) : proxy;

        _listener = new NativeListener<pw_proxy_events>(this);
        _listener.Events->version = PW_VERSION_PROXY_EVENTS;
        _listener.Events->destroy = &OnDestroy;
        _listener.Events->bound = &OnBound;
        _listener.Events->removed = &OnRemoved;
        _listener.Events->error = &OnError;

        pw_proxy_add_listener(_proxy, _listener.Hook, _listener.Events, _listener.UserData);
    }

    public event EventHandler? Destroyed;

    public event EventHandler<uint>? Bound;

    public event EventHandler? Removed;

    public event EventHandler<PipeWireErrorEventArgs>? Error;

    public pw_proxy* Handle => _proxy;

    public bool IsDisposed => _proxy is null;

    public uint Id => _proxy is null ? PW_ID_ANY : pw_proxy_get_id(_proxy);

    public uint BoundId => _proxy is null ? PW_ID_ANY : pw_proxy_get_bound_id(_proxy);

    public string? TypeName
    {
        get
        {
            if (_proxy is null)
            {
                return null;
            }

            uint version;
            return Utf8.FromNative(pw_proxy_get_type(_proxy, &version));
        }
    }

    public uint Version
    {
        get
        {
            if (_proxy is null)
            {
                return 0;
            }

            uint version;
            _ = pw_proxy_get_type(_proxy, &version);
            return version;
        }
    }

    public int Sync(int sequence = 0)
    {
        ObjectDisposedException.ThrowIf(_proxy is null, this);
        return PipeWireException.ThrowIfNegative(pw_proxy_sync(_proxy, sequence), "Could not sync the proxy");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private protected void AddObjectListener<TEvents>(NativeListener<TEvents> listener)
        where TEvents : unmanaged
    {
        ObjectDisposedException.ThrowIf(_proxy is null, this);
        pw_proxy_add_object_listener(_proxy, listener.Hook, listener.Events, listener.UserData);
    }

    private protected TMethods* GetMethods<TMethods>(uint minVersion, string method, out void* data)
        where TMethods : unmanaged
    {
        ObjectDisposedException.ThrowIf(_proxy is null, this);
        return SpaInterface.GetMethods<TMethods>((spa_interface*)_proxy, minVersion, method, out data);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_proxy is null)
        {
            return;
        }

        pw_proxy* proxy = _proxy;
        _proxy = null;

        DisposeObjectListener();
        _listener.Dispose();
        pw_proxy_destroy(proxy);
    }

    private protected virtual void DisposeObjectListener()
    {
    }

    private void OnProxyGone()
    {
        _proxy = null;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnDestroy(void* data)
    {
        PipeWireProxy? proxy = NativeListener<pw_proxy_events>.GetOwner<PipeWireProxy>(data);
        if (proxy is null)
        {
            return;
        }

        CallbackGuard.Run(() =>
        {
            proxy.Destroyed?.Invoke(proxy, EventArgs.Empty);

            proxy.OnProxyGone();
        });
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnBound(void* data, uint globalId)
    {
        PipeWireProxy? proxy = NativeListener<pw_proxy_events>.GetOwner<PipeWireProxy>(data);
        if (proxy is not null)
        {
            CallbackGuard.Run(() => proxy.Bound?.Invoke(proxy, globalId));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnRemoved(void* data)
    {
        PipeWireProxy? proxy = NativeListener<pw_proxy_events>.GetOwner<PipeWireProxy>(data);
        if (proxy is not null)
        {
            CallbackGuard.Run(() => proxy.Removed?.Invoke(proxy, EventArgs.Empty));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnError(void* data, int seq, int res, sbyte* message)
    {
        PipeWireProxy? proxy = NativeListener<pw_proxy_events>.GetOwner<PipeWireProxy>(data);
        if (proxy is null)
        {
            return;
        }

        string? text = Utf8.FromNative(message);
        CallbackGuard.Run(() => proxy.Error?.Invoke(proxy, new PipeWireErrorEventArgs(proxy.Id, seq, res, text)));
    }
}
