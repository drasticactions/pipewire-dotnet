using PipeWire.Native;
using PipeWire.Spa;

namespace PipeWire;

public sealed unsafe class PipeWireParamEventArgs : EventArgs
{
    private readonly spa_pod* _param;

    internal PipeWireParamEventArgs(int sequence, uint id, uint index, uint next, spa_pod* param)
    {
        Sequence = sequence;
        ParamType = (spa_param_type)id;
        Index = index;
        Next = next;
        _param = param;
    }

    public int Sequence { get; }

    public spa_param_type ParamType { get; }

    public uint Index { get; }

    public uint Next { get; }

    public SpaPod Param => new(_param);
}

internal static unsafe class ParamOperations
{
    internal static int EnumParams(
        delegate* unmanaged[Cdecl]<void*, int, uint, uint, uint, spa_pod*, int> enumParams,
        void* data,
        string what,
        int sequence,
        spa_param_type id,
        uint index,
        uint count,
        ReadOnlySpan<byte> filter)
    {
        if (enumParams is null)
        {
            throw new NotSupportedException($"The {what} does not implement enum_params.");
        }

        fixed (byte* f = filter)
        {
            return PipeWireException.ThrowIfNegative(
                enumParams(data, sequence, (uint)id, index, count, (spa_pod*)f),
                $"Could not enumerate the {id} param of the {what}");
        }
    }

    internal static void SubscribeParams(
        delegate* unmanaged[Cdecl]<void*, uint*, uint, int> subscribeParams,
        void* data,
        string what,
        ReadOnlySpan<spa_param_type> ids)
    {
        if (subscribeParams is null)
        {
            throw new NotSupportedException($"The {what} does not implement subscribe_params.");
        }

        Span<uint> raw = ids.Length <= 16 ? stackalloc uint[ids.Length] : new uint[ids.Length];
        for (int i = 0; i < ids.Length; i++)
        {
            raw[i] = (uint)ids[i];
        }

        fixed (uint* p = raw)
        {
            PipeWireException.ThrowIfNegative(
                subscribeParams(data, p, (uint)raw.Length),
                $"Could not subscribe to the params of the {what}");
        }
    }

    internal static void SetParam(
        delegate* unmanaged[Cdecl]<void*, uint, uint, spa_pod*, int> setParam,
        void* data,
        string what,
        spa_param_type id,
        uint flags,
        ReadOnlySpan<byte> param)
    {
        if (setParam is null)
        {
            throw new NotSupportedException($"The {what} does not implement set_param.");
        }

        fixed (byte* p = param)
        {
            PipeWireException.ThrowIfNegative(
                setParam(data, (uint)id, flags, (spa_pod*)p),
                $"Could not set the {id} param of the {what}");
        }
    }
}
