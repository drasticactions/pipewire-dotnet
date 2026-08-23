using PipeWire.Native;
using PipeWire.Spa;
using Xunit;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Tests;

[Collection(PipeWireDaemonCollection.Name)]
public sealed class PipeWireIntegrationTests(PipeWireDaemonFixture daemon) : IDisposable
{
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(15);

    private readonly PipeWireDaemonFixture _daemon = daemon;

    public void Dispose()
    {
    }

    [Fact]
    public async Task ConnectingReportsTheServerInfo()
    {
        _daemon.SkipIfUnavailable();

        using var session = new Session(_daemon);
        var info = new TaskCompletionSource<PipeWireCoreInfo>(TaskCreationOptions.RunContinuationsAsynchronously);

        using (session.Loop.Lock())
        {
            if (session.Core.ServerInfo is { } alreadyReceived)
            {
                info.TrySetResult(alreadyReceived);
            }
            else
            {
                session.Core.Info += (_, i) => info.TrySetResult(i);
            }
        }

        PipeWireCoreInfo received = await info.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken);

        Assert.NotNull(received.Version);
        Assert.NotEmpty(received.Version);
        Assert.NotEqual(0u, received.Cookie);
    }

    [Fact]
    public async Task SyncComesBackThroughDone()
    {
        _daemon.SkipIfUnavailable();

        using var session = new Session(_daemon);
        var done = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
        int sequence = 0;

        session.Core.Done += (_, d) =>
        {
            if (d.Sequence == sequence)
            {
                done.TrySetResult(d.Sequence);
            }
        };

        using (session.Loop.Lock())
        {
            sequence = session.Core.Sync();
        }

        Assert.Equal(sequence, await done.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task TheRegistryListsTheTestSink()
    {
        _daemon.SkipIfUnavailable();

        using var session = new Session(_daemon);
        IReadOnlyList<PipeWireGlobal> globals = await session.ListGlobalsAsync();

        Assert.NotEmpty(globals);
        Assert.Contains(globals, g => g.Type == PipeWireInterfaces.Core);

        PipeWireGlobal? sink = globals.FirstOrDefault(IsTestSink);
        Assert.NotNull(sink);
        Assert.True(sink!.IsNode);
        Assert.Equal("Audio/Sink", sink.GetProperty("media.class"));
    }

    [Fact]
    public async Task BindingANodeReportsItsInfoAndParams()
    {
        _daemon.SkipIfUnavailable();

        using var session = new Session(_daemon);
        IReadOnlyList<PipeWireGlobal> globals = await session.ListGlobalsAsync();
        PipeWireGlobal sink = globals.First(IsTestSink);

        var info = new TaskCompletionSource<PipeWireNodeInfo>(TaskCreationOptions.RunContinuationsAsynchronously);
        var format = new TaskCompletionSource<spa_audio_info_raw>(TaskCreationOptions.RunContinuationsAsynchronously);

        PipeWireNode node;
        using (session.Loop.Lock())
        {
            node = session.Registry.BindNode(sink.Id);
            node.Info += (_, i) => info.TrySetResult(i);
            node.ParamChanged += (_, p) =>
            {
                if (p.ParamType == spa_param_type.SPA_PARAM_EnumFormat &&
                    SpaAudioFormats.TryParseRaw(p.Param, out spa_audio_info_raw parsed))
                {
                    format.TrySetResult(parsed);
                }
            };
        }

        try
        {
            PipeWireNodeInfo received = await info.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken);

            Assert.Equal(sink.Id, received.Id);
            Assert.NotEmpty(received.Properties);
            Assert.Contains(received.Params, p => p.Id == spa_param_type.SPA_PARAM_EnumFormat);

            using (session.Loop.Lock())
            {
                node.EnumParams(spa_param_type.SPA_PARAM_EnumFormat);
            }

            spa_audio_info_raw negotiable = await format.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken);
            Assert.NotEqual(0u, negotiable.channels);
        }
        finally
        {
            using (session.Loop.Lock())
            {
                node.Dispose();
            }
        }
    }

    [Fact]
    public async Task RemovingAGlobalIsReported()
    {
        _daemon.SkipIfUnavailable();

        using var session = new Session(_daemon);
        await session.ListGlobalsAsync();

        var removed = new TaskCompletionSource<uint>(TaskCreationOptions.RunContinuationsAsynchronously);
        session.Registry.GlobalRemoved += (_, id) => removed.TrySetResult(id);

        PipeWireStream stream;
        using (session.Loop.Lock())
        {
            stream = new PipeWireStream(session.Core, "transient");
            stream.Connect(
                spa_direction.SPA_DIRECTION_OUTPUT,
                PW_ID_ANY,
                pw_stream_flags.PW_STREAM_FLAG_MAP_BUFFERS,
                SpaAudioFormats.BuildRaw(spa_audio_format.SPA_AUDIO_FORMAT_F32, 48000, 2));
        }

        await Task.Delay(500, TestContext.Current.CancellationToken);

        using (session.Loop.Lock())
        {
            stream.Dispose();
        }

        Assert.True(await removed.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken) > 0);
    }

    [Fact]
    public async Task AStreamConnectsAndJoinsTheGraph()
    {
        _daemon.SkipIfUnavailable();

        using var session = new Session(_daemon);
        await session.ListGlobalsAsync();

        var connected = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        PipeWireStream stream;
        using (session.Loop.Lock())
        {
            stream = new PipeWireStream(session.Core, "PipeWire.NET test tone");
        }

        try
        {
            stream.StateChanged += (_, e) =>
            {
                if (e.NewState is pw_stream_state.PW_STREAM_STATE_PAUSED or pw_stream_state.PW_STREAM_STATE_STREAMING)
                {
                    connected.TrySetResult();
                }
                else if (e.NewState == pw_stream_state.PW_STREAM_STATE_ERROR)
                {
                    connected.TrySetException(new InvalidOperationException(e.Error ?? "the stream failed"));
                }
            };

            using (session.Loop.Lock())
            {
                stream.Connect(
                    spa_direction.SPA_DIRECTION_OUTPUT,
                    PW_ID_ANY,
                    pw_stream_flags.PW_STREAM_FLAG_MAP_BUFFERS | pw_stream_flags.PW_STREAM_FLAG_RT_PROCESS,
                    SpaAudioFormats.BuildRawChoice([spa_audio_format.SPA_AUDIO_FORMAT_F32], defaultRate: 48000, defaultChannels: 2));
            }

            await connected.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken);

            uint streamNodeId = stream.NodeId;
            Assert.NotEqual(PW_ID_ANY, streamNodeId);

            IReadOnlyList<PipeWireGlobal> globals = await session.ListGlobalsAsync();
            Assert.Contains(globals, g => g.IsNode && g.Id == streamNodeId);
        }
        finally
        {
            using (session.Loop.Lock())
            {
                stream.Dispose();
            }
        }
    }

    [Fact]
    public async Task PropertiesSurviveTheRoundTripToTheDaemon()
    {
        _daemon.SkipIfUnavailable();

        using var session = new Session(_daemon);
        using PipeWireProperties properties = session.Core.CopyProperties();

        Assert.NotEqual(0, properties.Count);
        Assert.NotNull(properties["application.name"]);
    }

    [Fact]
    public async Task AConnectedStreamExposesItsObjectSerial()
    {
        _daemon.SkipIfUnavailable();

        using var session = new Session(_daemon);
        var ready = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        PipeWireStream stream;

        using (session.Loop.Lock())
        {
            stream = new PipeWireStream(session.Core, "serial-test", PipeWireProperties.FromEntries(
                new(PW_KEY_MEDIA_TYPE, "Audio"),
                new(PW_KEY_MEDIA_CATEGORY, "Playback"),
                new(PW_KEY_MEDIA_ROLE, "Music")));

            stream.StateChanged += (_, e) =>
            {
                if (e.NewState is pw_stream_state.PW_STREAM_STATE_PAUSED or pw_stream_state.PW_STREAM_STATE_STREAMING)
                {
                    ready.TrySetResult();
                }

                if (e.NewState == pw_stream_state.PW_STREAM_STATE_ERROR)
                {
                    ready.TrySetException(new InvalidOperationException(e.Error));
                }
            };

            stream.Connect(
                spa_direction.SPA_DIRECTION_OUTPUT,
                PW_ID_ANY,
                pw_stream_flags.PW_STREAM_FLAG_AUTOCONNECT | pw_stream_flags.PW_STREAM_FLAG_MAP_BUFFERS,
                SpaAudioFormats.BuildRaw(spa_audio_format.SPA_AUDIO_FORMAT_F32, 48000, 2));
        }

        try
        {
            await ready.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken);

            using (session.Loop.Lock())
            {
                Assert.NotEqual(0ul, stream.ObjectSerial);
                Assert.Equal(stream.ObjectSerial, stream.Properties.GetUInt64(PW_KEY_OBJECT_SERIAL));
                Assert.Equal("Playback", stream.Properties.Get(PW_KEY_MEDIA_CATEGORY));
                Assert.False(stream.Properties.IsOwned);
            }
        }
        finally
        {
            using (session.Loop.Lock())
            {
                stream.Dispose();
            }
        }
    }

    private static bool IsTestSink(PipeWireGlobal global)
        => global.IsNode && global.GetProperty("node.name") == PipeWireDaemonFixture.SinkName;

    private sealed class Session : IDisposable
    {
        private readonly PipeWireThreadLoop _loop;
        private readonly PipeWireContext _context;
        private readonly PipeWireCore _core;
        private readonly PipeWireRegistry _registry;
        private readonly List<PipeWireGlobal> _globals = [];

        internal Session(PipeWireDaemonFixture daemon)
        {
            PipeWireLibrary.Init();

            _loop = new PipeWireThreadLoop("tests");
            _context = new PipeWireContext(_loop);
            _loop.Start();

            using (_loop.Lock())
            {
                _core = _context.Connect(daemon.CreateConnectionProperties());
                _registry = _core.GetRegistry();

                _registry.GlobalAdded += (_, global) =>
                {
                    lock (_globals)
                    {
                        _globals.Add(global);
                    }
                };
            }
        }

        internal PipeWireThreadLoop Loop => _loop;

        internal PipeWireCore Core => _core;

        internal PipeWireRegistry Registry => _registry;

        internal async Task<IReadOnlyList<PipeWireGlobal>> ListGlobalsAsync()
        {
            var listed = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            int sequence = 0;

            using (_loop.Lock())
            {
                _core.Done += (_, done) =>
                {
                    if (done.Sequence == sequence)
                    {
                        listed.TrySetResult();
                    }
                };

                sequence = _core.Sync();
            }

            await listed.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken);

            lock (_globals)
            {
                return _globals.ToArray();
            }
        }

        public void Dispose()
        {
            _loop.Stop();

            _registry.Dispose();
            _core.Dispose();
            _context.Dispose();
            _loop.Dispose();
        }
    }
}
