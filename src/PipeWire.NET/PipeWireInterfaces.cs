using System.Text;
using static PipeWire.Native.Pipewire;

namespace PipeWire;

public static class PipeWireInterfaces
{
    public const string Core = "PipeWire:Interface:Core";

    public const string Registry = "PipeWire:Interface:Registry";

    public const string Node = "PipeWire:Interface:Node";

    public const string Port = "PipeWire:Interface:Port";

    public const string Link = "PipeWire:Interface:Link";

    public const string Device = "PipeWire:Interface:Device";

    public const string Client = "PipeWire:Interface:Client";

    public const string Module = "PipeWire:Interface:Module";

    public const string Factory = "PipeWire:Interface:Factory";

    public const string Metadata = "PipeWire:Interface:Metadata";

    public static string ToManaged(ReadOnlySpan<byte> utf8) => Encoding.UTF8.GetString(utf8);
}
