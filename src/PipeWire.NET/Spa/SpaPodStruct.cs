using PipeWire.Native;

namespace PipeWire.Spa;

public readonly unsafe struct SpaPodStruct
{
    private readonly spa_pod* _pod;

    internal SpaPodStruct(spa_pod* pod) => _pod = pod;

    public spa_pod* Handle => _pod;

    public bool IsNull => _pod is null;

    public Enumerator GetEnumerator() => new(_pod);

    public List<SpaPod> ToList()
    {
        List<SpaPod> fields = [];
        foreach (SpaPod field in this)
        {
            fields.Add(field);
        }

        return fields;
    }

    public override string ToString()
        => IsNull ? "<null struct>" : $"Struct({_pod->size} bytes)";

    public struct Enumerator
    {
        private readonly spa_pod* _pod;
        private readonly uint _bodySize;
        private uint _offset;
        private uint _next;

        internal Enumerator(spa_pod* pod)
        {
            _pod = pod;
            _bodySize = pod is null ? 0 : pod->size;
        }

        public readonly SpaPod Current
            => new((spa_pod*)((byte*)_pod + SpaPodBuilder.PodHeaderSize + _offset), _bodySize - _offset);

        public bool MoveNext()
        {
            if (_pod is null)
            {
                return false;
            }

            const uint headerSize = SpaPodBuilder.PodHeaderSize;
            if (_next + headerSize > _bodySize)
            {
                return false;
            }

            spa_pod* field = (spa_pod*)((byte*)_pod + headerSize + _next);
            if (_bodySize - _next - headerSize < field->size)
            {
                return false;
            }

            _offset = _next;
            _next += SpaPodTypes.RoundUpToAlignment(headerSize + field->size);
            return true;
        }
    }
}

public readonly unsafe struct SpaPodSequence
{
    private readonly spa_pod_sequence* _sequence;

    internal SpaPodSequence(spa_pod_sequence* sequence) => _sequence = sequence;

    public spa_pod_sequence* Handle => _sequence;

    public bool IsNull => _sequence is null;

    public uint Unit => _sequence is null ? 0 : _sequence->body.unit;

    public Enumerator GetEnumerator() => new(_sequence);

    public override string ToString()
        => IsNull ? "<null sequence>" : $"Sequence({_sequence->pod.size} bytes)";

    public struct Enumerator
    {
        private readonly spa_pod_sequence* _sequence;
        private readonly uint _bodySize;
        private uint _offset;
        private uint _next;

        internal Enumerator(spa_pod_sequence* sequence)
        {
            _sequence = sequence;
            _bodySize = sequence is null ? 0 : sequence->pod.size;

            _next = (uint)sizeof(spa_pod_sequence_body);
        }

        public readonly SpaPodControl Current
            => new((spa_pod_control*)((byte*)&_sequence->body + _offset));

        public bool MoveNext()
        {
            if (_sequence is null)
            {
                return false;
            }

            uint headerSize = (uint)sizeof(spa_pod_control);
            if (_next + headerSize > _bodySize)
            {
                return false;
            }

            spa_pod_control* control = (spa_pod_control*)((byte*)&_sequence->body + _next);
            if (_bodySize - _next - headerSize < control->value.size)
            {
                return false;
            }

            _offset = _next;
            _next += SpaPodTypes.RoundUpToAlignment(headerSize + control->value.size);
            return true;
        }
    }
}

public readonly unsafe struct SpaPodControl
{
    private readonly spa_pod_control* _control;

    internal SpaPodControl(spa_pod_control* control) => _control = control;

    public spa_pod_control* Handle => _control;

    public bool IsNull => _control is null;

    public uint Offset => _control is null ? 0 : _control->offset;

    public spa_control_type ControlType => _control is null ? 0 : (spa_control_type)_control->type;

    public SpaPod Value => _control is null ? default : new SpaPod(&_control->value);

    public override string ToString()
        => IsNull ? "<null control>" : $"{ControlType}@{Offset} = {Value}";
}
