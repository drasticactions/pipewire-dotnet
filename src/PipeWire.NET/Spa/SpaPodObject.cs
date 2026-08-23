using PipeWire.Native;

namespace PipeWire.Spa;

public readonly unsafe struct SpaPodObject
{
    private readonly spa_pod_object* _object;

    internal SpaPodObject(spa_pod_object* obj) => _object = obj;

    public spa_pod_object* Handle => _object;

    public bool IsNull => _object is null;

    public uint ObjectType => _object is null ? 0 : _object->body.type;

    public uint Id => _object is null ? 0 : _object->body.id;

    public SpaPod this[uint key]
        => TryGetValue(key, out SpaPod value) ? value : throw new KeyNotFoundException($"The object has no property with key {key}.");

    public bool TryGetValue(uint key, out SpaPod value)
    {
        if (TryGetProperty(key, out SpaPodProperty property))
        {
            value = property.Value.Value;
            return true;
        }

        value = default;
        return false;
    }

    public bool TryGetProperty(uint key, out SpaPodProperty property)
    {
        foreach (SpaPodProperty candidate in this)
        {
            if (candidate.Key == key)
            {
                property = candidate;
                return true;
            }
        }

        property = default;
        return false;
    }

    public Enumerator GetEnumerator() => new(_object);

    public override string ToString()
        => IsNull ? "<null object>" : $"Object(type {ObjectType}, id {Id})";

    public struct Enumerator
    {
        private readonly spa_pod_object* _object;
        private readonly uint _bodySize;
        private uint _offset;
        private uint _next;

        internal Enumerator(spa_pod_object* obj)
        {
            _object = obj;
            _bodySize = obj is null ? 0 : obj->pod.size;

            _next = (uint)sizeof(spa_pod_object_body);
        }

        public readonly SpaPodProperty Current
            => new((spa_pod_prop*)((byte*)&_object->body + _offset));

        public bool MoveNext()
        {
            if (_object is null)
            {
                return false;
            }

            uint headerSize = (uint)sizeof(spa_pod_prop);
            if (_next + headerSize > _bodySize)
            {
                return false;
            }

            spa_pod_prop* prop = (spa_pod_prop*)((byte*)&_object->body + _next);
            if (_bodySize - _next - headerSize < prop->value.size)
            {
                return false;
            }

            _offset = _next;
            _next += SpaPodTypes.RoundUpToAlignment(headerSize + prop->value.size);
            return true;
        }
    }
}

public readonly unsafe struct SpaPodProperty
{
    private readonly spa_pod_prop* _prop;

    internal SpaPodProperty(spa_pod_prop* prop) => _prop = prop;

    public spa_pod_prop* Handle => _prop;

    public bool IsNull => _prop is null;

    public uint Key => _prop is null ? 0 : _prop->key;

    public uint Flags => _prop is null ? 0 : _prop->flags;

    public SpaPod Value => _prop is null ? default : new SpaPod(&_prop->value);

    public override string ToString()
        => IsNull ? "<null property>" : $"{Key} = {Value}";
}
