using PipeWire.Native;
using PipeWire.Spa;
using Xunit;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Tests;

public class SpaPodBuilderTests
{
    [Fact]
    public void AudioEnumFormatObjectMatchesTheCBuilder()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushObject(SPA_TYPE_OBJECT_Format, (uint)spa_param_type.SPA_PARAM_EnumFormat))
        {
            builder.AddProperty((uint)spa_format.SPA_FORMAT_mediaType);
            builder.AddId((uint)spa_media_type.SPA_MEDIA_TYPE_audio);
            builder.AddProperty((uint)spa_format.SPA_FORMAT_mediaSubtype);
            builder.AddId((uint)spa_media_subtype.SPA_MEDIA_SUBTYPE_raw);
            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_format);
            builder.AddId((uint)spa_audio_format.SPA_AUDIO_FORMAT_F32);
            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_rate);
            builder.AddInt(48000);
            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_channels);
            builder.AddInt(2);
        }

        Assert.Equal(
            "800000000f0000000300040003000000010000000000000004000000030000000100000000000000" +
            "020000000000000004000000030000000100000000000000010001000000000004000000030000001b" +
            "010000000000000300010000000000040000000400000080bb000000000000040001000000000004000000040000000200000000000000",
            Hex(builder));
    }

    [Fact]
    public void StructOfPrimitivesMatchesTheCBuilder()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushStruct())
        {
            builder.AddBool(true);
            builder.AddInt(-7);
            builder.AddLong(0x1122334455667788L);
            builder.AddFloat(0.5f);
            builder.AddDouble(0.25);
            builder.AddString("hello");
            builder.AddBytes([1, 2, 3]);
            builder.AddRectangle(1920, 1080);
            builder.AddFraction(30000, 1001);
            builder.AddFd(42);
            builder.AddNone();
        }

        Assert.Equal(
            "a80000000e000000040000000200000001000000000000000400000004000000f9ffffff00000000" +
            "0800000005000000887766554433221104000000060000000000003f0000000008000000070000000000" +
            "00000000d03f060000000800000068656c6c6f00000003000000090000000102030000000000080000000a" +
            "0000008007000038040000080000000b00000030750000e903000008000000120000002a000000000000000000000001000000",
            Hex(builder));
    }

    [Fact]
    public void ArrayWrittenInOneCallMatchesTheCBuilder()
    {
        using var builder = new SpaPodBuilder();
        builder.AddIntArray([1, 2, 3, 4, 5]);

        Assert.Equal("1c0000000d0000000400000004000000010000000200000003000000040000000500000000000000", Hex(builder));
    }

    [Fact]
    public void ArrayWrittenElementByElementMatchesTheCBuilder()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushArray())
        {
            builder.AddInt(10);
            builder.AddInt(20);
            builder.AddInt(30);
        }

        Assert.Equal("140000000d00000004000000040000000a000000140000001e00000000000000", Hex(builder));
    }

    [Fact]
    public void EmptyArrayGetsTheNoneChildTheCBuilderWrites()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushArray())
        {
        }

        Assert.Equal("080000000d0000000000000001000000", Hex(builder));
    }

    [Fact]
    public void ObjectWithChoiceAndIdArrayMatchesTheCBuilder()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushObject(SPA_TYPE_OBJECT_Format, (uint)spa_param_type.SPA_PARAM_EnumFormat))
        {
            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_rate);
            builder.AddChoiceRange(48000, 8000, 192000);

            builder.AddProperty((uint)spa_format.SPA_FORMAT_AUDIO_position);
            builder.AddIdArray([(uint)spa_audio_channel.SPA_AUDIO_CHANNEL_FL, (uint)spa_audio_channel.SPA_AUDIO_CHANNEL_FR]);
        }

        Assert.Equal(
            "580000000f000000030004000300000003000100000000001c00000013000000010000000000000004000000" +
            "0400000080bb0000401f000000ee0200000000000500010000000000100000000d00000004000000030000000300000004000000",
            Hex(builder));
    }

    [Fact]
    public void ChoiceEnumMatchesTheCBuilder()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushChoice(spa_choice_type.SPA_CHOICE_Enum))
        {
            builder.AddId((uint)spa_audio_format.SPA_AUDIO_FORMAT_F32);
            builder.AddId((uint)spa_audio_format.SPA_AUDIO_FORMAT_S16);
            builder.AddId((uint)spa_audio_format.SPA_AUDIO_FORMAT_S32);
        }

        Assert.Equal("1c00000013000000030000000000000004000000030000001b010000030100000b01000000000000", Hex(builder));
    }

    [Fact]
    public void NestedStructsMatchTheCBuilder()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushStruct())
        {
            builder.AddInt(1);
            using (builder.PushStruct())
            {
                builder.AddString("nested");
                builder.AddInt(2);
            }

            builder.AddString(string.Empty);
        }

        Assert.Equal(
            "480000000e00000004000000040000000100000000000000200000000e00000007000000080000006e657374" +
            "656400000400000004000000020000000000000001000000080000000000000000000000",
            Hex(builder));
    }

    [Fact]
    public void PropsObjectMatchesTheCBuilder()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushObject(SPA_TYPE_OBJECT_Props, (uint)spa_param_type.SPA_PARAM_Props))
        {
            builder.AddProperty((uint)spa_prop.SPA_PROP_volume);
            builder.AddFloat(0.75f);
            builder.AddProperty((uint)spa_prop.SPA_PROP_mute);
            builder.AddBool(false);
        }

        Assert.Equal(
            "380000000f0000000200040002000000030001000000000004000000060000000000403f0000000004000100" +
            "0000000004000000020000000000000000000000",
            Hex(builder));
    }

    [Fact]
    public void SequenceOfControlsMatchesTheCBuilder()
    {
        using var builder = new SpaPodBuilder();
        using (builder.PushSequence())
        {
            builder.AddControl(0, (uint)spa_control_type.SPA_CONTROL_Properties);
            builder.AddInt(5);
            builder.AddControl(128, (uint)spa_control_type.SPA_CONTROL_Midi);
            builder.AddBytes([0x90, 0x40, 0x7f]);
        }

        Assert.Equal(
            "380000001000000000000000000000000000000001000000040000000400000005000000000000008000" +
            "000002000000030000000900000090407f0000000000",
            Hex(builder));
    }

    [Fact]
    public void FramesMustBeClosedBeforeReadingTheResult()
    {
        using var builder = new SpaPodBuilder();
        SpaPodFrame frame = builder.PushStruct();

        Assert.Throws<InvalidOperationException>(() => builder.ToArray());

        frame.Dispose();
        Assert.NotEmpty(builder.ToArray());
    }

    [Fact]
    public void EveryPodIsPaddedToTheAlignment()
    {
        using var builder = new SpaPodBuilder();
        builder.AddString("odd length");

        Assert.Equal(0, builder.Length % SpaPodBuilder.PodAlign);
    }

    private static string Hex(SpaPodBuilder builder) => Convert.ToHexString(builder.ToArray()).ToLowerInvariant();
}
