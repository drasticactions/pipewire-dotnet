using PipeWire.Native;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Spa;

public static unsafe class SpaVideoFormats
{
    public const uint ModifierPropertyFlags = SPA_POD_PROP_FLAG_MANDATORY | SPA_POD_PROP_FLAG_DONT_FIXATE;

    public static byte[] BuildRaw(
        spa_video_format format,
        (uint Width, uint Height) size,
        (uint Num, uint Denom) framerate = default,
        ulong? modifier = null,
        spa_param_type paramType = spa_param_type.SPA_PARAM_EnumFormat)
    {
        using var builder = new SpaPodBuilder(256);

        using (builder.PushObject(SPA_TYPE_OBJECT_Format, (uint)paramType))
        {
            AddMediaType(builder);

            builder.AddProperty((uint)spa_format.SPA_FORMAT_VIDEO_format);
            builder.AddId((uint)format);

            if (modifier.HasValue)
            {
                builder.AddProperty((uint)spa_format.SPA_FORMAT_VIDEO_modifier, SPA_POD_PROP_FLAG_MANDATORY);
                builder.AddLong(unchecked((long)modifier.Value));
            }

            builder.AddProperty((uint)spa_format.SPA_FORMAT_VIDEO_size);
            builder.AddRectangle(size.Width, size.Height);

            if (framerate.Denom != 0)
            {
                builder.AddProperty((uint)spa_format.SPA_FORMAT_VIDEO_framerate);
                builder.AddFraction(framerate.Num, framerate.Denom);
            }
        }

        return builder.ToArray();
    }

    public static byte[] BuildRawChoice(
        ReadOnlySpan<spa_video_format> formats,
        (uint Width, uint Height) defaultSize,
        (uint Width, uint Height) minSize = default,
        (uint Width, uint Height) maxSize = default,
        (uint Num, uint Denom) defaultFramerate = default,
        (uint Num, uint Denom) minFramerate = default,
        (uint Num, uint Denom) maxFramerate = default,
        spa_param_type paramType = spa_param_type.SPA_PARAM_EnumFormat)
        => Build(formats, default, dontFixate: false, defaultSize, minSize, maxSize, defaultFramerate, minFramerate, maxFramerate, paramType);

    public static byte[] BuildDmaBuf(
        ReadOnlySpan<spa_video_format> formats,
        ReadOnlySpan<ulong> modifiers,
        (uint Width, uint Height) defaultSize,
        (uint Width, uint Height) minSize = default,
        (uint Width, uint Height) maxSize = default,
        (uint Num, uint Denom) defaultFramerate = default,
        (uint Num, uint Denom) minFramerate = default,
        (uint Num, uint Denom) maxFramerate = default,
        bool dontFixate = true,
        spa_param_type paramType = spa_param_type.SPA_PARAM_EnumFormat)
    {
        if (modifiers.IsEmpty)
        {
            throw new ArgumentException("At least one modifier must be offered.", nameof(modifiers));
        }

        return Build(formats, modifiers, dontFixate, defaultSize, minSize, maxSize, defaultFramerate, minFramerate, maxFramerate, paramType);
    }

    public static byte[][] BuildDmaBufAndFallback(
        ReadOnlySpan<spa_video_format> formats,
        ReadOnlySpan<ulong> modifiers,
        (uint Width, uint Height) defaultSize,
        (uint Width, uint Height) minSize = default,
        (uint Width, uint Height) maxSize = default,
        (uint Num, uint Denom) defaultFramerate = default,
        (uint Num, uint Denom) minFramerate = default,
        (uint Num, uint Denom) maxFramerate = default,
        bool dontFixate = true,
        spa_param_type paramType = spa_param_type.SPA_PARAM_EnumFormat)
        =>
        [
            BuildDmaBuf(formats, modifiers, defaultSize, minSize, maxSize, defaultFramerate, minFramerate, maxFramerate, dontFixate, paramType),
            BuildRawChoice(formats, defaultSize, minSize, maxSize, defaultFramerate, minFramerate, maxFramerate, paramType),
        ];

    public static bool TryParseRaw(SpaPod pod, out spa_video_info_raw info)
    {
        info = default;

        if (pod.IsNull || pod.Type != SPA_TYPE_Object)
        {
            return false;
        }

        SpaPodObject obj = pod.AsObject();
        if (obj.ObjectType != SPA_TYPE_OBJECT_Format)
        {
            return false;
        }

        if (!obj.TryGetValue((uint)spa_format.SPA_FORMAT_mediaType, out SpaPod mediaType) ||
            !mediaType.Value.TryGetId(out uint mediaTypeId) ||
            mediaTypeId != (uint)spa_media_type.SPA_MEDIA_TYPE_video)
        {
            return false;
        }

        if (!obj.TryGetValue((uint)spa_format.SPA_FORMAT_mediaSubtype, out SpaPod subtype) ||
            !subtype.Value.TryGetId(out uint subtypeId) ||
            subtypeId != (uint)spa_media_subtype.SPA_MEDIA_SUBTYPE_raw)
        {
            return false;
        }

        if (obj.TryGetValue((uint)spa_format.SPA_FORMAT_VIDEO_format, out SpaPod format) &&
            format.Value.TryGetId(out uint formatId))
        {
            info.format = (spa_video_format)formatId;
        }

        if (obj.TryGetValue((uint)spa_format.SPA_FORMAT_VIDEO_modifier, out SpaPod modifier) &&
            modifier.Value.TryGetLong(out long modifierValue))
        {
            info.modifier = unchecked((ulong)modifierValue);
            info.flags |= (uint)spa_video_flags.SPA_VIDEO_FLAG_MODIFIER;
        }

        if (obj.TryGetValue((uint)spa_format.SPA_FORMAT_VIDEO_size, out SpaPod size) &&
            size.Value.TryGetRectangle(out spa_rectangle sizeValue))
        {
            info.size = sizeValue;
        }

        if (obj.TryGetValue((uint)spa_format.SPA_FORMAT_VIDEO_framerate, out SpaPod framerate) &&
            framerate.Value.TryGetFraction(out spa_fraction framerateValue))
        {
            info.framerate = framerateValue;
        }

        if (obj.TryGetValue((uint)spa_format.SPA_FORMAT_VIDEO_maxFramerate, out SpaPod maxFramerate) &&
            maxFramerate.Value.TryGetFraction(out spa_fraction maxFramerateValue))
        {
            info.max_framerate = maxFramerateValue;
        }

        return true;
    }

    public static bool TryGetModifier(SpaPod pod, out ulong modifier)
    {
        modifier = 0;

        if (pod.IsNull || pod.Type != SPA_TYPE_Object)
        {
            return false;
        }

        SpaPodObject obj = pod.AsObject();
        if (obj.ObjectType != SPA_TYPE_OBJECT_Format ||
            !obj.TryGetValue((uint)spa_format.SPA_FORMAT_VIDEO_modifier, out SpaPod value) ||
            !value.Value.TryGetLong(out long result))
        {
            return false;
        }

        modifier = unchecked((ulong)result);
        return true;
    }

    public static bool NeedsModifierFixation(SpaPod pod)
    {
        if (pod.IsNull || pod.Type != SPA_TYPE_Object)
        {
            return false;
        }

        SpaPodObject obj = pod.AsObject();
        return obj.ObjectType == SPA_TYPE_OBJECT_Format &&
               obj.TryGetProperty((uint)spa_format.SPA_FORMAT_VIDEO_modifier, out SpaPodProperty property) &&
               (property.Flags & SPA_POD_PROP_FLAG_DONT_FIXATE) != 0;
    }

    public static int GetBitsPerPixel(spa_video_format format) => format switch
    {
        spa_video_format.SPA_VIDEO_FORMAT_RGB or
        spa_video_format.SPA_VIDEO_FORMAT_BGR => 24,

        spa_video_format.SPA_VIDEO_FORMAT_RGBx or
        spa_video_format.SPA_VIDEO_FORMAT_BGRx or
        spa_video_format.SPA_VIDEO_FORMAT_xRGB or
        spa_video_format.SPA_VIDEO_FORMAT_xBGR or
        spa_video_format.SPA_VIDEO_FORMAT_RGBA or
        spa_video_format.SPA_VIDEO_FORMAT_BGRA or
        spa_video_format.SPA_VIDEO_FORMAT_ARGB or
        spa_video_format.SPA_VIDEO_FORMAT_ABGR => 32,

        spa_video_format.SPA_VIDEO_FORMAT_RGB16 or
        spa_video_format.SPA_VIDEO_FORMAT_BGR16 or
        spa_video_format.SPA_VIDEO_FORMAT_RGB15 or
        spa_video_format.SPA_VIDEO_FORMAT_BGR15 or
        spa_video_format.SPA_VIDEO_FORMAT_YUY2 or
        spa_video_format.SPA_VIDEO_FORMAT_YVYU or
        spa_video_format.SPA_VIDEO_FORMAT_UYVY => 16,

        spa_video_format.SPA_VIDEO_FORMAT_I420 or
        spa_video_format.SPA_VIDEO_FORMAT_YV12 or
        spa_video_format.SPA_VIDEO_FORMAT_NV12 or
        spa_video_format.SPA_VIDEO_FORMAT_NV21 => 12,

        spa_video_format.SPA_VIDEO_FORMAT_GRAY8 => 8,

        _ => 0,
    };

    private static byte[] Build(
        ReadOnlySpan<spa_video_format> formats,
        ReadOnlySpan<ulong> modifiers,
        bool dontFixate,
        (uint Width, uint Height) defaultSize,
        (uint Width, uint Height) minSize,
        (uint Width, uint Height) maxSize,
        (uint Num, uint Denom) defaultFramerate,
        (uint Num, uint Denom) minFramerate,
        (uint Num, uint Denom) maxFramerate,
        spa_param_type paramType)
    {
        if (formats.IsEmpty)
        {
            throw new ArgumentException("At least one format must be offered.", nameof(formats));
        }

        using var builder = new SpaPodBuilder(512);

        using (builder.PushObject(SPA_TYPE_OBJECT_Format, (uint)paramType))
        {
            AddMediaType(builder);

            builder.AddProperty((uint)spa_format.SPA_FORMAT_VIDEO_format);
            if (formats.Length == 1)
            {
                builder.AddId((uint)formats[0]);
            }
            else
            {
                using (builder.PushChoice(spa_choice_type.SPA_CHOICE_Enum))
                {
                    builder.AddId((uint)formats[0]);
                    foreach (spa_video_format format in formats)
                    {
                        builder.AddId((uint)format);
                    }
                }
            }

            if (!modifiers.IsEmpty)
            {
                uint flags = dontFixate ? ModifierPropertyFlags : SPA_POD_PROP_FLAG_MANDATORY;
                builder.AddProperty((uint)spa_format.SPA_FORMAT_VIDEO_modifier, flags);

                if (modifiers.Length == 1 && !dontFixate)
                {
                    builder.AddLong(unchecked((long)modifiers[0]));
                }
                else
                {
                    using (builder.PushChoice(spa_choice_type.SPA_CHOICE_Enum))
                    {
                        builder.AddLong(unchecked((long)modifiers[0]));
                        foreach (ulong modifier in modifiers)
                        {
                            builder.AddLong(unchecked((long)modifier));
                        }
                    }
                }
            }

            builder.AddProperty((uint)spa_format.SPA_FORMAT_VIDEO_size);
            if (IsRange(minSize, maxSize))
            {
                builder.AddChoiceRectangleRange(
                    defaultSize.Width,
                    defaultSize.Height,
                    minSize.Width,
                    minSize.Height,
                    maxSize.Width,
                    maxSize.Height);
            }
            else
            {
                builder.AddRectangle(defaultSize.Width, defaultSize.Height);
            }

            if (defaultFramerate.Denom != 0)
            {
                builder.AddProperty((uint)spa_format.SPA_FORMAT_VIDEO_framerate);
                if (minFramerate.Denom != 0 && maxFramerate.Denom != 0)
                {
                    using (builder.PushChoice(spa_choice_type.SPA_CHOICE_Range))
                    {
                        builder.AddFraction(defaultFramerate.Num, defaultFramerate.Denom);
                        builder.AddFraction(minFramerate.Num, minFramerate.Denom);
                        builder.AddFraction(maxFramerate.Num, maxFramerate.Denom);
                    }
                }
                else
                {
                    builder.AddFraction(defaultFramerate.Num, defaultFramerate.Denom);
                }
            }
        }

        return builder.ToArray();
    }

    private static void AddMediaType(SpaPodBuilder builder)
    {
        builder.AddProperty((uint)spa_format.SPA_FORMAT_mediaType);
        builder.AddId((uint)spa_media_type.SPA_MEDIA_TYPE_video);
        builder.AddProperty((uint)spa_format.SPA_FORMAT_mediaSubtype);
        builder.AddId((uint)spa_media_subtype.SPA_MEDIA_SUBTYPE_raw);
    }

    private static bool IsRange((uint Width, uint Height) min, (uint Width, uint Height) max)
        => min.Width != 0 && min.Height != 0 && max.Width >= min.Width && max.Height >= min.Height;
}
