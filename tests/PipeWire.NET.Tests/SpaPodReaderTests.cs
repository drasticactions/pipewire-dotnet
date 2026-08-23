using PipeWire.Native;
using PipeWire.Spa;
using Xunit;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Tests;

public unsafe class SpaPodReaderTests
{
    [Fact]
    public void PrimitivesRoundTrip()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushStruct())
        {
            builder.AddBool(true);
            builder.AddId(42);
            builder.AddInt(-7);
            builder.AddLong(long.MinValue);
            builder.AddFloat(0.5f);
            builder.AddDouble(-0.25);
            builder.AddString("hello");
            builder.AddBytes([1, 2, 3]);
            builder.AddRectangle(1920, 1080);
            builder.AddFraction(30000, 1001);
            builder.AddFd(9);
        }

        Read(builder, pod =>
        {
            List<SpaPod> fields = pod.AsStruct().ToList();

            Assert.Equal(11, fields.Count);
            Assert.True(fields[0].AsBool());
            Assert.Equal(42u, fields[1].AsId());
            Assert.Equal(-7, fields[2].AsInt());
            Assert.Equal(long.MinValue, fields[3].AsLong());
            Assert.Equal(0.5f, fields[4].AsFloat());
            Assert.Equal(-0.25, fields[5].AsDouble());
            Assert.Equal("hello", fields[6].AsString());
            Assert.Equal<byte>([1, 2, 3], fields[7].AsBytes().ToArray());
            Assert.Equal(1920u, fields[8].AsRectangle().width);
            Assert.Equal(1080u, fields[8].AsRectangle().height);
            Assert.Equal(30000u, fields[9].AsFraction().num);
            Assert.Equal(1001u, fields[9].AsFraction().denom);
            Assert.Equal(9, fields[10].AsFd());
        });
    }

    [Fact]
    public void ReadingTheWrongTypeThrowsButTryGetDoesNot()
    {
        using var builder = new SpaPodBuilder();
        builder.AddInt(5);

        Read(builder, pod =>
        {
            Assert.Throws<InvalidOperationException>(() => pod.AsString());
            Assert.False(pod.TryGetString(out string? value));
            Assert.Null(value);
            Assert.True(pod.TryGetInt(out int number));
            Assert.Equal(5, number);
        });
    }

    [Fact]
    public void ObjectPropertiesCanBeWalkedAndLookedUp()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushObject(SPA_TYPE_OBJECT_Props, (uint)spa_param_type.SPA_PARAM_Props))
        {
            builder.AddProperty((uint)spa_prop.SPA_PROP_volume);
            builder.AddFloat(0.75f);
            builder.AddProperty((uint)spa_prop.SPA_PROP_mute);
            builder.AddBool(true);
        }

        Read(builder, pod =>
        {
            SpaPodObject obj = pod.AsObject();

            Assert.Equal(SPA_TYPE_OBJECT_Props, obj.ObjectType);
            Assert.Equal((uint)spa_param_type.SPA_PARAM_Props, obj.Id);

            var keys = new List<uint>();
            foreach (SpaPodProperty property in obj)
            {
                keys.Add(property.Key);
            }

            Assert.Equal([(uint)spa_prop.SPA_PROP_volume, (uint)spa_prop.SPA_PROP_mute], keys);
            Assert.Equal(0.75f, obj[(uint)spa_prop.SPA_PROP_volume].AsFloat());
            Assert.True(obj[(uint)spa_prop.SPA_PROP_mute].AsBool());
            Assert.False(obj.TryGetValue(9999, out _));
            Assert.Throws<KeyNotFoundException>(() => obj[9999]);
        });
    }

    [Fact]
    public void ArraysComeBackAsTypedSpans()
    {
        using var builder = new SpaPodBuilder();
        builder.AddIdArray([1, 2, 3, 4]);

        Read(builder, pod =>
        {
            SpaPodArray array = pod.AsArray();

            Assert.Equal(SPA_TYPE_Id, array.ChildType);
            Assert.Equal(4u, array.ChildSize);
            Assert.Equal(4, array.Count);
            Assert.Equal<uint>([1, 2, 3, 4], array.AsSpan<uint>().ToArray());
            Assert.Throws<InvalidOperationException>(() => array.AsSpan<long>().Length);
        });
    }

    [Fact]
    public void AnEmptyArrayReadsAsEmpty()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushArray())
        {
        }

        Read(builder, pod => Assert.Equal(0, pod.AsArray().Count));
    }

    [Fact]
    public void ChoicesExposeTheirValuesAndUnwrapToTheFirst()
    {
        using var builder = new SpaPodBuilder();
        builder.AddChoiceRange(48000, 8000, 192000);

        Read(builder, pod =>
        {
            SpaPodChoice choice = pod.AsChoice();

            Assert.Equal(spa_choice_type.SPA_CHOICE_Range, choice.ChoiceType);
            Assert.Equal(3, choice.Count);
            Assert.Equal<int>([48000, 8000, 192000], choice.AsSpan<int>().ToArray());

            Assert.Equal(48000, pod.Value.AsInt());
        });
    }

    [Fact]
    public void NestedStructsWalkIndependently()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushStruct())
        {
            builder.AddInt(1);
            using (builder.PushStruct())
            {
                builder.AddString("inner");
                builder.AddInt(2);
            }

            builder.AddInt(3);
        }

        Read(builder, pod =>
        {
            List<SpaPod> outer = pod.AsStruct().ToList();

            Assert.Equal(3, outer.Count);
            Assert.Equal(1, outer[0].AsInt());
            Assert.Equal(3, outer[2].AsInt());

            List<SpaPod> inner = outer[1].AsStruct().ToList();
            Assert.Equal(2, inner.Count);
            Assert.Equal("inner", inner[0].AsString());
            Assert.Equal(2, inner[1].AsInt());
        });
    }

    [Fact]
    public void SequencesWalkTheirControls()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushSequence())
        {
            builder.AddControl(0, (uint)spa_control_type.SPA_CONTROL_Properties);
            builder.AddInt(5);
            builder.AddControl(128, (uint)spa_control_type.SPA_CONTROL_Midi);
            builder.AddBytes([0x90, 0x40, 0x7f]);
        }

        Read(builder, pod =>
        {
            var controls = new List<(uint Offset, spa_control_type Type)>();
            foreach (SpaPodControl control in pod.AsSequence())
            {
                controls.Add((control.Offset, control.ControlType));
            }

            Assert.Equal([(0u, spa_control_type.SPA_CONTROL_Properties), (128u, spa_control_type.SPA_CONTROL_Midi)], controls);
        });
    }

    [Fact]
    public void AStringWhosePaddingIsCountedInItsSizeStillReadsCleanly()
    {
        byte[] pod =
        [
            0x08, 0x00, 0x00, 0x00,
            0x08, 0x00, 0x00, 0x00,
            (byte)'a', (byte)'b', (byte)'c', 0x00, 0x00, 0x00, 0x00, 0x00,
        ];

        fixed (byte* p = pod)
        {
            Assert.Equal("abc", new SpaPod((spa_pod*)p).AsString());
        }
    }

    [Fact]
    public void ATruncatedObjectStopsIteratingInsteadOfReadingPastItsEnd()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushObject(SPA_TYPE_OBJECT_Props, 0))
        {
            builder.AddProperty((uint)spa_prop.SPA_PROP_volume);
            builder.AddFloat(0.5f);
            builder.AddProperty((uint)spa_prop.SPA_PROP_mute);
            builder.AddBool(false);
        }

        byte[] bytes = builder.ToArray();

        byte[] truncated = (byte[])bytes.Clone();
        uint size = System.Runtime.InteropServices.MemoryMarshal.Read<uint>(truncated);
        System.Runtime.InteropServices.MemoryMarshal.Write(truncated.AsSpan(), size - 8);

        fixed (byte* p = truncated)
        {
            var pod = new SpaPod((spa_pod*)p);
            int count = 0;
            foreach (SpaPodProperty _ in pod.AsObject())
            {
                count++;
            }

            Assert.Equal(1, count);
        }
    }

    [Fact]
    public void AudioFormatsRoundTripThroughTheTypedHelper()
    {
        byte[] pod = SpaAudioFormats.BuildRaw(
            spa_audio_format.SPA_AUDIO_FORMAT_S16_LE,
            44100,
            2,
            [spa_audio_channel.SPA_AUDIO_CHANNEL_FL, spa_audio_channel.SPA_AUDIO_CHANNEL_FR],
            spa_param_type.SPA_PARAM_Format);

        fixed (byte* p = pod)
        {
            Assert.True(SpaAudioFormats.TryParseRaw(new SpaPod((spa_pod*)p), out spa_audio_info_raw info));

            Assert.Equal(spa_audio_format.SPA_AUDIO_FORMAT_S16_LE, info.format);
            Assert.Equal(44100u, info.rate);
            Assert.Equal(2u, info.channels);
            Assert.Equal((uint)spa_audio_channel.SPA_AUDIO_CHANNEL_FL, info.position[0]);
            Assert.Equal((uint)spa_audio_channel.SPA_AUDIO_CHANNEL_FR, info.position[1]);
        }
    }

    [Fact]
    public void AFormatChoiceIsReadAsItsPreferredValue()
    {
        byte[] pod = SpaAudioFormats.BuildRawChoice(
            [spa_audio_format.SPA_AUDIO_FORMAT_F32_LE, spa_audio_format.SPA_AUDIO_FORMAT_S16_LE],
            defaultRate: 48000,
            minRate: 8000,
            maxRate: 192000,
            defaultChannels: 2);

        fixed (byte* p = pod)
        {
            Assert.True(SpaAudioFormats.TryParseRaw(new SpaPod((spa_pod*)p), out spa_audio_info_raw info));

            Assert.Equal(spa_audio_format.SPA_AUDIO_FORMAT_F32_LE, info.format);
            Assert.Equal(48000u, info.rate);
            Assert.Equal(2u, info.channels);
        }
    }

    [Fact]
    public void SomethingThatIsNotAnAudioFormatIsRejected()
    {
        using var builder = new SpaPodBuilder();
        builder.AddInt(5);

        Read(builder, pod => Assert.False(SpaAudioFormats.TryParseRaw(pod, out _)));
    }

    [Fact]
    public void ANullPodReadsAsEmptyRatherThanCrashing()
    {
        var pod = new SpaPod(null);

        Assert.True(pod.IsNull);
        Assert.Equal(0u, pod.BodySize);
        Assert.True(pod.Body.IsEmpty);
        Assert.False(pod.TryGetInt(out _));
        Assert.Equal("<null pod>", pod.ToString());
    }

    private static void Read(SpaPodBuilder builder, Action<SpaPod> assert)
    {
        byte[] bytes = builder.ToArray();
        fixed (byte* p = bytes)
        {
            assert(new SpaPod((spa_pod*)p));
        }
    }
}
