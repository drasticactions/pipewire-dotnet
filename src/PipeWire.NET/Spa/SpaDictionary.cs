using System.Runtime.InteropServices;
using System.Text;
using PipeWire.Native;

namespace PipeWire.Spa;

public sealed unsafe class SpaDictionary : IDisposable
{
    private readonly int _count;
    private spa_dict* _dict;

    public SpaDictionary(IEnumerable<KeyValuePair<string, string?>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        KeyValuePair<string, string?>[] items = [.. entries];
        _count = items.Length;

        int stringBytes = 0;
        foreach ((string key, string? value) in items)
        {
            ArgumentNullException.ThrowIfNull(key);
            stringBytes += Encoding.UTF8.GetByteCount(key) + 1 + Encoding.UTF8.GetByteCount(value ?? string.Empty) + 1;
        }

        nuint size = (nuint)(sizeof(spa_dict) + (sizeof(spa_dict_item) * _count) + stringBytes);
        _dict = (spa_dict*)NativeMemory.AllocZeroed(size);

        spa_dict_item* itemArray = (spa_dict_item*)((byte*)_dict + sizeof(spa_dict));
        byte* strings = (byte*)itemArray + (sizeof(spa_dict_item) * _count);

        for (int i = 0; i < items.Length; i++)
        {
            itemArray[i].key = (sbyte*)strings;
            strings += WriteUtf8(items[i].Key, strings);
            itemArray[i].value = (sbyte*)strings;
            strings += WriteUtf8(items[i].Value ?? string.Empty, strings);
        }

        _dict->flags = 0;
        _dict->n_items = (uint)_count;
        _dict->items = itemArray;
    }

    ~SpaDictionary() => Dispose();

    public int Count => _count;

    public spa_dict* Handle => _dict;

    public static SpaDictionary From(IEnumerable<KeyValuePair<string, string?>> entries) => new(entries);

    public static SpaDictionary From(params string?[] keysAndValues)
    {
        ArgumentNullException.ThrowIfNull(keysAndValues);

        if (keysAndValues.Length % 2 != 0)
        {
            throw new ArgumentException("Keys and values must be given in pairs.", nameof(keysAndValues));
        }

        var entries = new List<KeyValuePair<string, string?>>(keysAndValues.Length / 2);
        for (int i = 0; i < keysAndValues.Length; i += 2)
        {
            entries.Add(new KeyValuePair<string, string?>(
                keysAndValues[i] ?? throw new ArgumentException("A key cannot be null.", nameof(keysAndValues)),
                keysAndValues[i + 1]));
        }

        return new SpaDictionary(entries);
    }

    public static Dictionary<string, string?> ToDictionary(spa_dict* dict)
    {
        var result = new Dictionary<string, string?>(StringComparer.Ordinal);
        if (dict is null || dict->items is null)
        {
            return result;
        }

        for (uint i = 0; i < dict->n_items; i++)
        {
            string? key = Marshal.PtrToStringUTF8((IntPtr)dict->items[i].key);
            if (key is not null)
            {
                result[key] = Marshal.PtrToStringUTF8((IntPtr)dict->items[i].value);
            }
        }

        return result;
    }

    public void Dispose()
    {
        if (_dict is not null)
        {
            NativeMemory.Free(_dict);
            _dict = null;
        }

        GC.SuppressFinalize(this);
    }

    private static int WriteUtf8(string value, byte* destination)
    {
        int length = Encoding.UTF8.GetBytes(value, new Span<byte>(destination, Encoding.UTF8.GetByteCount(value)));
        destination[length] = 0;
        return length + 1;
    }
}
