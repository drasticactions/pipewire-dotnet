using System.Diagnostics;
using Xunit;

namespace PipeWire.Tests;

public sealed class PipeWireDaemonFixture : IDisposable
{
    public const string SinkName = "pipewire-dotnet-test-sink";

    private readonly string _runtimeDirectory;
    private readonly string _configPath;
    private readonly string _socketPath;
    private Process? _daemon;

    public PipeWireDaemonFixture()
    {
        _runtimeDirectory = Path.Combine(Path.GetTempPath(), $"pipewire-dotnet-tests-{Environment.ProcessId}");
        _configPath = Path.Combine(_runtimeDirectory, "pipewire-test.conf");
        _socketPath = Path.Combine(_runtimeDirectory, "pipewire-0");

        try
        {
            Directory.CreateDirectory(_runtimeDirectory);
            File.WriteAllText(_configPath, Configuration);

            _daemon = StartDaemon();
            IsAvailable = _daemon is not null && WaitForSocket(TimeSpan.FromSeconds(10));

            if (!IsAvailable)
            {
                SkipReason = _daemon is null
                    ? "The 'pipewire' binary is not installed."
                    : $"The test daemon did not create a socket in {_runtimeDirectory}.";
            }
        }
        catch (Exception ex)
        {
            IsAvailable = false;
            SkipReason = $"The test daemon could not be started: {ex.Message}";
        }
    }

    public bool IsAvailable { get; }

    public string SocketPath => _socketPath;

    public PipeWireProperties CreateConnectionProperties()
        => PipeWireProperties.From("remote.name", _socketPath);

    public string SkipReason { get; } = string.Empty;

    public void SkipIfUnavailable() => Assert.SkipUnless(IsAvailable, SkipReason);

    public void Dispose()
    {
        if (_daemon is not null)
        {
            try
            {
                if (!_daemon.HasExited)
                {
                    _daemon.Kill(entireProcessTree: true);
                    _daemon.WaitForExit(5000);
                }
            }
            catch (InvalidOperationException)
            {
            }

            _daemon.Dispose();
            _daemon = null;
        }

        try
        {
            Directory.Delete(_runtimeDirectory, recursive: true);
        }
        catch (IOException)
        {
        }
    }

    private static string Configuration => $$"""
        context.properties = {
            core.daemon = true
            core.name   = pipewire-0
            link.max-buffers = 16
            default.clock.rate = 48000
            default.clock.quantum = 1024
        }

        context.spa-libs = {
            audio.convert.* = audioconvert/libspa-audioconvert
            support.*       = support/libspa-support
        }

        context.modules = [
            { name = libpipewire-module-access }
            { name = libpipewire-module-protocol-native }
            { name = libpipewire-module-client-node }
            { name = libpipewire-module-adapter }
            { name = libpipewire-module-link-factory }
            { name = libpipewire-module-metadata }
            { name = libpipewire-module-spa-node-factory }
        ]

        context.objects = [
            { factory = spa-node-factory
              args = {
                  factory.name    = support.node.driver
                  node.name       = Dummy-Driver
                  priority.driver = 20000
              }
            }
            { factory = adapter
              args = {
                  factory.name     = support.null-audio-sink
                  node.name        = {{SinkName}}
                  media.class      = Audio/Sink
                  object.linger    = true
                  audio.position   = "[FL,FR]"
                  monitor.channel-volumes = true
              }
            }
        ]
        """;

    private Process? StartDaemon()
    {
        var startInfo = new ProcessStartInfo("pipewire")
        {
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
        };

        startInfo.ArgumentList.Add("-c");
        startInfo.ArgumentList.Add(_configPath);
        startInfo.Environment["PIPEWIRE_RUNTIME_DIR"] = _runtimeDirectory;

        startInfo.Environment["PIPEWIRE_LOG_LEVEL"] = "1";

        try
        {
            return Process.Start(startInfo);
        }
        catch (System.ComponentModel.Win32Exception)
        {
            return null;
        }
    }

    private bool WaitForSocket(TimeSpan timeout)
    {
        string socket = _socketPath;
        DateTime deadline = DateTime.UtcNow + timeout;

        while (DateTime.UtcNow < deadline)
        {
            if (File.Exists(socket))
            {
                return true;
            }

            if (_daemon?.HasExited == true)
            {
                return false;
            }

            Thread.Sleep(50);
        }

        return false;
    }
}

[CollectionDefinition(Name)]
public sealed class PipeWireDaemonCollection : ICollectionFixture<PipeWireDaemonFixture>
{
    public const string Name = "PipeWire daemon";
}
