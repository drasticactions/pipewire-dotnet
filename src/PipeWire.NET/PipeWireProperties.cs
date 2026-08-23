using System.Buffers;
using System.Collections;
using System.Text;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireProperties : IDisposable, IEnumerable<KeyValuePair<string, string?>>
{
    private const int StackKeyLimit = 256;

    private readonly bool _owned;
    private pw_properties* _properties;

    public PipeWireProperties()
    {
        PipeWireLibrary.EnsureInitialized();

        using var empty = new SpaDictionary([]);
        _properties = PipeWireException.ThrowIfNull(pw_properties_new_dict(empty.Handle), "Could not create PipeWire properties");
        _owned = true;
    }

    public PipeWireProperties(IEnumerable<KeyValuePair<string, string?>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);
        PipeWireLibrary.EnsureInitialized();

        using var dict = new SpaDictionary(entries);
        _properties = PipeWireException.ThrowIfNull(pw_properties_new_dict(dict.Handle), "Could not create PipeWire properties");
        _owned = true;
    }

    private PipeWireProperties(pw_properties* properties, bool owned)
    {
        _properties = properties;
        _owned = owned;
    }

    public pw_properties* Handle => _properties;

    public bool IsOwned => _owned;

    public bool IsReadOnly => !_owned;

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
            PipeWireException.ThrowIfNull(pw_properties_new_dict(dict.Handle), "Could not create PipeWire properties"),
            owned: true);
    }

    public static PipeWireProperties FromEntries(params SpaDictionaryEntry[] entries)
    {
        using SpaDictionary dict = SpaDictionary.FromEntries(entries);
        PipeWireLibrary.EnsureInitialized();
        return new PipeWireProperties(
            PipeWireException.ThrowIfNull(pw_properties_new_dict(dict.Handle), "Could not create PipeWire properties"),
            owned: true);
    }

    public static PipeWireProperties Copy(pw_properties* properties)
        => new(PipeWireException.ThrowIfNull(pw_properties_copy(properties), "Could not copy PipeWire properties"), owned: true);

    public static PipeWireProperties Borrow(pw_properties* properties)
        => new(PipeWireException.ThrowIfNull(properties, "The object does not expose any properties"), owned: false);

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

    public string? Get(ReadOnlySpan<byte> key)
    {
        ObjectDisposedException.ThrowIf(_properties is null, this);

        Span<byte> buffer = stackalloc byte[StackKeyLimit];
        scoped Span<byte> terminated = NulTerminate(key, buffer, out byte[]? rented);

        try
        {
            fixed (byte* p = terminated)
            {
                return Utf8.FromNative(pw_properties_get(_properties, (sbyte*)p));
            }
        }
        finally
        {
            Return(rented);
        }
    }

    public uint GetUInt32(string key, uint defaultValue = 0)
        => TryGetUInt32(key, out uint value) ? value : defaultValue;

    public uint GetUInt32(ReadOnlySpan<byte> key, uint defaultValue = 0)
        => TryGetUInt32(key, out uint value) ? value : defaultValue;

    public bool TryGetUInt32(string key, out uint value)
        => Fetch(key, &pw_properties_fetch_uint32, out value);

    public bool TryGetUInt32(ReadOnlySpan<byte> key, out uint value)
        => Fetch(key, &pw_properties_fetch_uint32, out value);

    public int GetInt32(string key, int defaultValue = 0)
        => TryGetInt32(key, out int value) ? value : defaultValue;

    public int GetInt32(ReadOnlySpan<byte> key, int defaultValue = 0)
        => TryGetInt32(key, out int value) ? value : defaultValue;

    public bool TryGetInt32(string key, out int value)
        => Fetch(key, &pw_properties_fetch_int32, out value);

    public bool TryGetInt32(ReadOnlySpan<byte> key, out int value)
        => Fetch(key, &pw_properties_fetch_int32, out value);

    public ulong GetUInt64(string key, ulong defaultValue = 0)
        => TryGetUInt64(key, out ulong value) ? value : defaultValue;

    public ulong GetUInt64(ReadOnlySpan<byte> key, ulong defaultValue = 0)
        => TryGetUInt64(key, out ulong value) ? value : defaultValue;

    public bool TryGetUInt64(string key, out ulong value)
        => Fetch(key, &pw_properties_fetch_uint64, out value);

    public bool TryGetUInt64(ReadOnlySpan<byte> key, out ulong value)
        => Fetch(key, &pw_properties_fetch_uint64, out value);

    public long GetInt64(string key, long defaultValue = 0)
        => TryGetInt64(key, out long value) ? value : defaultValue;

    public long GetInt64(ReadOnlySpan<byte> key, long defaultValue = 0)
        => TryGetInt64(key, out long value) ? value : defaultValue;

    public bool TryGetInt64(string key, out long value)
        => Fetch(key, &pw_properties_fetch_int64, out value);

    public bool TryGetInt64(ReadOnlySpan<byte> key, out long value)
        => Fetch(key, &pw_properties_fetch_int64, out value);

    public bool GetBool(string key, bool defaultValue = false)
        => TryGetBool(key, out bool value) ? value : defaultValue;

    public bool GetBool(ReadOnlySpan<byte> key, bool defaultValue = false)
        => TryGetBool(key, out bool value) ? value : defaultValue;

    public bool TryGetBool(string key, out bool value)
        => Fetch(key, &pw_properties_fetch_bool, out value);

    public bool TryGetBool(ReadOnlySpan<byte> key, out bool value)
        => Fetch(key, &pw_properties_fetch_bool, out value);

    public bool Set(string key, string? value)
    {
        ArgumentNullException.ThrowIfNull(key);
        ObjectDisposedException.ThrowIf(_properties is null, this);
        ThrowIfReadOnly();

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

    public bool Set(ReadOnlySpan<byte> key, string? value)
    {
        ObjectDisposedException.ThrowIf(_properties is null, this);
        ThrowIfReadOnly();

        Span<byte> buffer = stackalloc byte[StackKeyLimit];
        scoped Span<byte> terminated = NulTerminate(key, buffer, out byte[]? rented);
        byte[]? valueUtf8 = Utf8.ToNativeOrNull(value);

        try
        {
            fixed (byte* k = terminated)
            fixed (byte* v = valueUtf8)
            {
                return PipeWireException.ThrowIfNegative(
                    pw_properties_set(_properties, (sbyte*)k, (sbyte*)v),
                    "Could not set the property") > 0;
            }
        }
        finally
        {
            Return(rented);
        }
    }

    public Dictionary<string, string?> ToDictionary()
        => _properties is null ? [] : SpaDictionary.ToDictionary(&_properties->dict);

    public IEnumerator<KeyValuePair<string, string?>> GetEnumerator() => ToDictionary().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Dispose()
    {
        if (_properties is not null && _owned)
        {
            pw_properties_free(_properties);
        }

        _properties = null;
        GC.SuppressFinalize(this);
    }

    internal pw_properties* Release()
    {
        if (!_owned)
        {
            throw new InvalidOperationException("A borrowed property view does not own its handle and cannot transfer ownership.");
        }

        pw_properties* released = _properties;
        _properties = null;
        GC.SuppressFinalize(this);
        return released;
    }

    private static Span<byte> NulTerminate(ReadOnlySpan<byte> key, Span<byte> buffer, out byte[]? rented)
    {
        rented = key.Length + 1 > buffer.Length ? ArrayPool<byte>.Shared.Rent(key.Length + 1) : null;
        Span<byte> target = rented ?? buffer;

        key.CopyTo(target);
        target[key.Length] = 0;
        return target[..(key.Length + 1)];
    }

    private static void Return(byte[]? rented)
    {
        if (rented is not null)
        {
            ArrayPool<byte>.Shared.Return(rented);
        }
    }

    private bool Fetch<T>(string key, delegate*<pw_properties*, sbyte*, T*, int> fetch, out T value)
        where T : unmanaged
    {
        ArgumentNullException.ThrowIfNull(key);
        ObjectDisposedException.ThrowIf(_properties is null, this);

        int max = Encoding.UTF8.GetMaxByteCount(key.Length) + 1;
        byte[]? rented = max > StackKeyLimit ? ArrayPool<byte>.Shared.Rent(max) : null;
        Span<byte> buffer = rented ?? stackalloc byte[StackKeyLimit];

        try
        {
            int written = Encoding.UTF8.GetBytes(key, buffer);
            buffer[written] = 0;
            return FetchCore(buffer[..(written + 1)], fetch, out value);
        }
        finally
        {
            Return(rented);
        }
    }

    private bool Fetch<T>(ReadOnlySpan<byte> key, delegate*<pw_properties*, sbyte*, T*, int> fetch, out T value)
        where T : unmanaged
    {
        ObjectDisposedException.ThrowIf(_properties is null, this);

        Span<byte> buffer = stackalloc byte[StackKeyLimit];
        scoped Span<byte> terminated = NulTerminate(key, buffer, out byte[]? rented);

        try
        {
            return FetchCore(terminated, fetch, out value);
        }
        finally
        {
            Return(rented);
        }
    }

    private bool FetchCore<T>(ReadOnlySpan<byte> key, delegate*<pw_properties*, sbyte*, T*, int> fetch, out T value)
        where T : unmanaged
    {
        T result = default;
        int status;

        fixed (byte* k = key)
        {
            status = fetch(_properties, (sbyte*)k, &result);
        }

        value = status < 0 ? default : result;
        return status >= 0;
    }

    private void ThrowIfReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException(
                "This property view is owned by another object. Use that object's UpdateProperties method to modify it.");
        }
    }
}
