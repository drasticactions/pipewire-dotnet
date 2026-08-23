using PipeWire.Native;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Spa;

public static unsafe class SpaAudioFormats
{
    public const int MaxChannelPositions = 64;

    public static byte[] BuildRaw(
        spa_audio_format format,
        uint rate,
        uint channels,
        ReadOnlySpan<spa_audio_channel> positions = default,
        spa_param_type paramType = spa_param_type.SPA_PARAM_EnumFormat)
    {
        using var builder = new SpaPodBuilder(256);

        using (builder.PushObject(SPA_TYPE_OBJECT_Format, (uint)paramType))
        {
            builder.AddProperty((uint)spa_format.SPA_FORMAT_mediaType);
            builder.AddId((uint)spa_media_type.SPA_MEDIA_TYPE_audio);
            builder.AddProperty((uint)spa_format.SPA_FORMAT_mediaSubtype);
            builder.AddId((uint)spa_media_subtype.SPA_MEDIA_SUBTYPE_raw);
            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_format);
            builder.AddId((uint)format);
            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_rate);
            builder.AddInt((int)rate);
            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_channels);
            builder.AddInt((int)channels);

            if (!positions.IsEmpty)
            {
                builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_position);
                AddPositions(builder, positions);
            }
        }

        return builder.ToArray();
    }

    public static byte[] BuildRawChoice(
        ReadOnlySpan<spa_audio_format> formats,
        uint defaultRate = 48000,
        uint minRate = 0,
        uint maxRate = 0,
        uint defaultChannels = 2,
        uint minChannels = 0,
        uint maxChannels = 0)
    {
        if (formats.IsEmpty)
        {
            throw new ArgumentException("At least one format must be offered.", nameof(formats));
        }

        using var builder = new SpaPodBuilder(256);

        using (builder.PushObject(SPA_TYPE_OBJECT_Format, (uint)spa_param_type.SPA_PARAM_EnumFormat))
        {
            builder.AddProperty((uint)spa_format.SPA_FORMAT_mediaType);
            builder.AddId((uint)spa_media_type.SPA_MEDIA_TYPE_audio);
            builder.AddProperty((uint)spa_format.SPA_FORMAT_mediaSubtype);
            builder.AddId((uint)spa_media_subtype.SPA_MEDIA_SUBTYPE_raw);

            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_format);
            if (formats.Length == 1)
            {
                builder.AddId((uint)formats[0]);
            }
            else
            {
                using (builder.PushChoice(spa_choice_type.SPA_CHOICE_Enum))
                {
                    builder.AddId((uint)formats[0]);
                    foreach (spa_audio_format format in formats)
                    {
                        builder.AddId((uint)format);
                    }
                }
            }

            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_rate);
            AddIntChoice(builder, (int)defaultRate, (int)minRate, (int)maxRate);

            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_channels);
            AddIntChoice(builder, (int)defaultChannels, (int)minChannels, (int)maxChannels);
        }

        return builder.ToArray();
    }

    public static bool TryParseRaw(SpaPod pod, out spa_audio_info_raw info)
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
            !mediaType.TryGetId(out uint mediaTypeId) ||
            mediaTypeId != (uint)spa_media_type.SPA_MEDIA_TYPE_audio)
        {
            return false;
        }

        if (!obj.TryGetValue((uint)spa_format.SPA_FORMAT_mediaSubtype, out SpaPod subtype) ||
            !subtype.TryGetId(out uint subtypeId) ||
            subtypeId != (uint)spa_media_subtype.SPA_MEDIA_SUBTYPE_raw)
        {
            return false;
        }

        if (obj.TryGetValue((uint)spa_format.SPA_FORMAT_AUDIO_format, out SpaPod format) &&
            format.TryGetId(out uint formatId))
        {
            info.format = (spa_audio_format)formatId;
        }

        if (obj.TryGetValue((uint)spa_format.SPA_FORMAT_AUDIO_rate, out SpaPod rate) &&
            rate.TryGetInt(out int rateValue))
        {
            info.rate = (uint)rateValue;
        }

        if (obj.TryGetValue((uint)spa_format.SPA_FORMAT_AUDIO_channels, out SpaPod channels) &&
            channels.TryGetInt(out int channelCount))
        {
            info.channels = (uint)channelCount;
        }

        if (obj.TryGetValue((uint)spa_format.SPA_FORMAT_AUDIO_position, out SpaPod position) &&
            position.Type == SPA_TYPE_Array)
        {
            SpaPodArray array = position.AsArray();
            if (array.ChildType == SPA_TYPE_Id && array.ChildSize == sizeof(uint))
            {
                ReadOnlySpan<uint> values = array.AsSpan<uint>();
                int count = Math.Min(values.Length, MaxChannelPositions);
                for (int i = 0; i < count; i++)
                {
                    info.position[i] = values[i];
                }
            }
        }

        return true;
    }

    public static int GetSampleSize(spa_audio_format format) => format switch
    {
        spa_audio_format.SPA_AUDIO_FORMAT_U8 or
        spa_audio_format.SPA_AUDIO_FORMAT_S8 or
        spa_audio_format.SPA_AUDIO_FORMAT_ULAW or
        spa_audio_format.SPA_AUDIO_FORMAT_ALAW or
        spa_audio_format.SPA_AUDIO_FORMAT_U8P or
        spa_audio_format.SPA_AUDIO_FORMAT_S8P => 1,

        spa_audio_format.SPA_AUDIO_FORMAT_S16_LE or
        spa_audio_format.SPA_AUDIO_FORMAT_S16_BE or
        spa_audio_format.SPA_AUDIO_FORMAT_U16_LE or
        spa_audio_format.SPA_AUDIO_FORMAT_U16_BE or
        spa_audio_format.SPA_AUDIO_FORMAT_S16P => 2,

        spa_audio_format.SPA_AUDIO_FORMAT_S24_LE or
        spa_audio_format.SPA_AUDIO_FORMAT_S24_BE or
        spa_audio_format.SPA_AUDIO_FORMAT_U24_LE or
        spa_audio_format.SPA_AUDIO_FORMAT_U24_BE or
        spa_audio_format.SPA_AUDIO_FORMAT_S24P => 3,

        spa_audio_format.SPA_AUDIO_FORMAT_S32_LE or
        spa_audio_format.SPA_AUDIO_FORMAT_S32_BE or
        spa_audio_format.SPA_AUDIO_FORMAT_U32_LE or
        spa_audio_format.SPA_AUDIO_FORMAT_U32_BE or
        spa_audio_format.SPA_AUDIO_FORMAT_S24_32_LE or
        spa_audio_format.SPA_AUDIO_FORMAT_S24_32_BE or
        spa_audio_format.SPA_AUDIO_FORMAT_U24_32_LE or
        spa_audio_format.SPA_AUDIO_FORMAT_U24_32_BE or
        spa_audio_format.SPA_AUDIO_FORMAT_F32_LE or
        spa_audio_format.SPA_AUDIO_FORMAT_F32_BE or
        spa_audio_format.SPA_AUDIO_FORMAT_S32P or
        spa_audio_format.SPA_AUDIO_FORMAT_S24_32P or
        spa_audio_format.SPA_AUDIO_FORMAT_F32P => 4,

        spa_audio_format.SPA_AUDIO_FORMAT_F64_LE or
        spa_audio_format.SPA_AUDIO_FORMAT_F64_BE or
        spa_audio_format.SPA_AUDIO_FORMAT_F64P => 8,

        _ => 0,
    };

    private static void AddPositions(SpaPodBuilder builder, ReadOnlySpan<spa_audio_channel> positions)
    {
        int count = Math.Min(positions.Length, MaxChannelPositions);
        Span<uint> ids = count <= MaxChannelPositions ? stackalloc uint[count] : new uint[count];
        for (int i = 0; i < count; i++)
        {
            ids[i] = (uint)positions[i];
        }

        builder.AddIdArray(ids);
    }

    private static void AddIntChoice(SpaPodBuilder builder, int preferred, int min, int max)
    {
        if (min <= 0 || max <= 0 || min > max)
        {
            builder.AddInt(preferred);
            return;
        }

        builder.AddChoiceRange(preferred, min, max);
    }
}
