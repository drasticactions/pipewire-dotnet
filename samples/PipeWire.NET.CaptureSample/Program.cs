using System.Buffers.Binary;
using PipeWire;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

string path = args.Length > 0 ? args[0] : "capture.wav";
double seconds = args.Length > 1 && double.TryParse(args[1], out double parsed) ? parsed : 5;

PipeWireLibrary.Init();

var ring = new PipeWireRingBuffer(1 << 22);

using var loop = new PipeWireThreadLoop("capture");
using var context = new PipeWireContext(loop);
using PipeWireCore core = context.Connect();

using PipeWireProperties properties = PipeWireProperties.From(
    Key(PW_KEY_MEDIA_TYPE), "Audio",
    Key(PW_KEY_MEDIA_CATEGORY), "Capture",
    Key(PW_KEY_MEDIA_ROLE), "Music",
    Key(PW_KEY_NODE_NAME), "PipeWire.NET capture");

using var stream = new PipeWireStream(core, "PipeWire.NET capture", properties);

spa_audio_info_raw format = default;
var formatReady = new TaskCompletionSource();

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

    ReadOnlySpan<byte> samples = buffer[0].Chunked;
    if (!samples.IsEmpty)
    {
        ring.Write(samples);
    }

    s.QueueBuffer(buffer);
};

byte[] formats = SpaAudioFormats.BuildRawChoice(
    [spa_audio_format.SPA_AUDIO_FORMAT_S16_LE, spa_audio_format.SPA_AUDIO_FORMAT_F32_LE],
    defaultRate: 48000,
    defaultChannels: 2);

using (loop.Lock())
{
    stream.Connect(
        spa_direction.SPA_DIRECTION_INPUT,
        PW_ID_ANY,
        pw_stream_flags.PW_STREAM_FLAG_AUTOCONNECT |
        pw_stream_flags.PW_STREAM_FLAG_MAP_BUFFERS |
        pw_stream_flags.PW_STREAM_FLAG_RT_PROCESS,
        formats);
}

loop.Start();

await formatReady.Task.WaitAsync(TimeSpan.FromSeconds(10));

int sampleSize = SpaAudioFormats.GetSampleSize(format.format);
int frameSize = sampleSize * (int)format.channels;
long targetBytes = (long)(seconds * format.rate) * frameSize;

Console.WriteLine($"recording {seconds:0.#}s to {path}");

using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
{
    WriteWavHeader(file, format, sampleSize, 0);

    byte[] chunk = new byte[64 * 1024];
    long written = 0;

    while (written < targetBytes)
    {
        int read = ring.Read(chunk);
        if (read == 0)
        {
            await Task.Delay(5);
            continue;
        }

        int toWrite = (int)Math.Min(read, targetBytes - written);
        file.Write(chunk, 0, toWrite);
        written += toWrite;
    }

    file.Flush();
    file.Seek(0, SeekOrigin.Begin);
    WriteWavHeader(file, format, sampleSize, written);
}

using (loop.Lock())
{
    stream.Disconnect();
}

loop.Stop();

if (ring.DroppedBytes > 0)
{
    Console.Error.WriteLine($"dropped {ring.DroppedBytes} bytes: the writer could not keep up");
}

Console.WriteLine($"wrote {path}");

static string Key(ReadOnlySpan<byte> key) => System.Text.Encoding.UTF8.GetString(key);

static void WriteWavHeader(Stream stream, spa_audio_info_raw format, int sampleSize, long dataBytes)
{
    bool isFloat = format.format is spa_audio_format.SPA_AUDIO_FORMAT_F32_LE or spa_audio_format.SPA_AUDIO_FORMAT_F32;
    int channels = (int)format.channels;
    int rate = (int)format.rate;
    int blockAlign = sampleSize * channels;

    Span<byte> header = stackalloc byte[44];
    "RIFF"u8.CopyTo(header);
    BinaryPrimitives.WriteUInt32LittleEndian(header[4..], (uint)(36 + dataBytes));
    "WAVE"u8.CopyTo(header[8..]);
    "fmt "u8.CopyTo(header[12..]);
    BinaryPrimitives.WriteUInt32LittleEndian(header[16..], 16);
    BinaryPrimitives.WriteUInt16LittleEndian(header[20..], (ushort)(isFloat ? 3 : 1));
    BinaryPrimitives.WriteUInt16LittleEndian(header[22..], (ushort)channels);
    BinaryPrimitives.WriteUInt32LittleEndian(header[24..], (uint)rate);
    BinaryPrimitives.WriteUInt32LittleEndian(header[28..], (uint)(rate * blockAlign));
    BinaryPrimitives.WriteUInt16LittleEndian(header[32..], (ushort)blockAlign);
    BinaryPrimitives.WriteUInt16LittleEndian(header[34..], (ushort)(sampleSize * 8));
    "data"u8.CopyTo(header[36..]);
    BinaryPrimitives.WriteUInt32LittleEndian(header[40..], (uint)dataBytes);

    stream.Write(header);
}
