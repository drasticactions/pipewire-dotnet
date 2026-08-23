using System.Runtime.InteropServices;
using PipeWire.Native;
using PipeWire.Spa;

namespace PipeWire;

internal sealed unsafe class NativeListener<TEvents> : IDisposable
    where TEvents : unmanaged
{
    private GCHandle _owner;
    private spa_hook* _hook;
    private TEvents* _events;

    internal NativeListener(object owner)
    {
        _owner = GCHandle.Alloc(owner);
        _hook = (spa_hook*)NativeMemory.AllocZeroed((nuint)sizeof(spa_hook));
        _events = (TEvents*)NativeMemory.AllocZeroed((nuint)sizeof(TEvents));
    }

    internal spa_hook* Hook => _hook;

    internal TEvents* Events => _events;

    internal void* UserData => (void*)GCHandle.ToIntPtr(_owner);

    internal static TOwner? GetOwner<TOwner>(void* data)
        where TOwner : class
    {
        if (data is null)
        {
            return null;
        }

        GCHandle handle = GCHandle.FromIntPtr((IntPtr)data);
        return handle.IsAllocated ? handle.Target as TOwner : null;
    }

    public void Dispose()
    {
        if (_hook is not null)
        {
            SpaInterface.RemoveHook(_hook);
            NativeMemory.Free(_hook);
            _hook = null;
        }

        if (_events is not null)
        {
            NativeMemory.Free(_events);
            _events = null;
        }

        if (_owner.IsAllocated)
        {
            _owner.Free();
        }
    }
}
