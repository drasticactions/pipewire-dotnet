using System.Runtime.InteropServices;
using System.Text;
using PipeWire.Native;
using PipeWire.Spa;
using Xunit;
using static PipeWire.Native.Pipewire;

namespace PipeWire.Tests;

public unsafe class NativeBindingTests
{
    public static TheoryData<string, int> ExpectedSizes => new()
    {
        { nameof(timespec), 16 },
        { nameof(spa_pod), 8 },
        { nameof(spa_pod_bool), 16 },
        { nameof(spa_pod_int), 16 },
        { nameof(spa_pod_long), 16 },
        { nameof(spa_pod_double), 16 },
        { nameof(spa_pod_rectangle), 16 },
        { nameof(spa_pod_fraction), 16 },
        { nameof(spa_pod_array), 16 },
        { nameof(spa_pod_array_body), 8 },
        { nameof(spa_pod_choice), 24 },
        { nameof(spa_pod_choice_body), 16 },
        { nameof(spa_pod_object), 16 },
        { nameof(spa_pod_object_body), 8 },
        { nameof(spa_pod_prop), 16 },
        { nameof(spa_pod_control), 16 },
        { nameof(spa_pod_sequence), 16 },
        { nameof(spa_pod_sequence_body), 8 },
        { nameof(spa_dict), 16 },
        { nameof(spa_dict_item), 16 },
        { nameof(spa_interface), 32 },
        { nameof(spa_callbacks), 16 },
        { nameof(spa_hook), 48 },
        { nameof(spa_list), 16 },
        { nameof(spa_rectangle), 8 },
        { nameof(spa_fraction), 8 },
        { nameof(spa_buffer), 24 },
        { nameof(spa_data), 40 },
        { nameof(spa_chunk), 16 },
        { nameof(spa_audio_info_raw), 272 },
        { nameof(spa_video_info_raw), 88 },
        { nameof(spa_point), 8 },
        { nameof(spa_region), 16 },
        { nameof(spa_meta), 16 },
        { nameof(spa_meta_header), 32 },
        { nameof(spa_meta_region), 16 },
        { nameof(spa_meta_bitmap), 20 },
        { nameof(spa_meta_cursor), 28 },
        { nameof(spa_meta_busy), 8 },
        { nameof(spa_meta_videotransform), 4 },
        { nameof(spa_meta_sync_timeline), 24 },
        { nameof(spa_param_info), 32 },
        { nameof(pw_buffer), 40 },
        { nameof(pw_time), 64 },
        { nameof(pw_properties), 24 },
        { nameof(pw_core_events), 80 },
        { nameof(pw_core_methods), 72 },
        { nameof(pw_registry_events), 24 },
        { nameof(pw_registry_methods), 32 },
        { nameof(pw_node_events), 24 },
        { nameof(pw_node_methods), 48 },
        { nameof(pw_stream_events), 96 },
        { nameof(pw_filter_events), 80 },
        { nameof(pw_proxy_events), 56 },
        { nameof(spa_loop_control_methods), 104 },
    };

    [Theory]
    [MemberData(nameof(ExpectedSizes))]
    public void GeneratedStructsMatchTheirCSizes(string name, int expected)
        => Assert.Equal(expected, SizeOf(name));

    [Fact]
    public void TheLibraryResolvesAndReportsItsVersion()
    {
        string version = Utf8OrEmpty(pw_get_library_version());

        Assert.NotEmpty(version);
        Assert.StartsWith("1.", version, StringComparison.Ordinal);
    }

    [Fact]
    public void TheHeaderVersionMatchesThePinnedSubmodule()
    {
        Assert.Equal(1, PW_MAJOR);
        Assert.Equal(6, PW_MINOR);
        Assert.Equal(8, PW_MICRO);
    }

    [Fact]
    public void TheLinkedLibraryIsNewEnoughForTheseBindings()
        => Assert.True(pw_check_library_version(PW_MAJOR, PW_MINOR, 0) != 0,
            $"The bindings target {PW_MAJOR}.{PW_MINOR}.{PW_MICRO} but the installed library is {Utf8OrEmpty(pw_get_library_version())}.");

    [Theory]
    [InlineData(PipeWireInterfaces.Core)]
    [InlineData(PipeWireInterfaces.Registry)]
    [InlineData(PipeWireInterfaces.Node)]
    [InlineData(PipeWireInterfaces.Port)]
    [InlineData(PipeWireInterfaces.Link)]
    [InlineData(PipeWireInterfaces.Device)]
    [InlineData(PipeWireInterfaces.Client)]
    [InlineData(PipeWireInterfaces.Module)]
    [InlineData(PipeWireInterfaces.Factory)]
    [InlineData(PipeWireInterfaces.Metadata)]
    public void InterfaceNamesAreWellFormed(string name)
        => Assert.StartsWith("PipeWire:Interface:", name, StringComparison.Ordinal);

    [Fact]
    public void InterfaceNameConstantsMatchTheGeneratedLiterals()
    {
        Assert.Equal(PipeWireInterfaces.Core, Encoding.UTF8.GetString(PW_TYPE_INTERFACE_Core));
        Assert.Equal(PipeWireInterfaces.Registry, Encoding.UTF8.GetString(PW_TYPE_INTERFACE_Registry));
        Assert.Equal(PipeWireInterfaces.Node, Encoding.UTF8.GetString(PW_TYPE_INTERFACE_Node));
        Assert.Equal(PipeWireInterfaces.Port, Encoding.UTF8.GetString(PW_TYPE_INTERFACE_Port));
        Assert.Equal(PipeWireInterfaces.Link, Encoding.UTF8.GetString(PW_TYPE_INTERFACE_Link));
        Assert.Equal(PipeWireInterfaces.Device, Encoding.UTF8.GetString(PW_TYPE_INTERFACE_Device));
        Assert.Equal(PipeWireInterfaces.Client, Encoding.UTF8.GetString(PW_TYPE_INTERFACE_Client));
        Assert.Equal(PipeWireInterfaces.Module, Encoding.UTF8.GetString(PW_TYPE_INTERFACE_Module));
        Assert.Equal(PipeWireInterfaces.Factory, Encoding.UTF8.GetString(PW_TYPE_INTERFACE_Factory));
        Assert.Equal(PipeWireInterfaces.Metadata, Encoding.UTF8.GetString(PW_TYPE_INTERFACE_Metadata));
    }

    [Fact]
    public void PodTypeConstantsHaveTheValuesTheWireFormatUses()
    {
        Assert.Equal(1u, SPA_TYPE_None);
        Assert.Equal(2u, SPA_TYPE_Bool);
        Assert.Equal(3u, SPA_TYPE_Id);
        Assert.Equal(4u, SPA_TYPE_Int);
        Assert.Equal(8u, SPA_TYPE_String);
        Assert.Equal(13u, SPA_TYPE_Array);
        Assert.Equal(14u, SPA_TYPE_Struct);
        Assert.Equal(15u, SPA_TYPE_Object);
        Assert.Equal(19u, SPA_TYPE_Choice);
    }

    [Fact]
    public void TheMethodsTableVersionIsTheFirstFieldOfEveryInterface()
    {
        Assert.Equal(0, (int)Marshal.OffsetOf<pw_core_methods>(nameof(pw_core_methods.version)));
        Assert.Equal(0, (int)Marshal.OffsetOf<pw_registry_methods>(nameof(pw_registry_methods.version)));
        Assert.Equal(0, (int)Marshal.OffsetOf<pw_node_methods>(nameof(pw_node_methods.version)));
        Assert.Equal(0, (int)Marshal.OffsetOf<pw_device_methods>(nameof(pw_device_methods.version)));
        Assert.Equal(0, (int)Marshal.OffsetOf<spa_loop_control_methods>(nameof(spa_loop_control_methods.version)));
    }

    [Fact]
    public void SampleSizesAreKnownForTheCommonAudioFormats()
    {
        Assert.Equal(1, SpaAudioFormats.GetSampleSize(spa_audio_format.SPA_AUDIO_FORMAT_U8));
        Assert.Equal(2, SpaAudioFormats.GetSampleSize(spa_audio_format.SPA_AUDIO_FORMAT_S16_LE));
        Assert.Equal(4, SpaAudioFormats.GetSampleSize(spa_audio_format.SPA_AUDIO_FORMAT_S32_LE));
        Assert.Equal(4, SpaAudioFormats.GetSampleSize(spa_audio_format.SPA_AUDIO_FORMAT_F32_LE));
        Assert.Equal(8, SpaAudioFormats.GetSampleSize(spa_audio_format.SPA_AUDIO_FORMAT_F64_LE));
        Assert.Equal(0, SpaAudioFormats.GetSampleSize(spa_audio_format.SPA_AUDIO_FORMAT_UNKNOWN));
    }

    private static string Utf8OrEmpty(sbyte* value)
        => value is null ? string.Empty : Marshal.PtrToStringUTF8((IntPtr)value) ?? string.Empty;

    private static int SizeOf(string name) => name switch
    {
        nameof(timespec) => sizeof(timespec),
        nameof(spa_pod) => sizeof(spa_pod),
        nameof(spa_pod_bool) => sizeof(spa_pod_bool),
        nameof(spa_pod_int) => sizeof(spa_pod_int),
        nameof(spa_pod_long) => sizeof(spa_pod_long),
        nameof(spa_pod_double) => sizeof(spa_pod_double),
        nameof(spa_pod_rectangle) => sizeof(spa_pod_rectangle),
        nameof(spa_pod_fraction) => sizeof(spa_pod_fraction),
        nameof(spa_pod_array) => sizeof(spa_pod_array),
        nameof(spa_pod_array_body) => sizeof(spa_pod_array_body),
        nameof(spa_pod_choice) => sizeof(spa_pod_choice),
        nameof(spa_pod_choice_body) => sizeof(spa_pod_choice_body),
        nameof(spa_pod_object) => sizeof(spa_pod_object),
        nameof(spa_pod_object_body) => sizeof(spa_pod_object_body),
        nameof(spa_pod_prop) => sizeof(spa_pod_prop),
        nameof(spa_pod_control) => sizeof(spa_pod_control),
        nameof(spa_pod_sequence) => sizeof(spa_pod_sequence),
        nameof(spa_pod_sequence_body) => sizeof(spa_pod_sequence_body),
        nameof(spa_dict) => sizeof(spa_dict),
        nameof(spa_dict_item) => sizeof(spa_dict_item),
        nameof(spa_interface) => sizeof(spa_interface),
        nameof(spa_callbacks) => sizeof(spa_callbacks),
        nameof(spa_hook) => sizeof(spa_hook),
        nameof(spa_list) => sizeof(spa_list),
        nameof(spa_rectangle) => sizeof(spa_rectangle),
        nameof(spa_fraction) => sizeof(spa_fraction),
        nameof(spa_buffer) => sizeof(spa_buffer),
        nameof(spa_data) => sizeof(spa_data),
        nameof(spa_chunk) => sizeof(spa_chunk),
        nameof(spa_audio_info_raw) => sizeof(spa_audio_info_raw),
        nameof(spa_video_info_raw) => sizeof(spa_video_info_raw),
        nameof(spa_point) => sizeof(spa_point),
        nameof(spa_region) => sizeof(spa_region),
        nameof(spa_meta) => sizeof(spa_meta),
        nameof(spa_meta_header) => sizeof(spa_meta_header),
        nameof(spa_meta_region) => sizeof(spa_meta_region),
        nameof(spa_meta_bitmap) => sizeof(spa_meta_bitmap),
        nameof(spa_meta_cursor) => sizeof(spa_meta_cursor),
        nameof(spa_meta_busy) => sizeof(spa_meta_busy),
        nameof(spa_meta_videotransform) => sizeof(spa_meta_videotransform),
        nameof(spa_meta_sync_timeline) => sizeof(spa_meta_sync_timeline),
        nameof(spa_param_info) => sizeof(spa_param_info),
        nameof(pw_buffer) => sizeof(pw_buffer),
        nameof(pw_time) => sizeof(pw_time),
        nameof(pw_properties) => sizeof(pw_properties),
        nameof(pw_core_events) => sizeof(pw_core_events),
        nameof(pw_core_methods) => sizeof(pw_core_methods),
        nameof(pw_registry_events) => sizeof(pw_registry_events),
        nameof(pw_registry_methods) => sizeof(pw_registry_methods),
        nameof(pw_node_events) => sizeof(pw_node_events),
        nameof(pw_node_methods) => sizeof(pw_node_methods),
        nameof(pw_stream_events) => sizeof(pw_stream_events),
        nameof(pw_filter_events) => sizeof(pw_filter_events),
        nameof(pw_proxy_events) => sizeof(pw_proxy_events),
        nameof(spa_loop_control_methods) => sizeof(spa_loop_control_methods),
        _ => throw new ArgumentOutOfRangeException(nameof(name), name, "Add the type to SizeOf as well as to ExpectedSizes."),
    };
}
