using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireModule : IDisposable
{
    private NativeListener<pw_impl_module_events>? _events;
    private pw_impl_module* _module;

    internal PipeWireModule(pw_impl_module* module)
    {
        _module = module;

        _events = new NativeListener<pw_impl_module_events>(this);
        _events.Events->version = PW_VERSION_IMPL_MODULE_EVENTS;
        _events.Events->destroy = &OnDestroy;

        pw_impl_module_add_listener(_module, _events.Hook, _events.Events, _events.UserData);
    }

    public event EventHandler? Destroyed;

    public pw_impl_module* Handle => _module;

    public bool IsDestroyed => _module is null;

    public uint GlobalId
    {
        get
        {
            ObjectDisposedException.ThrowIf(_module is null, this);
            return pw_impl_module_get_info(_module)->id;
        }
    }

    public string? Name
    {
        get
        {
            ObjectDisposedException.ThrowIf(_module is null, this);
            return Utf8.FromNative(pw_impl_module_get_info(_module)->name);
        }
    }

    public IReadOnlyDictionary<string, string?> Properties
    {
        get
        {
            ObjectDisposedException.ThrowIf(_module is null, this);
            return SpaDictionary.ToDictionary(&pw_impl_module_get_properties(_module)->dict);
        }
    }

    public void Dispose()
    {
        pw_impl_module* module = _module;
        if (module is null)
        {
            return;
        }

        ReleaseListener();
        pw_impl_module_destroy(module);
    }

    private void ReleaseListener()
    {
        _module = null;
        _events?.Dispose();
        _events = null;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnDestroy(void* data)
    {
        PipeWireModule? module = NativeListener<pw_impl_module_events>.GetOwner<PipeWireModule>(data);
        if (module is null)
        {
            return;
        }

        CallbackGuard.Run(() =>
        {
            module.ReleaseListener();
            module.Destroyed?.Invoke(module, EventArgs.Empty);
        });
    }
}
