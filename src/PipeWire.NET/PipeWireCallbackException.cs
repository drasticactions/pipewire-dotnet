using System.Runtime.ExceptionServices;

namespace PipeWire;

public sealed class PipeWireCallbackExceptionEventArgs(Exception exception) : EventArgs
{
    public Exception Exception { get; } = exception;

    public bool Handled { get; set; }
}

internal static class CallbackGuard
{
    internal static void Run(Action body)
    {
        try
        {
            body();
        }
        catch (Exception ex)
        {
            Report(ex);
        }
    }

    internal static void Report(Exception exception)
    {
        try
        {
            if (PipeWireLibrary.RaiseUnhandledCallbackException(exception))
            {
                return;
            }
        }
        catch
        {
        }

        ExceptionDispatchInfo edi = ExceptionDispatchInfo.Capture(exception);
        ThreadPool.UnsafeQueueUserWorkItem(static state => ((ExceptionDispatchInfo)state!).Throw(), edi);
    }
}
