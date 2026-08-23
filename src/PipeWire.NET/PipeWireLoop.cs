using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireLoop
{
    private readonly pw_loop* _loop;

    internal PipeWireLoop(pw_loop* loop) => _loop = loop;

    public pw_loop* Handle => _loop;

    public int Fd
    {
        get
        {
            spa_loop_control_methods* methods = Control("get_fd", out void* data);
            return methods->get_fd is null
                ? throw new NotSupportedException("The loop does not implement get_fd.")
                : PipeWireException.ThrowIfNegative(methods->get_fd(data), "Could not get the loop file descriptor");
        }
    }

    public int Iterate(int timeoutMilliseconds = 0)
    {
        spa_loop_control_methods* methods = Control("iterate", out void* data);
        return methods->iterate is null
            ? throw new NotSupportedException("The loop does not implement iterate.")
            : PipeWireException.ThrowIfNegative(methods->iterate(data, timeoutMilliseconds), "Could not iterate the loop");
    }

    public void Enter()
    {
        spa_loop_control_methods* methods = Control("enter", out void* data);
        if (methods->enter is not null)
        {
            methods->enter(data);
        }
    }

    public void Leave()
    {
        spa_loop_control_methods* methods = Control("leave", out void* data);
        if (methods->leave is not null)
        {
            methods->leave(data);
        }
    }

    public bool IsCallerOnLoop
    {
        get
        {
            spa_loop_control_methods* methods = Control("check", out void* data);
            return methods->check is not null && methods->check(data) > 0;
        }
    }

    public void SetName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        byte[] utf8 = Utf8.ToNative(name);
        fixed (byte* p = utf8)
        {
            PipeWireException.ThrowIfNegative(pw_loop_set_name(_loop, (sbyte*)p), "Could not set the loop name");
        }
    }

    private spa_loop_control_methods* Control(string method, out void* data)
    {
        if (_loop is null || _loop->control is null)
        {
            throw new InvalidOperationException("The loop has no control interface.");
        }

        return SpaInterface.GetMethods<spa_loop_control_methods>((spa_interface*)_loop->control, 0, method, out data);
    }
}
