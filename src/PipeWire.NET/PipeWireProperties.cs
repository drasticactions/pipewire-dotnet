using System.Collections;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireProperties : IDisposable, IEnumerable<KeyValuePair<string, string?>>
{
    private pw_properties* _properties;

    public PipeWireProperties()
    {
        PipeWireLibrary.EnsureInitialized();

        using var empty = new SpaDictionary([]);
        _properties = PipeWireException.ThrowIfNull(pw_properties_new_dict(empty.Handle), "Could not create PipeWire properties");
    }

    public PipeWireProperties(IEnumerable<KeyValuePair<string, string?>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);
        PipeWireLibrary.EnsureInitialized();

        using var dict = new SpaDictionary(entries);
        _properties = PipeWireException.ThrowIfNull(pw_properties_new_dict(dict.Handle), "Could not create PipeWire properties");
    }

    private PipeWireProperties(pw_properties* properties) => _properties = properties;

    public pw_properties* Handle => _properties;

    public int Count => _properties is null ? 0 : (int)_properties->dict.n_items;

    public string? this[string key]
    {
        get => Get(key);
        set => Set(key, value);
    }

    public static PipeWireProperties From(params string?[] keysAndValues)
    {
        using SpaDictionary dict = SpaDictionary.From(keysAndValues);
        PipeWireLibrary.EnsureInitialized();
        return new PipeWireProperties(
            PipeWireException.ThrowIfNull(pw_properties_new_dict(dict.Handle), "Could not create PipeWire properties"));
    }

    public static PipeWireProperties Copy(pw_properties* properties)
        => new(PipeWireException.ThrowIfNull(pw_properties_copy(properties), "Could not copy PipeWire properties"));

    public string? Get(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        ObjectDisposedException.ThrowIf(_properties is null, this);

        byte[] utf8 = Utf8.ToNative(key);
        fixed (byte* p = utf8)
        {
            return Utf8.FromNative(pw_properties_get(_properties, (sbyte*)p));
        }
    }

    public bool Set(string key, string? value)
    {
        ArgumentNullException.ThrowIfNull(key);
        ObjectDisposedException.ThrowIf(_properties is null, this);

        byte[] keyUtf8 = Utf8.ToNative(key);
        byte[]? valueUtf8 = Utf8.ToNativeOrNull(value);

        fixed (byte* k = keyUtf8)
        fixed (byte* v = valueUtf8)
        {
            return PipeWireException.ThrowIfNegative(
                pw_properties_set(_properties, (sbyte*)k, (sbyte*)v),
                $"Could not set the property '{key}'") > 0;
        }
    }

    public Dictionary<string, string?> ToDictionary()
        => _properties is null ? [] : SpaDictionary.ToDictionary(&_properties->dict);

    public IEnumerator<KeyValuePair<string, string?>> GetEnumerator() => ToDictionary().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Dispose()
    {
        if (_properties is not null)
        {
            pw_properties_free(_properties);
            _properties = null;
        }

        GC.SuppressFinalize(this);
    }

    internal pw_properties* Release()
    {
        pw_properties* released = _properties;
        _properties = null;
        GC.SuppressFinalize(this);
        return released;
    }
}
