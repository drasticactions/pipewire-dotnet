using PipeWire.Native;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public sealed unsafe class PipeWireContext : IDisposable
{
    private readonly PipeWireLoop _loop;
    private pw_context* _context;

    public PipeWireContext(PipeWireLoop loop, PipeWireProperties? properties = null)
    {
        ArgumentNullException.ThrowIfNull(loop);
        PipeWireLibrary.EnsureInitialized();

        _loop = loop;
        _context = PipeWireException.ThrowIfNull(
            pw_context_new(loop.Handle, properties is null ? null : properties.Release(), 0),
            "Could not create the PipeWire context");
    }

    public PipeWireContext(PipeWireThreadLoop loop, PipeWireProperties? properties = null)
        : this((loop ?? throw new ArgumentNullException(nameof(loop))).Loop, properties)
    {
    }

    public PipeWireContext(PipeWireMainLoop loop, PipeWireProperties? properties = null)
        : this((loop ?? throw new ArgumentNullException(nameof(loop))).Loop, properties)
    {
    }

    public pw_context* Handle => _context;

    public PipeWireLoop Loop => _loop;

    public PipeWireProperties GetProperties()
    {
        ObjectDisposedException.ThrowIf(_context is null, this);
        return PipeWireProperties.Copy(pw_context_get_properties(_context));
    }

    public int UpdateProperties(IEnumerable<KeyValuePair<string, string?>> properties)
    {
        ArgumentNullException.ThrowIfNull(properties);
        ObjectDisposedException.ThrowIf(_context is null, this);

        using var dict = new Spa.SpaDictionary(properties);
        return PipeWireException.ThrowIfNegative(
            pw_context_update_properties(_context, dict.Handle),
            "Could not update the context properties");
    }

    public PipeWireCore Connect(PipeWireProperties? properties = null)
    {
        ObjectDisposedException.ThrowIf(_context is null, this);

        pw_core* core = PipeWireException.ThrowIfNull(
            pw_context_connect(_context, properties is null ? null : properties.Release(), 0),
            "Could not connect to PipeWire");

        return new PipeWireCore(this, core);
    }

    public PipeWireCore ConnectFd(int fd, PipeWireProperties? properties = null)
    {
        ObjectDisposedException.ThrowIf(_context is null, this);

        pw_core* core = PipeWireException.ThrowIfNull(
            pw_context_connect_fd(_context, fd, properties is null ? null : properties.Release(), 0),
            "Could not connect to PipeWire over the given descriptor");

        return new PipeWireCore(this, core);
    }

    public void Dispose()
    {
        if (_context is not null)
        {
            pw_context_destroy(_context);
            _context = null;
        }

        GC.SuppressFinalize(this);
    }
}
