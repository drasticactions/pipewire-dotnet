using PipeWire.Native;
using PipeWire.Spa;
using Xunit;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Tests;

public sealed class PipeWireSessionTests
{
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(15);

    private static bool HasSession
    {
        get
        {
            string? directory =
                Environment.GetEnvironmentVariable("PIPEWIRE_RUNTIME_DIR") ??
                Environment.GetEnvironmentVariable("XDG_RUNTIME_DIR");

            return directory is not null && File.Exists(Path.Combine(directory, "pipewire-0"));
        }
    }

    [Fact]
    public async Task APlaybackStreamNegotiatesAFormatAndRunsItsProcessCallback()
    {
        Assert.SkipUnless(HasSession, "No PipeWire session is running for this user.");

        var negotiated = new TaskCompletionSource<spa_audio_info_raw>(TaskCreationOptions.RunContinuationsAsynchronously);
        var streaming = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        int cycles = 0;
        int framesWritten = 0;

        PipeWireLibrary.Init();

        using var loop = new PipeWireThreadLoop("session-tests");
        using var context = new PipeWireContext(loop);
        loop.Start();

        PipeWireCore core;
        PipeWireStream stream;

        using (loop.Lock())
        {
            core = context.Connect();
            stream = new PipeWireStream(core, "PipeWire.NET tests", PipeWireProperties.From(
                "media.type", "Audio",
                "media.category", "Playback",
                "media.role", "Test"));
        }

        try
        {
            stream.ParamChanged += (_, e) =>
            {
                if (e.ParamType == spa_param_type.SPA_PARAM_Format &&
                    SpaAudioFormats.TryParseRaw(e.Param, out spa_audio_info_raw format))
                {
                    negotiated.TrySetResult(format);
                }
            };

            stream.StateChanged += (_, e) =>
            {
                if (e.NewState == pw_stream_state.PW_STREAM_STATE_STREAMING)
                {
                    streaming.TrySetResult();
                }
                else if (e.NewState == pw_stream_state.PW_STREAM_STATE_ERROR)
                {
                    var failure = new InvalidOperationException(e.Error ?? "the stream failed");
                    negotiated.TrySetException(failure);
                    streaming.TrySetException(failure);
                }
            };

            stream.Process = s =>
            {
                PipeWireBuffer buffer = s.DequeueBuffer();
                if (buffer.IsNull)
                {
                    return;
                }

                PipeWireBufferData data = buffer[0];
                Span<byte> memory = data.Memory;
                memory.Clear();

                const int stride = 2 * sizeof(float);
                int frames = memory.Length / stride;
                if (buffer.RequestedFrames > 0)
                {
                    frames = (int)Math.Min((ulong)frames, buffer.RequestedFrames);
                }

                data.SetChunk(0, (uint)(frames * stride), stride);
                Interlocked.Add(ref framesWritten, frames);
                Interlocked.Increment(ref cycles);

                s.QueueBuffer(buffer);
            };

            using (loop.Lock())
            {
                stream.Connect(
                    spa_direction.SPA_DIRECTION_OUTPUT,
                    PW_ID_ANY,
                    pw_stream_flags.PW_STREAM_FLAG_AUTOCONNECT |
                    pw_stream_flags.PW_STREAM_FLAG_MAP_BUFFERS |
                    pw_stream_flags.PW_STREAM_FLAG_RT_PROCESS,
                    SpaAudioFormats.BuildRawChoice(
                        [spa_audio_format.SPA_AUDIO_FORMAT_F32],
                        defaultRate: 48000,
                        defaultChannels: 2));
            }

            spa_audio_info_raw result = await negotiated.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken);
            Assert.Equal(2u, result.channels);
            Assert.NotEqual(0u, result.rate);

            await streaming.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken);

            DateTime deadline = DateTime.UtcNow + Timeout;
            while (Volatile.Read(ref cycles) < 5 && DateTime.UtcNow < deadline)
            {
                await Task.Delay(50, TestContext.Current.CancellationToken);
            }

            Assert.True(Volatile.Read(ref cycles) >= 5, $"The process callback ran {Volatile.Read(ref cycles)} time(s).");
            Assert.True(Volatile.Read(ref framesWritten) > 0, "No frames reached the graph.");
        }
        finally
        {
            loop.Stop();
            stream.Dispose();
            core.Dispose();
        }
    }
}
