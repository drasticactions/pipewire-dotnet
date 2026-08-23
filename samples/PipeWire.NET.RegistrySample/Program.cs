using PipeWire;
using PipeWire.Native;
using PipeWire.Spa;
using static PipeWire.Native.Pipewire;

bool showParams = args.Contains("--params");

PipeWireLibrary.Init();
Console.WriteLine($"pipewire {PipeWireLibrary.LibraryVersion} (headers {PipeWireLibrary.HeadersVersion})");

using var loop = new PipeWireMainLoop();
using var context = new PipeWireContext(loop);

using PipeWireCore core = context.Connect();
using PipeWireRegistry registry = core.GetRegistry();

var globals = new List<PipeWireGlobal>();
var nodes = new List<PipeWireNode>();
int listingSequence = 0;

int roundsLeft = showParams ? 3 : 1;

core.Error += (_, e) => Console.Error.WriteLine($"error on {e.Id}: {e.Message} ({e.Result})");
core.Info += (_, info) => Console.WriteLine($"connected to {info.Name} as {info.UserName}@{info.HostName}, cookie {info.Cookie}");

registry.GlobalAdded += (_, global) =>
{
    globals.Add(global);

    if (showParams && global.IsNode)
    {
        PipeWireNode node = registry.BindNode(global.Id);
        nodes.Add(node);

        node.ParamChanged += (_, p) => PrintParam(global.Id, p);
        node.Info += (_, info) =>
        {
            foreach (PipeWireParamInfo param in info.Params)
            {
                if (!param.CanRead)
                {
                    continue;
                }

                if (param.Id is spa_param_type.SPA_PARAM_EnumFormat or spa_param_type.SPA_PARAM_Props)
                {
                    node.EnumParams(param.Id);
                }
            }
        };
    }
};

core.Done += (_, done) =>
{
    if (done.Sequence != listingSequence)
    {
        return;
    }

    if (--roundsLeft > 0)
    {
        listingSequence = core.Sync();
        return;
    }

    Print(globals);
    loop.Quit();
};

listingSequence = core.Sync();
loop.Run();

foreach (PipeWireNode node in nodes)
{
    node.Dispose();
}

static void Print(List<PipeWireGlobal> globals)
{
    Console.WriteLine();
    Console.WriteLine($"{globals.Count} objects in the graph");

    foreach (IGrouping<string, PipeWireGlobal> group in globals.GroupBy(g => g.Type).OrderBy(g => g.Key))
    {
        Console.WriteLine();
        Console.WriteLine($"== {group.Key} ({group.Count()}) ==");

        foreach (PipeWireGlobal global in group.OrderBy(g => g.Id))
        {
            string? name =
                global.GetProperty(Describe(PW_KEY_NODE_DESCRIPTION)) ??
                global.GetProperty(Describe(PW_KEY_NODE_NAME)) ??
                global.GetProperty(Describe(PW_KEY_PORT_NAME)) ??
                global.GetProperty(Describe(PW_KEY_DEVICE_NAME)) ??
                global.GetProperty(Describe(PW_KEY_APP_NAME)) ??
                global.GetProperty(Describe(PW_KEY_MODULE_NAME)) ??
                global.GetProperty(Describe(PW_KEY_FACTORY_NAME));

            Console.WriteLine($"  {global.Id,5}  {name}");

            string? mediaClass = global.GetProperty(Describe(PW_KEY_MEDIA_CLASS));
            if (mediaClass is not null)
            {
                Console.WriteLine($"         media.class = {mediaClass}");
            }

            if (global.IsLink)
            {
                Console.WriteLine(
                    $"         {global.GetProperty(Describe(PW_KEY_LINK_OUTPUT_NODE))}:{global.GetProperty(Describe(PW_KEY_LINK_OUTPUT_PORT))}" +
                    $" -> {global.GetProperty(Describe(PW_KEY_LINK_INPUT_NODE))}:{global.GetProperty(Describe(PW_KEY_LINK_INPUT_PORT))}");
            }
        }
    }
}

static void PrintParam(uint nodeId, PipeWireParamEventArgs args)
{
    SpaPod pod = args.Param;
    if (pod.IsNull || pod.Type != SPA_TYPE_Object)
    {
        return;
    }

    if (args.ParamType == spa_param_type.SPA_PARAM_EnumFormat &&
        SpaAudioFormats.TryParseRaw(pod, out spa_audio_info_raw info))
    {
        Console.WriteLine($"  node {nodeId}: format {info.format}, {info.rate} Hz, {info.channels} channels");
        return;
    }

    SpaPodObject obj = pod.AsObject();
    var parts = new List<string>();
    foreach (SpaPodProperty property in obj)
    {
        SpaPod value = property.Value.Value;
        string text = value.Type switch
        {
            SPA_TYPE_Bool => value.AsBool().ToString(),
            SPA_TYPE_Id => value.AsId().ToString(),
            SPA_TYPE_Int => value.AsInt().ToString(),
            SPA_TYPE_Long => value.AsLong().ToString(),
            SPA_TYPE_Float => value.AsFloat().ToString("0.###"),
            SPA_TYPE_Double => value.AsDouble().ToString("0.###"),
            SPA_TYPE_String => value.AsString(),
            SPA_TYPE_Array => value.ToString(),
            _ => value.ToString(),
        };

        parts.Add($"{property.Key}={text}");
    }

    if (parts.Count > 0)
    {
        Console.WriteLine($"  node {nodeId}: {args.ParamType} {string.Join(", ", parts)}");
    }
}

static string Describe(ReadOnlySpan<byte> key) => System.Text.Encoding.UTF8.GetString(key);
