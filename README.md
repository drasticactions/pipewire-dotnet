# pipewire-dotnet

.NET bindings for [PipeWire](https://pipewire.org).

```sh
dotnet add package pipewire-dotnet
```

## Sample Code

```csharp
using PipeWire;
using PipeWire.Native;
using PipeWire.Spa;

PipeWireLibrary.Init();

using var loop = new PipeWireThreadLoop("tone");
using var context = new PipeWireContext(loop);
loop.Start();

PipeWireCore core;
PipeWireStream stream;
using (loop.Lock())
{
    core = context.Connect();
    stream = new PipeWireStream(core, "tone");
}

double phase = 0;
stream.Process = s =>
{
    PipeWireBuffer buffer = s.DequeueBuffer();
    if (buffer.IsNull)
    {
        return;
    }

    PipeWireBufferData data = buffer[0];
    Span<float> samples = MemoryMarshal.Cast<byte, float>(data.Memory);
    int frames = (int)Math.Min((ulong)(samples.Length / 2), buffer.RequestedFrames);

    for (int i = 0; i < frames; i++)
    {
        float v = (float)(Math.Sin(phase) * 0.2);
        phase += 2 * Math.PI * 440 / 48000;
        samples[i * 2] = samples[(i * 2) + 1] = v;
    }

    data.SetChunk(0, (uint)(frames * 8), 8);
    s.QueueBuffer(buffer);
};

using (loop.Lock())
{
    stream.Connect(
        spa_direction.SPA_DIRECTION_OUTPUT,
        Pipewire.PW_ID_ANY,
        pw_stream_flags.PW_STREAM_FLAG_AUTOCONNECT |
        pw_stream_flags.PW_STREAM_FLAG_MAP_BUFFERS |
        pw_stream_flags.PW_STREAM_FLAG_RT_PROCESS,
        SpaAudioFormats.BuildRaw(spa_audio_format.SPA_AUDIO_FORMAT_F32, 48000, 2));
}

await Task.Delay(3000);

loop.Stop();
stream.Dispose();
core.Dispose();
```

## Samples

```sh
dotnet run --project samples/PipeWire.NET.RegistrySample            # the graph, pw-dump style
dotnet run --project samples/PipeWire.NET.RegistrySample -- --params
dotnet run --project samples/PipeWire.NET.CaptureSample -- out.wav 5
dotnet run --project samples/PipeWire.NET.PlaybackSample -- 440 3
```

## Building

```sh
git submodule update --init
dotnet build
dotnet test
```
