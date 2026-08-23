#pragma warning disable CS1591

using System;
using System.Diagnostics;

namespace PipeWire.Native;

[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
[Conditional("DEBUG")]
internal sealed partial class NativeTypeNameAttribute : Attribute
{
    private readonly string _name;

    public NativeTypeNameAttribute(string name)
    {
        _name = name;
    }

    public string Name => _name;
}
