using PipeWire.Native;
using PipeWire.Spa;
using Xunit;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Tests;

public unsafe class SpaVideoFormatTests
{
    [Fact]
    public void FixedRawFormatMatchesTheCBuilder()
    {
        byte[] pod = SpaVideoFormats.BuildRaw(
            spa_video_format.SPA_VIDEO_FORMAT_BGRx,
            size: (1920, 1080),
            framerate: (60, 1));

        Assert.Equal(
            "800000000f00000003000400030000000100000000000000040000000300000002000000000000000200000000000000" +
            "040000000300000001000000000000000100020000000000040000000300000008000000000000000300020000000000" +
            "080000000a00000080070000380400000400020000000000080000000b0000003c00000001000000",
            Hex(pod));
    }

    [Fact]
    public void RawChoiceMatchesTheCBuilder()
    {
        byte[] pod = SpaVideoFormats.BuildRawChoice(
            [spa_video_format.SPA_VIDEO_FORMAT_BGRx, spa_video_format.SPA_VIDEO_FORMAT_RGBx],
            defaultSize: (1920, 1080),
            minSize: (1, 1),
            maxSize: (8192, 4320),
            defaultFramerate: (60, 1),
            minFramerate: (0, 1),
            maxFramerate: (1000, 1));

        Assert.Equal(
            "d80000000f00000003000400030000000100000000000000040000000300000002000000000000000200000000000000" +
            "0400000003000000010000000000000001000200000000001c0000001300000003000000000000000400000003000000" +
            "08000000080000000700000000000000030002000000000028000000130000000100000000000000080000000a000000" +
            "8007000038040000010000000100000000200000e0100000040002000000000028000000130000000100000000000000" +
            "080000000b0000003c000000010000000000000001000000e803000001000000",
            Hex(pod));
    }

    [Fact]
    public void DmaBufModifiersAreMandatoryAndDontFixate()
    {
        byte[] pod = SpaVideoFormats.BuildDmaBuf(
            [spa_video_format.SPA_VIDEO_FORMAT_BGRx],
            [0x0100000000000001UL, 0x00ffffffffffffffUL],
            defaultSize: (1920, 1080),
            defaultFramerate: (60, 1));

        Assert.Equal(
            "b80000000f00000003000400030000000100000000000000040000000300000002000000000000000200000000000000" +
            "040000000300000001000000000000000100020000000000040000000300000008000000000000000200020018000000" +
            "28000000130000000300000000000000080000000500000001000000000000010100000000000001ffffffffffffff00" +
            "0300020000000000080000000a00000080070000380400000400020000000000080000000b0000003c00000001000000",
            Hex(pod));
    }

    [Fact]
    public void DmaBufAndFallbackOffersTheModifierPodFirst()
    {
        byte[][] pods = SpaVideoFormats.BuildDmaBufAndFallback(
            [spa_video_format.SPA_VIDEO_FORMAT_BGRx],
            [0x0100000000000001UL],
            defaultSize: (1920, 1080),
            defaultFramerate: (60, 1));

        Assert.Equal(2, pods.Length);

        fixed (byte* first = pods[0])
        fixed (byte* second = pods[1])
        {
            Assert.True(SpaVideoFormats.NeedsModifierFixation(new SpaPod((spa_pod*)first)));
            Assert.True(SpaVideoFormats.TryGetModifier(new SpaPod((spa_pod*)first), out ulong modifier));
            Assert.Equal(0x0100000000000001UL, modifier);

            Assert.False(SpaVideoFormats.NeedsModifierFixation(new SpaPod((spa_pod*)second)));
            Assert.False(SpaVideoFormats.TryGetModifier(new SpaPod((spa_pod*)second), out _));
        }
    }

    [Fact]
    public void FixedFormatRoundTripsThroughTryParseRaw()
    {
        byte[] pod = SpaVideoFormats.BuildRaw(
            spa_video_format.SPA_VIDEO_FORMAT_NV12,
            size: (1280, 720),
            framerate: (30000, 1001),
            modifier: 0x0100000000000002UL,
            paramType: spa_param_type.SPA_PARAM_Format);

        fixed (byte* p = pod)
        {
            Assert.True(SpaVideoFormats.TryParseRaw(new SpaPod((spa_pod*)p), out spa_video_info_raw info));

            Assert.Equal(spa_video_format.SPA_VIDEO_FORMAT_NV12, info.format);
            Assert.Equal(1280u, info.size.width);
            Assert.Equal(720u, info.size.height);
            Assert.Equal(30000u, info.framerate.num);
            Assert.Equal(1001u, info.framerate.denom);
            Assert.Equal(0x0100000000000002UL, info.modifier);
            Assert.Equal((uint)spa_video_flags.SPA_VIDEO_FLAG_MODIFIER, info.flags & (uint)spa_video_flags.SPA_VIDEO_FLAG_MODIFIER);
        }
    }

    [Fact]
    public void TryParseRawReadsTheDefaultOutOfAChoice()
    {
        byte[] pod = SpaVideoFormats.BuildRawChoice(
            [spa_video_format.SPA_VIDEO_FORMAT_BGRx, spa_video_format.SPA_VIDEO_FORMAT_RGBx],
            defaultSize: (1920, 1080),
            minSize: (1, 1),
            maxSize: (8192, 4320),
            defaultFramerate: (60, 1),
            minFramerate: (0, 1),
            maxFramerate: (1000, 1));

        fixed (byte* p = pod)
        {
            Assert.True(SpaVideoFormats.TryParseRaw(new SpaPod((spa_pod*)p), out spa_video_info_raw info));

            Assert.Equal(spa_video_format.SPA_VIDEO_FORMAT_BGRx, info.format);
            Assert.Equal(1920u, info.size.width);
            Assert.Equal(60u, info.framerate.num);
            Assert.Equal(0u, info.modifier);
        }
    }

    [Fact]
    public void AnAudioFormatIsNotParsedAsVideo()
    {
        byte[] pod = SpaAudioFormats.BuildRaw(spa_audio_format.SPA_AUDIO_FORMAT_F32, 48000, 2);

        fixed (byte* p = pod)
        {
            Assert.False(SpaVideoFormats.TryParseRaw(new SpaPod((spa_pod*)p), out _));
        }
    }

    [Fact]
    public void BuildRawChoiceRejectsAnEmptyFormatList()
        => Assert.Throws<ArgumentException>(() => SpaVideoFormats.BuildRawChoice([], (1920, 1080)));

    [Fact]
    public void BuildDmaBufRejectsAnEmptyModifierList()
        => Assert.Throws<ArgumentException>(
            () => SpaVideoFormats.BuildDmaBuf([spa_video_format.SPA_VIDEO_FORMAT_BGRx], [], (1920, 1080)));

    private static string Hex(byte[] pod) => Convert.ToHexString(pod).ToLowerInvariant();
}
