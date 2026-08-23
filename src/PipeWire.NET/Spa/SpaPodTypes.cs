using static PipeWire.Native.Pipewire;

namespace PipeWire.Spa;

public static class SpaPodTypes
{
    public static uint RoundUpToAlignment(uint size)
        => (size + SpaPodBuilder.PodAlign - 1) & ~((uint)SpaPodBuilder.PodAlign - 1);

    public static string GetName(uint type) => type switch
    {
        SPA_TYPE_None => "None",
        SPA_TYPE_Bool => "Bool",
        SPA_TYPE_Id => "Id",
        SPA_TYPE_Int => "Int",
        SPA_TYPE_Long => "Long",
        SPA_TYPE_Float => "Float",
        SPA_TYPE_Double => "Double",
        SPA_TYPE_String => "String",
        SPA_TYPE_Bytes => "Bytes",
        SPA_TYPE_Rectangle => "Rectangle",
        SPA_TYPE_Fraction => "Fraction",
        SPA_TYPE_Bitmap => "Bitmap",
        SPA_TYPE_Array => "Array",
        SPA_TYPE_Struct => "Struct",
        SPA_TYPE_Object => "Object",
        SPA_TYPE_Sequence => "Sequence",
        SPA_TYPE_Pointer => "Pointer",
        SPA_TYPE_Fd => "Fd",
        SPA_TYPE_Choice => "Choice",
        SPA_TYPE_Pod => "Pod",
        _ => $"Type({type})",
    };
}
