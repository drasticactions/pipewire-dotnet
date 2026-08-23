using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using PipeWire.Native;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireMetadata : PipeWireProxy
{
    private readonly NativeListener<pw_metadata_events> _events;

    internal PipeWireMetadata(pw_proxy* proxy)
        : base(proxy)
    {
        _events = new NativeListener<pw_metadata_events>(this);
        _events.Events->version = PW_VERSION_METADATA_EVENTS;
        _events.Events->property = &OnProperty;

        AddObjectListener(_events);
    }

    public event EventHandler<PipeWireMetadataProperty>? PropertyChanged;

    public void SetProperty(uint subject, string? key, string? type, string? value)
    {
        pw_metadata_methods* methods = GetMethods<pw_metadata_methods>(PW_VERSION_METADATA_METHODS, "set_property", out void* data);

        if (methods->set_property is null)
        {
            throw new NotSupportedException("The metadata object does not implement set_property.");
        }

        byte[]? keyUtf8 = Utf8.ToNativeOrNull(key);
        byte[]? typeUtf8 = Utf8.ToNativeOrNull(type);
        byte[]? valueUtf8 = Utf8.ToNativeOrNull(value);

        fixed (byte* k = keyUtf8)
        fixed (byte* t = typeUtf8)
        fixed (byte* v = valueUtf8)
        {
            PipeWireException.ThrowIfNegative(
                methods->set_property(data, subject, (sbyte*)k, (sbyte*)t, (sbyte*)v),
                $"Could not set the metadata property '{key}'");
        }
    }

    public void Clear()
    {
        pw_metadata_methods* methods = GetMethods<pw_metadata_methods>(PW_VERSION_METADATA_METHODS, "clear", out void* data);

        if (methods->clear is null)
        {
            throw new NotSupportedException("The metadata object does not implement clear.");
        }

        PipeWireException.ThrowIfNegative(methods->clear(data), "Could not clear the metadata");
    }

    private protected override void DisposeObjectListener() => _events.Dispose();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static int OnProperty(void* data, uint subject, sbyte* key, sbyte* type, sbyte* value)
    {
        PipeWireMetadata? metadata = NativeListener<pw_metadata_events>.GetOwner<PipeWireMetadata>(data);
        if (metadata is null)
        {
            return 0;
        }

        var property = new PipeWireMetadataProperty(
            subject,
            Utf8.FromNative(key),
            Utf8.FromNative(type),
            Utf8.FromNative(value));

        CallbackGuard.Run(() => metadata.PropertyChanged?.Invoke(metadata, property));
        return 0;
    }
}

public readonly record struct PipeWireMetadataProperty(uint Subject, string? Key, string? Type, string? Value);
