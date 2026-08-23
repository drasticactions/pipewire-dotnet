using PipeWire;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

double frequency = args.Length > 0 && double.TryParse(args[0], out double hz) ? hz : 440;
double seconds = args.Length > 1 && double.TryParse(args[1], out double parsed) ? parsed : 3;

PipeWireLibrary.Init();

using var loop = new PipeWireThreadLoop("playback");
using var context = new PipeWireContext(loop);
using PipeWireCore core = context.Connect();

using PipeWireProperties properties = PipeWireProperties.From(
    Key(PW_KEY_MEDIA_TYPE), "Audio",
    Key(PW_KEY_MEDIA_CATEGORY), "Playback",
    Key(PW_KEY_MEDIA_ROLE), "Music",
    Key(PW_KEY_NODE_NAME), "PipeWire.NET tone");

using var stream = new PipeWireStream(core, "PipeWire.NET tone", properties);

var format = new spa_audio_info_raw { format = spa_audio_format.SPA_AUDIO_FORMAT_F32, rate = 48000, channels = 2 };
var formatReady = new TaskCompletionSource();

var state = new ToneState { Frequency = frequency };

stream.StateChanged += (_, e) =>
{
    Console.WriteLine($"state: {e.OldState} -> {e.NewState}{(e.Error is null ? "" : $" ({e.Error})")}");
    if (e.NewState == pw_stream_state.PW_STREAM_STATE_ERROR)
    {
        formatReady.TrySetException(new InvalidOperationException(e.Error ?? "the stream failed"));
    }
};

stream.ParamChanged += (_, e) =>
{
    if (e.ParamType != spa_param_type.SPA_PARAM_Format || !SpaAudioFormats.TryParseRaw(e.Param, out spa_audio_info_raw negotiated))
    {
        return;
    }

    format = negotiated;
    state.Rate = negotiated.rate;
    state.Channels = (int)negotiated.channels;
    Console.WriteLine($"negotiated {format.format}, {format.rate} Hz, {format.channels} channels");
    formatReady.TrySetResult();
};

stream.Process = s =>
{
    PipeWireBuffer buffer = s.DequeueBuffer();
    if (buffer.IsNull)
    {
        return;
    }

    PipeWireBufferData data = buffer[0];
    Span<float> samples = System.Runtime.InteropServices.MemoryMarshal.Cast<byte, float>(data.Memory);

    int channels = state.Channels;
    int frameCount = samples.Length / channels;

    if (buffer.RequestedFrames > 0)
    {
        frameCount = (int)Math.Min((ulong)frameCount, buffer.RequestedFrames);
    }

    double step = 2 * Math.PI * state.Frequency / state.Rate;
    double phase = state.Phase;

    for (int frame = 0; frame < frameCount; frame++)
    {
        float value = (float)(Math.Sin(phase) * 0.2);
        phase += step;

        for (int channel = 0; channel < channels; channel++)
        {
            samples[(frame * channels) + channel] = value;
        }
    }

    state.Phase = phase % (2 * Math.PI);

    int stride = channels * sizeof(float);
    data.SetChunk(0, (uint)(frameCount * stride), stride);
    s.QueueBuffer(buffer);
};

byte[] formats = SpaAudioFormats.BuildRaw(
    spa_audio_format.SPA_AUDIO_FORMAT_F32,
    format.rate,
    format.channels);

using (loop.Lock())
{
    stream.Connect(
        spa_direction.SPA_DIRECTION_OUTPUT,
        PW_ID_ANY,
        pw_stream_flags.PW_STREAM_FLAG_AUTOCONNECT |
        pw_stream_flags.PW_STREAM_FLAG_MAP_BUFFERS |
        pw_stream_flags.PW_STREAM_FLAG_RT_PROCESS,
        formats);
}

loop.Start();

await formatReady.Task.WaitAsync(TimeSpan.FromSeconds(10));

Console.WriteLine($"playing {frequency:0.#} Hz for {seconds:0.#}s");
await Task.Delay(TimeSpan.FromSeconds(seconds));

using (loop.Lock())
{
    stream.Disconnect();
}

loop.Stop();
Console.WriteLine("done");

static string Key(ReadOnlySpan<byte> key) => System.Text.Encoding.UTF8.GetString(key);

internal sealed class ToneState
{
    public double Frequency { get; init; } = 440;

    public double Phase { get; set; }

    public uint Rate { get; set; } = 48000;

    public int Channels { get; set; } = 2;
}
