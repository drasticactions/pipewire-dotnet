using PipeWire.Native;

namespace PipeWire.Spa;

public readonly unsafe struct SpaPodArray
{
    private readonly spa_pod_array* _array;

    internal SpaPodArray(spa_pod_array* array) => _array = array;

    public spa_pod_array* Handle => _array;

    public bool IsNull => _array is null;

    public uint ChildType => _array is null ? 0 : _array->body.child.type;

    public uint ChildSize => _array is null ? 0 : _array->body.child.size;

    public int Count
    {
        get
        {
            if (_array is null || ChildSize == 0)
            {
                return 0;
            }

            uint bodySize = _array->pod.size;
            uint headerSize = (uint)sizeof(spa_pod_array_body);
            return bodySize <= headerSize ? 0 : (int)((bodySize - headerSize) / ChildSize);
        }
    }

    public ReadOnlySpan<T> AsSpan<T>()
        where T : unmanaged
    {
        if (_array is null)
        {
            return default;
        }

        if (sizeof(T) != ChildSize)
        {
            throw new InvalidOperationException($"The array holds {ChildSize}-byte elements, but {typeof(T).Name} is {sizeof(T)} bytes.");
        }

        return new ReadOnlySpan<T>((byte*)_array + sizeof(spa_pod_array), Count);
    }

    public override string ToString()
        => IsNull ? "<null array>" : $"Array({SpaPodTypes.GetName(ChildType)} x {Count})";
}

public readonly unsafe struct SpaPodChoice
{
    private readonly spa_pod_choice* _choice;

    internal SpaPodChoice(spa_pod_choice* choice) => _choice = choice;

    public spa_pod_choice* Handle => _choice;

    public bool IsNull => _choice is null;

    public spa_choice_type ChoiceType => _choice is null ? spa_choice_type.SPA_CHOICE_None : (spa_choice_type)_choice->body.type;

    public uint Flags => _choice is null ? 0 : _choice->body.flags;

    public uint ChildType => _choice is null ? 0 : _choice->body.child.type;

    public uint ChildSize => _choice is null ? 0 : _choice->body.child.size;

    public int Count
    {
        get
        {
            if (_choice is null || ChildSize == 0)
            {
                return 0;
            }

            uint bodySize = _choice->pod.size;
            uint headerSize = (uint)sizeof(spa_pod_choice_body);
            return bodySize <= headerSize ? 0 : (int)((bodySize - headerSize) / ChildSize);
        }
    }

    public SpaPod FirstValue
        => _choice is null || Count == 0 ? default : new SpaPod(&_choice->body.child);

    public ReadOnlySpan<T> AsSpan<T>()
        where T : unmanaged
    {
        if (_choice is null)
        {
            return default;
        }

        if (sizeof(T) != ChildSize)
        {
            throw new InvalidOperationException($"The choice holds {ChildSize}-byte values, but {typeof(T).Name} is {sizeof(T)} bytes.");
        }

        return new ReadOnlySpan<T>((byte*)_choice + sizeof(spa_pod_choice), Count);
    }

    public override string ToString()
        => IsNull ? "<null choice>" : $"{ChoiceType}({SpaPodTypes.GetName(ChildType)} x {Count})";
}
