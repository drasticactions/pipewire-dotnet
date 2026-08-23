namespace PipeWire.Spa;

public readonly struct SpaPodFrame : IDisposable
{
    private readonly SpaPodBuilder? _builder;
    private readonly int _depth;

    internal SpaPodFrame(SpaPodBuilder builder, int depth)
    {
        _builder = builder;
        _depth = depth;
    }

    public void Dispose() => _builder?.Pop(_depth);
}
