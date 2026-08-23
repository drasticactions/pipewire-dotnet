using System.Text;

namespace PipeWire.Spa;

public readonly struct SpaDictionaryEntry
{
    private readonly byte[]? _key;
    private readonly byte[]? _value;

    public SpaDictionaryEntry(string key, string? value)
    {
        ArgumentNullException.ThrowIfNull(key);

        _key = Encoding.UTF8.GetBytes(key);
        _value = value is null ? null : Encoding.UTF8.GetBytes(value);
    }

    public SpaDictionaryEntry(ReadOnlySpan<byte> key, string? value)
    {
        _key = Trim(key).ToArray();
        _value = value is null ? null : Encoding.UTF8.GetBytes(value);
    }

    public ReadOnlySpan<byte> KeyUtf8 => _key;

    public ReadOnlySpan<byte> ValueUtf8 => _value;

    public string Key => _key is null ? string.Empty : Encoding.UTF8.GetString(_key);

    public string? Value => _value is null ? null : Encoding.UTF8.GetString(_value);

    public static implicit operator SpaDictionaryEntry(KeyValuePair<string, string?> pair)
        => new(pair.Key, pair.Value);

    public KeyValuePair<string, string?> ToKeyValuePair() => new(Key, Value);

    public override string ToString() => $"{Key}={Value}";

    private static ReadOnlySpan<byte> Trim(ReadOnlySpan<byte> utf8)
        => utf8.Length > 0 && utf8[^1] == 0 ? utf8[..^1] : utf8;
}
