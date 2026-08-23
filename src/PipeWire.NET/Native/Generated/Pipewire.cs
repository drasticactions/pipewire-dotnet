#pragma warning disable CS1591

using System;
using System.Runtime.InteropServices;
using static PipeWire.Native.spa_direction;

namespace PipeWire.Native;

public static unsafe partial class Pipewire
{
    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int spa_handle_factory_enum([NativeTypeName("const struct spa_handle_factory **")] spa_handle_factory** factory, [NativeTypeName("uint32_t *")] uint* index);

    public const uint SPA_TYPE_START = 0x00000;
    public const uint SPA_TYPE_None = 1;
    public const uint SPA_TYPE_Bool = 2;
    public const uint SPA_TYPE_Id = 3;
    public const uint SPA_TYPE_Int = 4;
    public const uint SPA_TYPE_Long = 5;
    public const uint SPA_TYPE_Float = 6;
    public const uint SPA_TYPE_Double = 7;
    public const uint SPA_TYPE_String = 8;
    public const uint SPA_TYPE_Bytes = 9;
    public const uint SPA_TYPE_Rectangle = 10;
    public const uint SPA_TYPE_Fraction = 11;
    public const uint SPA_TYPE_Bitmap = 12;
    public const uint SPA_TYPE_Array = 13;
    public const uint SPA_TYPE_Struct = 14;
    public const uint SPA_TYPE_Object = 15;
    public const uint SPA_TYPE_Sequence = 16;
    public const uint SPA_TYPE_Pointer = 17;
    public const uint SPA_TYPE_Fd = 18;
    public const uint SPA_TYPE_Choice = 19;
    public const uint SPA_TYPE_Pod = 20;
    public const uint _SPA_TYPE_LAST = 21;
    public const uint SPA_TYPE_POINTER_START = 0x10000;
    public const uint SPA_TYPE_POINTER_Buffer = 65537;
    public const uint SPA_TYPE_POINTER_Meta = 65538;
    public const uint SPA_TYPE_POINTER_Dict = 65539;
    public const uint _SPA_TYPE_POINTER_LAST = 65540;
    public const uint SPA_TYPE_EVENT_START = 0x20000;
    public const uint SPA_TYPE_EVENT_Device = 131073;
    public const uint SPA_TYPE_EVENT_Node = 131074;
    public const uint _SPA_TYPE_EVENT_LAST = 131075;
    public const uint SPA_TYPE_COMMAND_START = 0x30000;
    public const uint SPA_TYPE_COMMAND_Device = 196609;
    public const uint SPA_TYPE_COMMAND_Node = 196610;
    public const uint _SPA_TYPE_COMMAND_LAST = 196611;
    public const uint SPA_TYPE_OBJECT_START = 0x40000;
    public const uint SPA_TYPE_OBJECT_PropInfo = 262145;
    public const uint SPA_TYPE_OBJECT_Props = 262146;
    public const uint SPA_TYPE_OBJECT_Format = 262147;
    public const uint SPA_TYPE_OBJECT_ParamBuffers = 262148;
    public const uint SPA_TYPE_OBJECT_ParamMeta = 262149;
    public const uint SPA_TYPE_OBJECT_ParamIO = 262150;
    public const uint SPA_TYPE_OBJECT_ParamProfile = 262151;
    public const uint SPA_TYPE_OBJECT_ParamPortConfig = 262152;
    public const uint SPA_TYPE_OBJECT_ParamRoute = 262153;
    public const uint SPA_TYPE_OBJECT_Profiler = 262154;
    public const uint SPA_TYPE_OBJECT_ParamLatency = 262155;
    public const uint SPA_TYPE_OBJECT_ParamProcessLatency = 262156;
    public const uint SPA_TYPE_OBJECT_ParamTag = 262157;
    public const uint SPA_TYPE_OBJECT_PeerParam = 262158;
    public const uint SPA_TYPE_OBJECT_ParamDict = 262159;
    public const uint _SPA_TYPE_OBJECT_LAST = 262160;
    public const uint SPA_TYPE_VENDOR_PipeWire = 0x02000000;
    public const uint SPA_TYPE_VENDOR_Other = 0x7f000000;

    public const uint PW_TYPE_FIRST = SPA_TYPE_VENDOR_PipeWire;

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct spa_type_info *")]
    public static extern spa_type_info* pw_type_info();

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_properties *")]
    public static extern pw_properties* pw_properties_new([NativeTypeName("const char *")] sbyte* key, __arglist);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_properties *")]
    public static extern pw_properties* pw_properties_new_dict([NativeTypeName("const struct spa_dict *")] spa_dict* dict);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_properties *")]
    public static extern pw_properties* pw_properties_new_string([NativeTypeName("const char *")] sbyte* args);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_properties *")]
    public static extern pw_properties* pw_properties_new_string_checked([NativeTypeName("const char *")] sbyte* args, [NativeTypeName("size_t")] nuint size, [NativeTypeName("struct spa_error_location *")] spa_error_location* loc);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_properties *")]
    public static extern pw_properties* pw_properties_copy([NativeTypeName("const struct pw_properties *")] pw_properties* properties);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_update_keys([NativeTypeName("struct pw_properties *")] pw_properties* props, [NativeTypeName("const struct spa_dict *")] spa_dict* dict, [NativeTypeName("const char *const[]")] sbyte** keys);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_update_ignore([NativeTypeName("struct pw_properties *")] pw_properties* props, [NativeTypeName("const struct spa_dict *")] spa_dict* dict, [NativeTypeName("const char *const[]")] sbyte** ignore);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_update([NativeTypeName("struct pw_properties *")] pw_properties* props, [NativeTypeName("const struct spa_dict *")] spa_dict* dict);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_update_string([NativeTypeName("struct pw_properties *")] pw_properties* props, [NativeTypeName("const char *")] sbyte* str, [NativeTypeName("size_t")] nuint size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_update_string_checked([NativeTypeName("struct pw_properties *")] pw_properties* props, [NativeTypeName("const char *")] sbyte* str, [NativeTypeName("size_t")] nuint size, [NativeTypeName("struct spa_error_location *")] spa_error_location* loc);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_add([NativeTypeName("struct pw_properties *")] pw_properties* oldprops, [NativeTypeName("const struct spa_dict *")] spa_dict* dict);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_add_keys([NativeTypeName("struct pw_properties *")] pw_properties* oldprops, [NativeTypeName("const struct spa_dict *")] spa_dict* dict, [NativeTypeName("const char *const[]")] sbyte** keys);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_properties_clear([NativeTypeName("struct pw_properties *")] pw_properties* properties);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_properties_free([NativeTypeName("struct pw_properties *")] pw_properties* properties);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_set([NativeTypeName("struct pw_properties *")] pw_properties* properties, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("const char *")] sbyte* value);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_setf([NativeTypeName("struct pw_properties *")] pw_properties* properties, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("const char *")] sbyte* format, __arglist);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_setva([NativeTypeName("struct pw_properties *")] pw_properties* properties, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("const char *")] sbyte* format, [NativeTypeName("va_list")] void* args);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_properties_get([NativeTypeName("const struct pw_properties *")] pw_properties* properties, [NativeTypeName("const char *")] sbyte* key);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_fetch_uint32([NativeTypeName("const struct pw_properties *")] pw_properties* properties, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("uint32_t *")] uint* value);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_fetch_int32([NativeTypeName("const struct pw_properties *")] pw_properties* properties, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("int32_t *")] int* value);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_fetch_uint64([NativeTypeName("const struct pw_properties *")] pw_properties* properties, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("uint64_t *")] ulong* value);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_fetch_int64([NativeTypeName("const struct pw_properties *")] pw_properties* properties, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("int64_t *")] long* value);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_fetch_bool([NativeTypeName("const struct pw_properties *")] pw_properties* properties, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("_Bool *")] bool* value);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_properties_iterate([NativeTypeName("const struct pw_properties *")] pw_properties* properties, void** state);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_properties_serialize_dict([NativeTypeName("FILE *")] _IO_FILE* f, [NativeTypeName("const struct spa_dict *")] spa_dict* dict, [NativeTypeName("uint32_t")] uint flags);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_core_info *")]
    public static extern pw_core_info* pw_core_info_update([NativeTypeName("struct pw_core_info *")] pw_core_info* info, [NativeTypeName("const struct pw_core_info *")] pw_core_info* update);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_core_info *")]
    public static extern pw_core_info* pw_core_info_merge([NativeTypeName("struct pw_core_info *")] pw_core_info* info, [NativeTypeName("const struct pw_core_info *")] pw_core_info* update, [NativeTypeName("_Bool")] byte reset);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_core_info_free([NativeTypeName("struct pw_core_info *")] pw_core_info* info);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_core *")]
    public static extern pw_core* pw_context_connect([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("struct pw_properties *")] pw_properties* properties, [NativeTypeName("size_t")] nuint user_data_size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_core *")]
    public static extern pw_core* pw_context_connect_fd([NativeTypeName("struct pw_context *")] pw_context* context, int fd, [NativeTypeName("struct pw_properties *")] pw_properties* properties, [NativeTypeName("size_t")] nuint user_data_size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_core *")]
    public static extern pw_core* pw_context_connect_self([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("struct pw_properties *")] pw_properties* properties, [NativeTypeName("size_t")] nuint user_data_size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_core_steal_fd([NativeTypeName("struct pw_core *")] pw_core* core);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_core_set_paused([NativeTypeName("struct pw_core *")] pw_core* core, [NativeTypeName("_Bool")] byte paused);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_core_disconnect([NativeTypeName("struct pw_core *")] pw_core* core);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* pw_core_get_user_data([NativeTypeName("struct pw_core *")] pw_core* core);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_client *")]
    public static extern pw_client* pw_core_get_client([NativeTypeName("struct pw_core *")] pw_core* core);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_context *")]
    public static extern pw_context* pw_core_get_context([NativeTypeName("struct pw_core *")] pw_core* core);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct pw_properties *")]
    public static extern pw_properties* pw_core_get_properties([NativeTypeName("struct pw_core *")] pw_core* core);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_core_update_properties([NativeTypeName("struct pw_core *")] pw_core* core, [NativeTypeName("const struct spa_dict *")] spa_dict* dict);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_mempool *")]
    public static extern pw_mempool* pw_core_get_mempool([NativeTypeName("struct pw_core *")] pw_core* core);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_proxy *")]
    public static extern pw_proxy* pw_core_find_proxy([NativeTypeName("struct pw_core *")] pw_core* core, [NativeTypeName("uint32_t")] uint id);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_proxy *")]
    public static extern pw_proxy* pw_core_export([NativeTypeName("struct pw_core *")] pw_core* core, [NativeTypeName("const char *")] sbyte* type, [NativeTypeName("const struct spa_dict *")] spa_dict* props, void* @object, [NativeTypeName("size_t")] nuint user_data_size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_loop *")]
    public static extern pw_loop* pw_loop_new([NativeTypeName("const struct spa_dict *")] spa_dict* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_loop_destroy([NativeTypeName("struct pw_loop *")] pw_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_loop_set_name([NativeTypeName("struct pw_loop *")] pw_loop* loop, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_context *")]
    public static extern pw_context* pw_context_new([NativeTypeName("struct pw_loop *")] pw_loop* main_loop, [NativeTypeName("struct pw_properties *")] pw_properties* props, [NativeTypeName("size_t")] nuint user_data_size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_context_destroy([NativeTypeName("struct pw_context *")] pw_context* context);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* pw_context_get_user_data([NativeTypeName("struct pw_context *")] pw_context* context);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_context_add_listener([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("struct spa_hook *")] spa_hook* listener, [NativeTypeName("const struct pw_context_events *")] pw_context_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct pw_properties *")]
    public static extern pw_properties* pw_context_get_properties([NativeTypeName("struct pw_context *")] pw_context* context);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_context_update_properties([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const struct spa_dict *")] spa_dict* dict);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_context_get_conf_section([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* section);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_context_parse_conf_section([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("struct pw_properties *")] pw_properties* conf, [NativeTypeName("const char *")] sbyte* section);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_context_conf_update_props([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* section, [NativeTypeName("struct pw_properties *")] pw_properties* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_context_conf_section_for_each([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* section, [NativeTypeName("int (*)(void *, const char *, const char *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, nuint, int> callback, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_context_conf_section_match_rules([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* section, [NativeTypeName("const struct spa_dict *")] spa_dict* props, [NativeTypeName("int (*)(void *, const char *, const char *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, nuint, int> callback, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct spa_support *")]
    public static extern spa_support* pw_context_get_support([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("uint32_t *")] uint* n_support);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_loop *")]
    public static extern pw_loop* pw_context_get_main_loop([NativeTypeName("struct pw_context *")] pw_context* context);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_data_loop *")]
    public static extern pw_data_loop* pw_context_get_data_loop([NativeTypeName("struct pw_context *")] pw_context* context);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_loop *")]
    public static extern pw_loop* pw_context_acquire_loop([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const struct spa_dict *")] spa_dict* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_context_release_loop([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("struct pw_loop *")] pw_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_work_queue *")]
    public static extern pw_work_queue* pw_context_get_work_queue([NativeTypeName("struct pw_context *")] pw_context* context);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_timer_queue *")]
    public static extern pw_timer_queue* pw_context_get_timer_queue([NativeTypeName("struct pw_context *")] pw_context* context);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_mempool *")]
    public static extern pw_mempool* pw_context_get_mempool([NativeTypeName("struct pw_context *")] pw_context* context);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_context_for_each_global([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("int (*)(void *, struct pw_global *)")] delegate* unmanaged[Cdecl]<void*, pw_global*, int> callback, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_global *")]
    public static extern pw_global* pw_context_find_global([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("uint32_t")] uint id);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_context_add_spa_lib([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* factory_regex, [NativeTypeName("const char *")] sbyte* lib);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_context_find_spa_lib([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* factory_name);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct spa_handle *")]
    public static extern spa_handle* pw_context_load_spa_handle([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* factory_name, [NativeTypeName("const struct spa_dict *")] spa_dict* info);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_context_register_export_type([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("struct pw_export_type *")] pw_export_type* type);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct pw_export_type *")]
    public static extern pw_export_type* pw_context_find_export_type([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* type);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_context_set_object([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* type, void* value);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* pw_context_get_object([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* type);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_split_walk([NativeTypeName("const char *")] sbyte* str, [NativeTypeName("const char *")] sbyte* delimiter, [NativeTypeName("size_t *")] nuint* len, [NativeTypeName("const char **")] sbyte** state);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char **")]
    public static extern sbyte** pw_split_strv([NativeTypeName("const char *")] sbyte* str, [NativeTypeName("const char *")] sbyte* delimiter, int max_tokens, int* n_tokens);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_split_ip([NativeTypeName("char *")] sbyte* str, [NativeTypeName("const char *")] sbyte* delimiter, int max_tokens, [NativeTypeName("char *[]")] sbyte** tokens);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char **")]
    public static extern sbyte** pw_strv_parse([NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint len, int max_tokens, int* n_tokens);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_strv_find([NativeTypeName("char **")] sbyte** a, [NativeTypeName("const char *")] sbyte* b);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_strv_find_common([NativeTypeName("char **")] sbyte** a, [NativeTypeName("char **")] sbyte** b);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_free_strv([NativeTypeName("char **")] sbyte** str);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* pw_strip([NativeTypeName("char *")] sbyte* str, [NativeTypeName("const char *")] sbyte* whitespace);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("ssize_t")]
    public static extern nint pw_getrandom(void* buf, [NativeTypeName("size_t")] nuint buflen, [NativeTypeName("unsigned int")] uint flags);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_random(void* buf, [NativeTypeName("size_t")] nuint buflen);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* pw_reallocarray(void* ptr, [NativeTypeName("size_t")] nuint nmemb, [NativeTypeName("size_t")] nuint size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_protocol *")]
    public static extern pw_protocol* pw_protocol_new([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("size_t")] nuint user_data_size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_protocol_destroy([NativeTypeName("struct pw_protocol *")] pw_protocol* protocol);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_context *")]
    public static extern pw_context* pw_protocol_get_context([NativeTypeName("struct pw_protocol *")] pw_protocol* protocol);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* pw_protocol_get_user_data([NativeTypeName("struct pw_protocol *")] pw_protocol* protocol);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct pw_protocol_implementation *")]
    public static extern pw_protocol_implementation* pw_protocol_get_implementation([NativeTypeName("struct pw_protocol *")] pw_protocol* protocol);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const void *")]
    public static extern void* pw_protocol_get_extension([NativeTypeName("struct pw_protocol *")] pw_protocol* protocol);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_protocol_add_listener([NativeTypeName("struct pw_protocol *")] pw_protocol* protocol, [NativeTypeName("struct spa_hook *")] spa_hook* listener, [NativeTypeName("const struct pw_protocol_events *")] pw_protocol_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_protocol_add_marshal([NativeTypeName("struct pw_protocol *")] pw_protocol* protocol, [NativeTypeName("const struct pw_protocol_marshal *")] pw_protocol_marshal* marshal);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct pw_protocol_marshal *")]
    public static extern pw_protocol_marshal* pw_protocol_get_marshal([NativeTypeName("struct pw_protocol *")] pw_protocol* protocol, [NativeTypeName("const char *")] sbyte* type, [NativeTypeName("uint32_t")] uint version, [NativeTypeName("uint32_t")] uint flags);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_protocol *")]
    public static extern pw_protocol* pw_context_find_protocol([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("const char *")] sbyte* name);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_proxy *")]
    public static extern pw_proxy* pw_proxy_new([NativeTypeName("struct pw_proxy *")] pw_proxy* factory, [NativeTypeName("const char *")] sbyte* type, [NativeTypeName("uint32_t")] uint version, [NativeTypeName("size_t")] nuint user_data_size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_proxy_add_listener([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy, [NativeTypeName("struct spa_hook *")] spa_hook* listener, [NativeTypeName("const struct pw_proxy_events *")] pw_proxy_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_proxy_add_object_listener([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy, [NativeTypeName("struct spa_hook *")] spa_hook* listener, [NativeTypeName("const void *")] void* funcs, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_proxy_destroy([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_proxy_ref([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_proxy_unref([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* pw_proxy_get_user_data([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint pw_proxy_get_id([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_proxy_get_type([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy, [NativeTypeName("uint32_t *")] uint* version);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_protocol *")]
    public static extern pw_protocol* pw_proxy_get_protocol([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_proxy_sync([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy, int seq);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_proxy_set_bound_id([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy, [NativeTypeName("uint32_t")] uint global_id);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint pw_proxy_get_bound_id([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_proxy_error([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy, int res, [NativeTypeName("const char *")] sbyte* error);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_proxy_errorf([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy, int res, [NativeTypeName("const char *")] sbyte* error, __arglist);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct spa_hook_list *")]
    public static extern spa_hook_list* pw_proxy_get_object_listeners([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct pw_protocol_marshal *")]
    public static extern pw_protocol_marshal* pw_proxy_get_marshal([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_proxy_install_marshal([NativeTypeName("struct pw_proxy *")] pw_proxy* proxy, [NativeTypeName("_Bool")] byte implementor);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_client_info *")]
    public static extern pw_client_info* pw_client_info_update([NativeTypeName("struct pw_client_info *")] pw_client_info* info, [NativeTypeName("const struct pw_client_info *")] pw_client_info* update);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_client_info *")]
    public static extern pw_client_info* pw_client_info_merge([NativeTypeName("struct pw_client_info *")] pw_client_info* info, [NativeTypeName("const struct pw_client_info *")] pw_client_info* update, [NativeTypeName("_Bool")] byte reset);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_client_info_free([NativeTypeName("struct pw_client_info *")] pw_client_info* info);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_conf_load_conf_for_context([NativeTypeName("struct pw_properties *")] pw_properties* props, [NativeTypeName("struct pw_properties *")] pw_properties* conf);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_conf_load_conf([NativeTypeName("const char *")] sbyte* prefix, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("struct pw_properties *")] pw_properties* conf);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_conf_load_state([NativeTypeName("const char *")] sbyte* prefix, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("struct pw_properties *")] pw_properties* conf);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_conf_save_state([NativeTypeName("const char *")] sbyte* prefix, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const struct pw_properties *")] pw_properties* conf);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern byte pw_conf_find_match([NativeTypeName("struct spa_json *")] spa_json* arr, [NativeTypeName("const struct spa_dict *")] spa_dict* props, [NativeTypeName("_Bool")] byte condition);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_conf_section_update_props([NativeTypeName("const struct spa_dict *")] spa_dict* conf, [NativeTypeName("const char *")] sbyte* section, [NativeTypeName("struct pw_properties *")] pw_properties* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_conf_section_update_props_rules([NativeTypeName("const struct spa_dict *")] spa_dict* conf, [NativeTypeName("const struct spa_dict *")] spa_dict* context, [NativeTypeName("const char *")] sbyte* section, [NativeTypeName("struct pw_properties *")] pw_properties* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_conf_section_for_each([NativeTypeName("const struct spa_dict *")] spa_dict* conf, [NativeTypeName("const char *")] sbyte* section, [NativeTypeName("int (*)(void *, const char *, const char *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, nuint, int> callback, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_conf_match_rules([NativeTypeName("const char *")] sbyte* str, [NativeTypeName("size_t")] nuint len, [NativeTypeName("const char *")] sbyte* location, [NativeTypeName("const struct spa_dict *")] spa_dict* props, [NativeTypeName("int (*)(void *, const char *, const char *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, nuint, int> callback, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_conf_section_match_rules([NativeTypeName("const struct spa_dict *")] spa_dict* conf, [NativeTypeName("const char *")] sbyte* section, [NativeTypeName("const struct spa_dict *")] spa_dict* props, [NativeTypeName("int (*)(void *, const char *, const char *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte*, sbyte*, nuint, int> callback, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_device_info *")]
    public static extern pw_device_info* pw_device_info_update([NativeTypeName("struct pw_device_info *")] pw_device_info* info, [NativeTypeName("const struct pw_device_info *")] pw_device_info* update);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_device_info *")]
    public static extern pw_device_info* pw_device_info_merge([NativeTypeName("struct pw_device_info *")] pw_device_info* info, [NativeTypeName("const struct pw_device_info *")] pw_device_info* update, [NativeTypeName("_Bool")] byte reset);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_device_info_free([NativeTypeName("struct pw_device_info *")] pw_device_info* info);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_mempool *")]
    public static extern pw_mempool* pw_mempool_new([NativeTypeName("struct pw_properties *")] pw_properties* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_mempool_add_listener([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, [NativeTypeName("struct spa_hook *")] spa_hook* listener, [NativeTypeName("const struct pw_mempool_events *")] pw_mempool_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_mempool_clear([NativeTypeName("struct pw_mempool *")] pw_mempool* pool);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_mempool_destroy([NativeTypeName("struct pw_mempool *")] pw_mempool* pool);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_memblock *")]
    public static extern pw_memblock* pw_mempool_alloc([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, [NativeTypeName("enum pw_memblock_flags")] pw_memblock_flags flags, [NativeTypeName("uint32_t")] uint type, [NativeTypeName("size_t")] nuint size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_memblock *")]
    public static extern pw_memblock* pw_mempool_import_block([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, [NativeTypeName("struct pw_memblock *")] pw_memblock* mem);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_memblock *")]
    public static extern pw_memblock* pw_mempool_import([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, [NativeTypeName("enum pw_memblock_flags")] pw_memblock_flags flags, [NativeTypeName("uint32_t")] uint type, int fd);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_memblock_free([NativeTypeName("struct pw_memblock *")] pw_memblock* mem);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_mempool_remove_id([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, [NativeTypeName("uint32_t")] uint id);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_memblock *")]
    public static extern pw_memblock* pw_mempool_find_ptr([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, [NativeTypeName("const void *")] void* ptr);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_memblock *")]
    public static extern pw_memblock* pw_mempool_find_id([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, [NativeTypeName("uint32_t")] uint id);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_memblock *")]
    public static extern pw_memblock* pw_mempool_find_fd([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, int fd);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_memmap *")]
    public static extern pw_memmap* pw_memblock_map([NativeTypeName("struct pw_memblock *")] pw_memblock* block, [NativeTypeName("enum pw_memmap_flags")] pw_memmap_flags flags, [NativeTypeName("uint32_t")] uint offset, [NativeTypeName("uint32_t")] uint size, [NativeTypeName("uint32_t[5]")] uint* tag);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_memmap *")]
    public static extern pw_memmap* pw_mempool_map_id([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, [NativeTypeName("uint32_t")] uint id, [NativeTypeName("enum pw_memmap_flags")] pw_memmap_flags flags, [NativeTypeName("uint32_t")] uint offset, [NativeTypeName("uint32_t")] uint size, [NativeTypeName("uint32_t[5]")] uint* tag);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_memmap *")]
    public static extern pw_memmap* pw_mempool_import_map([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, [NativeTypeName("struct pw_mempool *")] pw_mempool* other, void* data, [NativeTypeName("uint32_t")] uint size, [NativeTypeName("uint32_t[5]")] uint* tag);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_memmap *")]
    public static extern pw_memmap* pw_mempool_find_tag([NativeTypeName("struct pw_mempool *")] pw_mempool* pool, [NativeTypeName("uint32_t[5]")] uint* tag, [NativeTypeName("size_t")] nuint size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_memmap_free([NativeTypeName("struct pw_memmap *")] pw_memmap* map);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_buffers_negotiate([NativeTypeName("struct pw_context *")] pw_context* context, [NativeTypeName("uint32_t")] uint flags, [NativeTypeName("struct spa_node *")] spa_node* outnode, [NativeTypeName("uint32_t")] uint out_port_id, [NativeTypeName("struct spa_node *")] spa_node* innode, [NativeTypeName("uint32_t")] uint in_port_id, [NativeTypeName("struct pw_buffers *")] pw_buffers* result);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_buffers_clear([NativeTypeName("struct pw_buffers *")] pw_buffers* buffers);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_factory_info *")]
    public static extern pw_factory_info* pw_factory_info_update([NativeTypeName("struct pw_factory_info *")] pw_factory_info* info, [NativeTypeName("const struct pw_factory_info *")] pw_factory_info* update);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_factory_info *")]
    public static extern pw_factory_info* pw_factory_info_merge([NativeTypeName("struct pw_factory_info *")] pw_factory_info* info, [NativeTypeName("const struct pw_factory_info *")] pw_factory_info* update, [NativeTypeName("_Bool")] byte reset);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_factory_info_free([NativeTypeName("struct pw_factory_info *")] pw_factory_info* info);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_log_set([NativeTypeName("struct spa_log *")] spa_log* log);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct spa_log *")]
    public static extern spa_log* pw_log_get();

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_log_set_level([NativeTypeName("enum spa_log_level")] spa_log_level level);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_log_set_level_string([NativeTypeName("const char *")] sbyte* str);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_log_logt([NativeTypeName("enum spa_log_level")] spa_log_level level, [NativeTypeName("const struct spa_log_topic *")] spa_log_topic* topic, [NativeTypeName("const char *")] sbyte* file, int line, [NativeTypeName("const char *")] sbyte* func, [NativeTypeName("const char *")] sbyte* fmt, __arglist);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_log_logtv([NativeTypeName("enum spa_log_level")] spa_log_level level, [NativeTypeName("const struct spa_log_topic *")] spa_log_topic* topic, [NativeTypeName("const char *")] sbyte* file, int line, [NativeTypeName("const char *")] sbyte* func, [NativeTypeName("const char *")] sbyte* fmt, [NativeTypeName("va_list")] void* args);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_log_log([NativeTypeName("enum spa_log_level")] spa_log_level level, [NativeTypeName("const char *")] sbyte* file, int line, [NativeTypeName("const char *")] sbyte* func, [NativeTypeName("const char *")] sbyte* fmt, __arglist);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_log_logv([NativeTypeName("enum spa_log_level")] spa_log_level level, [NativeTypeName("const char *")] sbyte* file, int line, [NativeTypeName("const char *")] sbyte* func, [NativeTypeName("const char *")] sbyte* fmt, [NativeTypeName("va_list")] void* args);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_log_topic_register([NativeTypeName("struct spa_log_topic *")] spa_log_topic* t);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_log_topic_unregister([NativeTypeName("struct spa_log_topic *")] spa_log_topic* t);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_link_state_as_string([NativeTypeName("enum pw_link_state")] pw_link_state state);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_link_info *")]
    public static extern pw_link_info* pw_link_info_update([NativeTypeName("struct pw_link_info *")] pw_link_info* info, [NativeTypeName("const struct pw_link_info *")] pw_link_info* update);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_link_info *")]
    public static extern pw_link_info* pw_link_info_merge([NativeTypeName("struct pw_link_info *")] pw_link_info* info, [NativeTypeName("const struct pw_link_info *")] pw_link_info* update, [NativeTypeName("_Bool")] byte reset);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_link_info_free([NativeTypeName("struct pw_link_info *")] pw_link_info* info);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_main_loop *")]
    public static extern pw_main_loop* pw_main_loop_new([NativeTypeName("const struct spa_dict *")] spa_dict* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_main_loop_add_listener([NativeTypeName("struct pw_main_loop *")] pw_main_loop* loop, [NativeTypeName("struct spa_hook *")] spa_hook* listener, [NativeTypeName("const struct pw_main_loop_events *")] pw_main_loop_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_loop *")]
    public static extern pw_loop* pw_main_loop_get_loop([NativeTypeName("struct pw_main_loop *")] pw_main_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_main_loop_destroy([NativeTypeName("struct pw_main_loop *")] pw_main_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_main_loop_run([NativeTypeName("struct pw_main_loop *")] pw_main_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_main_loop_quit([NativeTypeName("struct pw_main_loop *")] pw_main_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_module_info *")]
    public static extern pw_module_info* pw_module_info_update([NativeTypeName("struct pw_module_info *")] pw_module_info* info, [NativeTypeName("const struct pw_module_info *")] pw_module_info* update);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_module_info *")]
    public static extern pw_module_info* pw_module_info_merge([NativeTypeName("struct pw_module_info *")] pw_module_info* info, [NativeTypeName("const struct pw_module_info *")] pw_module_info* update, [NativeTypeName("_Bool")] byte reset);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_module_info_free([NativeTypeName("struct pw_module_info *")] pw_module_info* info);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_node_state_as_string([NativeTypeName("enum pw_node_state")] pw_node_state state);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_node_info *")]
    public static extern pw_node_info* pw_node_info_update([NativeTypeName("struct pw_node_info *")] pw_node_info* info, [NativeTypeName("const struct pw_node_info *")] pw_node_info* update);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_node_info *")]
    public static extern pw_node_info* pw_node_info_merge([NativeTypeName("struct pw_node_info *")] pw_node_info* info, [NativeTypeName("const struct pw_node_info *")] pw_node_info* update, [NativeTypeName("_Bool")] byte reset);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_node_info_free([NativeTypeName("struct pw_node_info *")] pw_node_info* info);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_direction_as_string([NativeTypeName("enum spa_direction")] spa_direction direction);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_port_info *")]
    public static extern pw_port_info* pw_port_info_update([NativeTypeName("struct pw_port_info *")] pw_port_info* info, [NativeTypeName("const struct pw_port_info *")] pw_port_info* update);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_port_info *")]
    public static extern pw_port_info* pw_port_info_merge([NativeTypeName("struct pw_port_info *")] pw_port_info* info, [NativeTypeName("const struct pw_port_info *")] pw_port_info* update, [NativeTypeName("_Bool")] byte reset);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_port_info_free([NativeTypeName("struct pw_port_info *")] pw_port_info* info);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_stream_state_as_string([NativeTypeName("enum pw_stream_state")] pw_stream_state state);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_stream *")]
    public static extern pw_stream* pw_stream_new([NativeTypeName("struct pw_core *")] pw_core* core, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("struct pw_properties *")] pw_properties* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_stream *")]
    public static extern pw_stream* pw_stream_new_simple([NativeTypeName("struct pw_loop *")] pw_loop* loop, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("struct pw_properties *")] pw_properties* props, [NativeTypeName("const struct pw_stream_events *")] pw_stream_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_stream_destroy([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_stream_add_listener([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("struct spa_hook *")] spa_hook* listener, [NativeTypeName("const struct pw_stream_events *")] pw_stream_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("enum pw_stream_state")]
    public static extern pw_stream_state pw_stream_get_state([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("const char **")] sbyte** error);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_stream_get_name([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_core *")]
    public static extern pw_core* pw_stream_get_core([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct pw_properties *")]
    public static extern pw_properties* pw_stream_get_properties([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_update_properties([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("const struct spa_dict *")] spa_dict* dict);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_connect([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("enum spa_direction")] spa_direction direction, [NativeTypeName("uint32_t")] uint target_id, [NativeTypeName("enum pw_stream_flags")] pw_stream_flags flags, [NativeTypeName("const struct spa_pod **")] spa_pod** @params, [NativeTypeName("uint32_t")] uint n_params);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint pw_stream_get_node_id([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_disconnect([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_set_error([NativeTypeName("struct pw_stream *")] pw_stream* stream, int res, [NativeTypeName("const char *")] sbyte* error, __arglist);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_update_params([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("const struct spa_pod **")] spa_pod** @params, [NativeTypeName("uint32_t")] uint n_params);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_set_param([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("uint32_t")] uint id, [NativeTypeName("const struct spa_pod *")] spa_pod* param2);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct pw_stream_control *")]
    public static extern pw_stream_control* pw_stream_get_control([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("uint32_t")] uint id);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_set_control([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("uint32_t")] uint id, [NativeTypeName("uint32_t")] uint n_values, float* values, __arglist);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_get_time_n([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("struct pw_time *")] pw_time* time, [NativeTypeName("size_t")] nuint size);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong pw_stream_get_nsec([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_loop *")]
    public static extern pw_loop* pw_stream_get_data_loop([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [Obsolete]
    public static extern int pw_stream_get_time([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("struct pw_time *")] pw_time* time);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_buffer *")]
    public static extern pw_buffer* pw_stream_dequeue_buffer([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_queue_buffer([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("struct pw_buffer *")] pw_buffer* buffer);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_return_buffer([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("struct pw_buffer *")] pw_buffer* buffer);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_set_active([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("_Bool")] byte active);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_flush([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("_Bool")] byte drain);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern byte pw_stream_is_driving([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern byte pw_stream_is_lazy([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_trigger_process([NativeTypeName("struct pw_stream *")] pw_stream* stream);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_emit_event([NativeTypeName("struct pw_stream *")] pw_stream* stream, [NativeTypeName("const struct spa_event *")] spa_event* @event);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_stream_set_rate([NativeTypeName("struct pw_stream *")] pw_stream* stream, double rate);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_filter_state_as_string([NativeTypeName("enum pw_filter_state")] pw_filter_state state);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_filter *")]
    public static extern pw_filter* pw_filter_new([NativeTypeName("struct pw_core *")] pw_core* core, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("struct pw_properties *")] pw_properties* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_filter *")]
    public static extern pw_filter* pw_filter_new_simple([NativeTypeName("struct pw_loop *")] pw_loop* loop, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("struct pw_properties *")] pw_properties* props, [NativeTypeName("const struct pw_filter_events *")] pw_filter_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_filter_destroy([NativeTypeName("struct pw_filter *")] pw_filter* filter);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_filter_add_listener([NativeTypeName("struct pw_filter *")] pw_filter* filter, [NativeTypeName("struct spa_hook *")] spa_hook* listener, [NativeTypeName("const struct pw_filter_events *")] pw_filter_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("enum pw_filter_state")]
    public static extern pw_filter_state pw_filter_get_state([NativeTypeName("struct pw_filter *")] pw_filter* filter, [NativeTypeName("const char **")] sbyte** error);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_filter_get_name([NativeTypeName("struct pw_filter *")] pw_filter* filter);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_core *")]
    public static extern pw_core* pw_filter_get_core([NativeTypeName("struct pw_filter *")] pw_filter* filter);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_connect([NativeTypeName("struct pw_filter *")] pw_filter* filter, [NativeTypeName("enum pw_filter_flags")] pw_filter_flags flags, [NativeTypeName("const struct spa_pod **")] spa_pod** @params, [NativeTypeName("uint32_t")] uint n_params);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint pw_filter_get_node_id([NativeTypeName("struct pw_filter *")] pw_filter* filter);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_disconnect([NativeTypeName("struct pw_filter *")] pw_filter* filter);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* pw_filter_add_port([NativeTypeName("struct pw_filter *")] pw_filter* filter, [NativeTypeName("enum spa_direction")] spa_direction direction, [NativeTypeName("enum pw_filter_port_flags")] pw_filter_port_flags flags, [NativeTypeName("size_t")] nuint port_data_size, [NativeTypeName("struct pw_properties *")] pw_properties* props, [NativeTypeName("const struct spa_pod **")] spa_pod** @params, [NativeTypeName("uint32_t")] uint n_params);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_remove_port(void* port_data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const struct pw_properties *")]
    public static extern pw_properties* pw_filter_get_properties([NativeTypeName("struct pw_filter *")] pw_filter* filter, void* port_data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_update_properties([NativeTypeName("struct pw_filter *")] pw_filter* filter, void* port_data, [NativeTypeName("const struct spa_dict *")] spa_dict* dict);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_set_error([NativeTypeName("struct pw_filter *")] pw_filter* filter, int res, [NativeTypeName("const char *")] sbyte* error, __arglist);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_update_params([NativeTypeName("struct pw_filter *")] pw_filter* filter, void* port_data, [NativeTypeName("const struct spa_pod **")] spa_pod** @params, [NativeTypeName("uint32_t")] uint n_params);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [Obsolete]
    public static extern int pw_filter_get_time([NativeTypeName("struct pw_filter *")] pw_filter* filter, [NativeTypeName("struct pw_time *")] pw_time* time);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong pw_filter_get_nsec([NativeTypeName("struct pw_filter *")] pw_filter* filter);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_loop *")]
    public static extern pw_loop* pw_filter_get_data_loop([NativeTypeName("struct pw_filter *")] pw_filter* filter);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_buffer *")]
    public static extern pw_buffer* pw_filter_dequeue_buffer(void* port_data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_queue_buffer(void* port_data, [NativeTypeName("struct pw_buffer *")] pw_buffer* buffer);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void* pw_filter_get_dsp_buffer(void* port_data, [NativeTypeName("uint32_t")] uint n_samples);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_set_active([NativeTypeName("struct pw_filter *")] pw_filter* filter, [NativeTypeName("_Bool")] byte active);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_flush([NativeTypeName("struct pw_filter *")] pw_filter* filter, [NativeTypeName("_Bool")] byte drain);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern byte pw_filter_is_driving([NativeTypeName("struct pw_filter *")] pw_filter* filter);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern byte pw_filter_is_lazy([NativeTypeName("struct pw_filter *")] pw_filter* filter);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_trigger_process([NativeTypeName("struct pw_filter *")] pw_filter* filter);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_filter_emit_event([NativeTypeName("struct pw_filter *")] pw_filter* filter, [NativeTypeName("const struct spa_event *")] spa_event* @event);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_thread_loop *")]
    public static extern pw_thread_loop* pw_thread_loop_new([NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const struct spa_dict *")] spa_dict* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_thread_loop *")]
    public static extern pw_thread_loop* pw_thread_loop_new_full([NativeTypeName("struct pw_loop *")] pw_loop* loop, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const struct spa_dict *")] spa_dict* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_thread_loop_destroy([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_thread_loop_add_listener([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop, [NativeTypeName("struct spa_hook *")] spa_hook* listener, [NativeTypeName("const struct pw_thread_loop_events *")] pw_thread_loop_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_loop *")]
    public static extern pw_loop* pw_thread_loop_get_loop([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_thread_loop_start([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_thread_loop_stop([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_thread_loop_lock([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_thread_loop_unlock([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_thread_loop_wait([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_thread_loop_timed_wait([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop, int wait_max_sec);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_thread_loop_get_time([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop, [NativeTypeName("struct timespec *")] timespec* abstime, [NativeTypeName("int64_t")] long timeout);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_thread_loop_timed_wait_full([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop, [NativeTypeName("const struct timespec *")] timespec* abstime);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_thread_loop_signal([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop, [NativeTypeName("_Bool")] byte wait_for_accept);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_thread_loop_accept([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern byte pw_thread_loop_in_thread([NativeTypeName("struct pw_thread_loop *")] pw_thread_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_data_loop *")]
    public static extern pw_data_loop* pw_data_loop_new([NativeTypeName("const struct spa_dict *")] spa_dict* props);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_data_loop_add_listener([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop, [NativeTypeName("struct spa_hook *")] spa_hook* listener, [NativeTypeName("const struct pw_data_loop_events *")] pw_data_loop_events* events, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_data_loop_wait([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop, int timeout);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_data_loop_exit([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_loop *")]
    public static extern pw_loop* pw_data_loop_get_loop([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_data_loop_get_name([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_data_loop_get_class([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_data_loop_destroy([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_data_loop_start([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_data_loop_stop([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern byte pw_data_loop_in_thread([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct spa_thread *")]
    public static extern spa_thread* pw_data_loop_get_thread([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_data_loop_invoke([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop, [NativeTypeName("spa_invoke_func_t")] delegate* unmanaged[Cdecl]<spa_loop*, byte, uint, void*, nuint, void*, int> func, [NativeTypeName("uint32_t")] uint seq, [NativeTypeName("const void *")] void* data, [NativeTypeName("size_t")] nuint size, [NativeTypeName("_Bool")] byte block, void* user_data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_data_loop_set_thread_utils([NativeTypeName("struct pw_data_loop *")] pw_data_loop* loop, [NativeTypeName("struct spa_thread_utils *")] spa_thread_utils* impl);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct pw_timer_queue *")]
    public static extern pw_timer_queue* pw_timer_queue_new([NativeTypeName("struct pw_loop *")] pw_loop* loop);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_timer_queue_destroy([NativeTypeName("struct pw_timer_queue *")] pw_timer_queue* queue);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_timer_queue_add([NativeTypeName("struct pw_timer_queue *")] pw_timer_queue* queue, [NativeTypeName("struct pw_timer *")] pw_timer* timer, [NativeTypeName("struct timespec *")] timespec* abs_time, [NativeTypeName("int64_t")] long timeout_ns, [NativeTypeName("pw_timer_callback")] delegate* unmanaged[Cdecl]<void*, void> callback, void* data);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_timer_queue_cancel([NativeTypeName("struct pw_timer *")] pw_timer* timer);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_get_library_version();

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern byte pw_check_library_version(int major, int minor, int micro);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_init(int* argc, [NativeTypeName("char **[]")] sbyte*** argv);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void pw_deinit();

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern byte pw_debug_is_category_enabled([NativeTypeName("const char *")] sbyte* name);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_get_application_name();

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_get_prgname();

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_get_user_name();

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_get_host_name();

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_get_client_name();

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern byte pw_check_option([NativeTypeName("const char *")] sbyte* option, [NativeTypeName("const char *")] sbyte* value);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("enum spa_direction")]
    public static extern spa_direction pw_direction_reverse([NativeTypeName("enum spa_direction")] spa_direction direction);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_set_domain([NativeTypeName("const char *")] sbyte* domain);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* pw_get_domain();

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint pw_get_support([NativeTypeName("struct spa_support *")] spa_support* support, [NativeTypeName("uint32_t")] uint max_support);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("struct spa_handle *")]
    public static extern spa_handle* pw_load_spa_handle([NativeTypeName("const char *")] sbyte* lib, [NativeTypeName("const char *")] sbyte* factory_name, [NativeTypeName("const struct spa_dict *")] spa_dict* info, [NativeTypeName("uint32_t")] uint n_support, [NativeTypeName("const struct spa_support[]")] spa_support* support);

    [DllImport("pipewire-0.3", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int pw_unload_spa_handle([NativeTypeName("struct spa_handle *")] spa_handle* handle);

    [NativeTypeName("#define SPA_TIME_INVALID ((int64_t)INT64_MIN)")]
    public const long SPA_TIME_INVALID = long.MinValue;

    [NativeTypeName("#define SPA_IDX_INVALID ((unsigned int)-1)")]
    public const uint SPA_IDX_INVALID = unchecked((uint)(-1));

    [NativeTypeName("#define SPA_ID_INVALID ((uint32_t)0xffffffff)")]
    public const uint SPA_ID_INVALID = ((uint)(0xffffffff));

    [NativeTypeName("#define SPA_NSEC_PER_SEC (1000000000LL)")]
    public const long SPA_NSEC_PER_SEC = (1000000000L);

    [NativeTypeName("#define SPA_NSEC_PER_MSEC (1000000ll)")]
    public const long SPA_NSEC_PER_MSEC = (1000000L);

    [NativeTypeName("#define SPA_NSEC_PER_USEC (1000ll)")]
    public const long SPA_NSEC_PER_USEC = (1000L);

    [NativeTypeName("#define SPA_USEC_PER_SEC (1000000ll)")]
    public const long SPA_USEC_PER_SEC = (1000000L);

    [NativeTypeName("#define SPA_USEC_PER_MSEC (1000ll)")]
    public const long SPA_USEC_PER_MSEC = (1000L);

    [NativeTypeName("#define SPA_MSEC_PER_SEC (1000ll)")]
    public const long SPA_MSEC_PER_SEC = (1000L);

    [NativeTypeName("#define SPA_DICT_FLAG_SORTED (1<<0)")]
    public const int SPA_DICT_FLAG_SORTED = (1 << 0);

    [NativeTypeName("#define SPA_VERSION_HANDLE 0")]
    public const int SPA_VERSION_HANDLE = 0;

    [NativeTypeName("#define SPA_VERSION_HANDLE_FACTORY 1")]
    public const int SPA_VERSION_HANDLE_FACTORY = 1;

    [NativeTypeName("#define SPA_HANDLE_FACTORY_ENUM_FUNC_NAME \"spa_handle_factory_enum\"")]
    public static ReadOnlySpan<byte> SPA_HANDLE_FACTORY_ENUM_FUNC_NAME => "spa_handle_factory_enum"u8;

    [NativeTypeName("#define SPA_KEY_FACTORY_NAME \"factory.name\"")]
    public static ReadOnlySpan<byte> SPA_KEY_FACTORY_NAME => "factory.name"u8;

    [NativeTypeName("#define SPA_KEY_FACTORY_AUTHOR \"factory.author\"")]
    public static ReadOnlySpan<byte> SPA_KEY_FACTORY_AUTHOR => "factory.author"u8;

    [NativeTypeName("#define SPA_KEY_FACTORY_DESCRIPTION \"factory.description\"")]
    public static ReadOnlySpan<byte> SPA_KEY_FACTORY_DESCRIPTION => "factory.description"u8;

    [NativeTypeName("#define SPA_KEY_FACTORY_USAGE \"factory.usage\"")]
    public static ReadOnlySpan<byte> SPA_KEY_FACTORY_USAGE => "factory.usage"u8;

    [NativeTypeName("#define SPA_KEY_LIBRARY_NAME \"library.name\"")]
    public static ReadOnlySpan<byte> SPA_KEY_LIBRARY_NAME => "library.name"u8;

    [NativeTypeName("#define SPA_PARAM_INFO_SERIAL (1<<0)")]
    public const int SPA_PARAM_INFO_SERIAL = (1 << 0);

    [NativeTypeName("#define SPA_PARAM_INFO_READ (1<<1)")]
    public const int SPA_PARAM_INFO_READ = (1 << 1);

    [NativeTypeName("#define SPA_PARAM_INFO_WRITE (1<<2)")]
    public const int SPA_PARAM_INFO_WRITE = (1 << 2);

    [NativeTypeName("#define SPA_PARAM_INFO_READWRITE (SPA_PARAM_INFO_WRITE|SPA_PARAM_INFO_READ)")]
    public const int SPA_PARAM_INFO_READWRITE = ((1 << 2) | (1 << 1));

    [NativeTypeName("#define SPA_TYPE_INFO_BASE \"Spa:\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_BASE => "Spa:"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_Flags SPA_TYPE_INFO_BASE \"Flags\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_Flags => "Spa:Flags"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_FLAGS_BASE SPA_TYPE_INFO_Flags \":\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_FLAGS_BASE => "Spa:Flags:"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_Enum SPA_TYPE_INFO_BASE \"Enum\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_Enum => "Spa:Enum"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_ENUM_BASE SPA_TYPE_INFO_Enum \":\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_ENUM_BASE => "Spa:Enum:"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_Pod SPA_TYPE_INFO_BASE \"Pod\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_Pod => "Spa:Pod"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_POD_BASE SPA_TYPE_INFO_Pod \":\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_POD_BASE => "Spa:Pod:"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_Struct SPA_TYPE_INFO_POD_BASE \"Struct\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_Struct => "Spa:Pod:Struct"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_STRUCT_BASE SPA_TYPE_INFO_Struct \":\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_STRUCT_BASE => "Spa:Pod:Struct:"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_Object SPA_TYPE_INFO_POD_BASE \"Object\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_Object => "Spa:Pod:Object"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_OBJECT_BASE SPA_TYPE_INFO_Object \":\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_OBJECT_BASE => "Spa:Pod:Object:"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_Pointer SPA_TYPE_INFO_BASE \"Pointer\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_Pointer => "Spa:Pointer"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_POINTER_BASE SPA_TYPE_INFO_Pointer \":\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_POINTER_BASE => "Spa:Pointer:"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_Interface SPA_TYPE_INFO_POINTER_BASE \"Interface\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_Interface => "Spa:Pointer:Interface"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_INTERFACE_BASE SPA_TYPE_INFO_Interface \":\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_INTERFACE_BASE => "Spa:Pointer:Interface:"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_Event SPA_TYPE_INFO_OBJECT_BASE \"Event\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_Event => "Spa:Pod:Object:Event"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_EVENT_BASE SPA_TYPE_INFO_Event \":\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_EVENT_BASE => "Spa:Pod:Object:Event:"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_Command SPA_TYPE_INFO_OBJECT_BASE \"Command\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_Command => "Spa:Pod:Object:Command"u8;

    [NativeTypeName("#define SPA_TYPE_INFO_COMMAND_BASE SPA_TYPE_INFO_Command \":\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_COMMAND_BASE => "Spa:Pod:Object:Command:"u8;

    [NativeTypeName("#define PW_TYPE_INFO_BASE \"PipeWire:\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INFO_BASE => "PipeWire:"u8;

    [NativeTypeName("#define PW_TYPE_INFO_Object PW_TYPE_INFO_BASE \"Object\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INFO_Object => "PipeWire:Object"u8;

    [NativeTypeName("#define PW_TYPE_INFO_OBJECT_BASE PW_TYPE_INFO_Object \":\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INFO_OBJECT_BASE => "PipeWire:Object:"u8;

    [NativeTypeName("#define PW_TYPE_INFO_Interface PW_TYPE_INFO_BASE \"Interface\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INFO_Interface => "PipeWire:Interface"u8;

    [NativeTypeName("#define PW_TYPE_INFO_INTERFACE_BASE PW_TYPE_INFO_Interface \":\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INFO_INTERFACE_BASE => "PipeWire:Interface:"u8;

    [NativeTypeName("#define PW_TYPE_INTERFACE_Core PW_TYPE_INFO_INTERFACE_BASE \"Core\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INTERFACE_Core => "PipeWire:Interface:Core"u8;

    [NativeTypeName("#define PW_TYPE_INTERFACE_Registry PW_TYPE_INFO_INTERFACE_BASE \"Registry\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INTERFACE_Registry => "PipeWire:Interface:Registry"u8;

    [NativeTypeName("#define PW_CORE_PERM_MASK PW_PERM_R|PW_PERM_X|PW_PERM_M")]
    public const int PW_CORE_PERM_MASK = 0400 | 0100 | 0010;

    [NativeTypeName("#define PW_VERSION_CORE 4")]
    public const int PW_VERSION_CORE = 4;

    [NativeTypeName("#define PW_VERSION_REGISTRY 3")]
    public const int PW_VERSION_REGISTRY = 3;

    [NativeTypeName("#define PW_DEFAULT_REMOTE \"pipewire-0\"")]
    public static ReadOnlySpan<byte> PW_DEFAULT_REMOTE => "pipewire-0"u8;

    [NativeTypeName("#define PW_ID_CORE 0")]
    public const int PW_ID_CORE = 0;

    [NativeTypeName("#define PW_ID_ANY (uint32_t)(0xffffffff)")]
    public const uint PW_ID_ANY = (uint)(0xffffffff);

    [NativeTypeName("#define PW_CORE_CHANGE_MASK_PROPS (1 << 0)")]
    public const int PW_CORE_CHANGE_MASK_PROPS = (1 << 0);

    [NativeTypeName("#define PW_CORE_CHANGE_MASK_ALL ((1 << 1)-1)")]
    public const int PW_CORE_CHANGE_MASK_ALL = ((1 << 1) - 1);

    [NativeTypeName("#define PW_PROPERTIES_FLAG_NL (1<<0)")]
    public const int PW_PROPERTIES_FLAG_NL = (1 << 0);

    [NativeTypeName("#define PW_PROPERTIES_FLAG_RECURSE (1<<1)")]
    public const int PW_PROPERTIES_FLAG_RECURSE = (1 << 1);

    [NativeTypeName("#define PW_PROPERTIES_FLAG_ENCLOSE (1<<2)")]
    public const int PW_PROPERTIES_FLAG_ENCLOSE = (1 << 2);

    [NativeTypeName("#define PW_PROPERTIES_FLAG_ARRAY (1<<3)")]
    public const int PW_PROPERTIES_FLAG_ARRAY = (1 << 3);

    [NativeTypeName("#define PW_PROPERTIES_FLAG_COLORS (1<<4)")]
    public const int PW_PROPERTIES_FLAG_COLORS = (1 << 4);

    [NativeTypeName("#define PW_CORE_EVENT_INFO 0")]
    public const int PW_CORE_EVENT_INFO = 0;

    [NativeTypeName("#define PW_CORE_EVENT_DONE 1")]
    public const int PW_CORE_EVENT_DONE = 1;

    [NativeTypeName("#define PW_CORE_EVENT_PING 2")]
    public const int PW_CORE_EVENT_PING = 2;

    [NativeTypeName("#define PW_CORE_EVENT_ERROR 3")]
    public const int PW_CORE_EVENT_ERROR = 3;

    [NativeTypeName("#define PW_CORE_EVENT_REMOVE_ID 4")]
    public const int PW_CORE_EVENT_REMOVE_ID = 4;

    [NativeTypeName("#define PW_CORE_EVENT_BOUND_ID 5")]
    public const int PW_CORE_EVENT_BOUND_ID = 5;

    [NativeTypeName("#define PW_CORE_EVENT_ADD_MEM 6")]
    public const int PW_CORE_EVENT_ADD_MEM = 6;

    [NativeTypeName("#define PW_CORE_EVENT_REMOVE_MEM 7")]
    public const int PW_CORE_EVENT_REMOVE_MEM = 7;

    [NativeTypeName("#define PW_CORE_EVENT_BOUND_PROPS 8")]
    public const int PW_CORE_EVENT_BOUND_PROPS = 8;

    [NativeTypeName("#define PW_CORE_EVENT_NUM 9")]
    public const int PW_CORE_EVENT_NUM = 9;

    [NativeTypeName("#define PW_VERSION_CORE_EVENTS 1")]
    public const int PW_VERSION_CORE_EVENTS = 1;

    [NativeTypeName("#define PW_CORE_METHOD_ADD_LISTENER 0")]
    public const int PW_CORE_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define PW_CORE_METHOD_HELLO 1")]
    public const int PW_CORE_METHOD_HELLO = 1;

    [NativeTypeName("#define PW_CORE_METHOD_SYNC 2")]
    public const int PW_CORE_METHOD_SYNC = 2;

    [NativeTypeName("#define PW_CORE_METHOD_PONG 3")]
    public const int PW_CORE_METHOD_PONG = 3;

    [NativeTypeName("#define PW_CORE_METHOD_ERROR 4")]
    public const int PW_CORE_METHOD_ERROR = 4;

    [NativeTypeName("#define PW_CORE_METHOD_GET_REGISTRY 5")]
    public const int PW_CORE_METHOD_GET_REGISTRY = 5;

    [NativeTypeName("#define PW_CORE_METHOD_CREATE_OBJECT 6")]
    public const int PW_CORE_METHOD_CREATE_OBJECT = 6;

    [NativeTypeName("#define PW_CORE_METHOD_DESTROY 7")]
    public const int PW_CORE_METHOD_DESTROY = 7;

    [NativeTypeName("#define PW_CORE_METHOD_NUM 8")]
    public const int PW_CORE_METHOD_NUM = 8;

    [NativeTypeName("#define PW_VERSION_CORE_METHODS 0")]
    public const int PW_VERSION_CORE_METHODS = 0;

    [NativeTypeName("#define PW_REGISTRY_EVENT_GLOBAL 0")]
    public const int PW_REGISTRY_EVENT_GLOBAL = 0;

    [NativeTypeName("#define PW_REGISTRY_EVENT_GLOBAL_REMOVE 1")]
    public const int PW_REGISTRY_EVENT_GLOBAL_REMOVE = 1;

    [NativeTypeName("#define PW_REGISTRY_EVENT_NUM 2")]
    public const int PW_REGISTRY_EVENT_NUM = 2;

    [NativeTypeName("#define PW_VERSION_REGISTRY_EVENTS 0")]
    public const int PW_VERSION_REGISTRY_EVENTS = 0;

    [NativeTypeName("#define PW_REGISTRY_METHOD_ADD_LISTENER 0")]
    public const int PW_REGISTRY_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define PW_REGISTRY_METHOD_BIND 1")]
    public const int PW_REGISTRY_METHOD_BIND = 1;

    [NativeTypeName("#define PW_REGISTRY_METHOD_DESTROY 2")]
    public const int PW_REGISTRY_METHOD_DESTROY = 2;

    [NativeTypeName("#define PW_REGISTRY_METHOD_NUM 3")]
    public const int PW_REGISTRY_METHOD_NUM = 3;

    [NativeTypeName("#define PW_VERSION_REGISTRY_METHODS 0")]
    public const int PW_VERSION_REGISTRY_METHODS = 0;

    [NativeTypeName("#define SPA_TYPE_INTERFACE_System SPA_TYPE_INFO_INTERFACE_BASE \"System\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INTERFACE_System => "Spa:Pointer:Interface:System"u8;

    [NativeTypeName("#define SPA_TYPE_INTERFACE_DataSystem SPA_TYPE_INFO_INTERFACE_BASE \"DataSystem\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INTERFACE_DataSystem => "Spa:Pointer:Interface:DataSystem"u8;

    [NativeTypeName("#define SPA_VERSION_SYSTEM 0")]
    public const int SPA_VERSION_SYSTEM = 0;

    [NativeTypeName("#define SPA_IO_IN (1 << 0)")]
    public const int SPA_IO_IN = (1 << 0);

    [NativeTypeName("#define SPA_IO_OUT (1 << 2)")]
    public const int SPA_IO_OUT = (1 << 2);

    [NativeTypeName("#define SPA_IO_ERR (1 << 3)")]
    public const int SPA_IO_ERR = (1 << 3);

    [NativeTypeName("#define SPA_IO_HUP (1 << 4)")]
    public const int SPA_IO_HUP = (1 << 4);

    [NativeTypeName("#define SPA_FD_CLOEXEC (1<<0)")]
    public const int SPA_FD_CLOEXEC = (1 << 0);

    [NativeTypeName("#define SPA_FD_NONBLOCK (1<<1)")]
    public const int SPA_FD_NONBLOCK = (1 << 1);

    [NativeTypeName("#define SPA_FD_EVENT_SEMAPHORE (1<<2)")]
    public const int SPA_FD_EVENT_SEMAPHORE = (1 << 2);

    [NativeTypeName("#define SPA_FD_TIMER_ABSTIME (1<<3)")]
    public const int SPA_FD_TIMER_ABSTIME = (1 << 3);

    [NativeTypeName("#define SPA_FD_TIMER_CANCEL_ON_SET (1<<4)")]
    public const int SPA_FD_TIMER_CANCEL_ON_SET = (1 << 4);

    [NativeTypeName("#define SPA_VERSION_SYSTEM_METHODS 0")]
    public const int SPA_VERSION_SYSTEM_METHODS = 0;

    [NativeTypeName("#define SPA_TYPE_INTERFACE_Loop SPA_TYPE_INFO_INTERFACE_BASE \"Loop\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INTERFACE_Loop => "Spa:Pointer:Interface:Loop"u8;

    [NativeTypeName("#define SPA_TYPE_INTERFACE_DataLoop SPA_TYPE_INFO_INTERFACE_BASE \"DataLoop\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INTERFACE_DataLoop => "Spa:Pointer:Interface:DataLoop"u8;

    [NativeTypeName("#define SPA_VERSION_LOOP 0")]
    public const int SPA_VERSION_LOOP = 0;

    [NativeTypeName("#define SPA_TYPE_INTERFACE_LoopControl SPA_TYPE_INFO_INTERFACE_BASE \"LoopControl\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INTERFACE_LoopControl => "Spa:Pointer:Interface:LoopControl"u8;

    [NativeTypeName("#define SPA_VERSION_LOOP_CONTROL 2")]
    public const int SPA_VERSION_LOOP_CONTROL = 2;

    [NativeTypeName("#define SPA_TYPE_INTERFACE_LoopUtils SPA_TYPE_INFO_INTERFACE_BASE \"LoopUtils\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INTERFACE_LoopUtils => "Spa:Pointer:Interface:LoopUtils"u8;

    [NativeTypeName("#define SPA_VERSION_LOOP_UTILS 0")]
    public const int SPA_VERSION_LOOP_UTILS = 0;

    [NativeTypeName("#define SPA_VERSION_LOOP_METHODS 0")]
    public const int SPA_VERSION_LOOP_METHODS = 0;

    [NativeTypeName("#define SPA_VERSION_LOOP_CONTROL_HOOKS 0")]
    public const int SPA_VERSION_LOOP_CONTROL_HOOKS = 0;

    [NativeTypeName("#define SPA_VERSION_LOOP_CONTROL_METHODS 2")]
    public const int SPA_VERSION_LOOP_CONTROL_METHODS = 2;

    [NativeTypeName("#define SPA_VERSION_LOOP_UTILS_METHODS 0")]
    public const int SPA_VERSION_LOOP_UTILS_METHODS = 0;

    [NativeTypeName("#define PW_VERSION_CONTEXT_EVENTS 1")]
    public const int PW_VERSION_CONTEXT_EVENTS = 1;

    [NativeTypeName("#define SPA_POD_ALIGN 8")]
    public const int SPA_POD_ALIGN = 8;

    [NativeTypeName("#define SPA_POD_MAX_SIZE (1u<<20)")]
    public const uint SPA_POD_MAX_SIZE = (1U << 20);

    [NativeTypeName("#define SPA_POD_PROP_FLAG_READONLY (1u<<0)")]
    public const uint SPA_POD_PROP_FLAG_READONLY = (1U << 0);

    [NativeTypeName("#define SPA_POD_PROP_FLAG_HARDWARE (1u<<1)")]
    public const uint SPA_POD_PROP_FLAG_HARDWARE = (1U << 1);

    [NativeTypeName("#define SPA_POD_PROP_FLAG_HINT_DICT (1u<<2)")]
    public const uint SPA_POD_PROP_FLAG_HINT_DICT = (1U << 2);

    [NativeTypeName("#define SPA_POD_PROP_FLAG_MANDATORY (1u<<3)")]
    public const uint SPA_POD_PROP_FLAG_MANDATORY = (1U << 3);

    [NativeTypeName("#define SPA_POD_PROP_FLAG_DONT_FIXATE (1u<<4)")]
    public const uint SPA_POD_PROP_FLAG_DONT_FIXATE = (1U << 4);

    [NativeTypeName("#define SPA_POD_PROP_FLAG_DROP (1u<<5)")]
    public const uint SPA_POD_PROP_FLAG_DROP = (1U << 5);

    [NativeTypeName("#define PW_TYPE_INFO_Protocol \"PipeWire:Protocol\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INFO_Protocol => "PipeWire:Protocol"u8;

    [NativeTypeName("#define PW_TYPE_INFO_PROTOCOL_BASE PW_TYPE_INFO_Protocol \":\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INFO_PROTOCOL_BASE => "PipeWire:Protocol:"u8;

    [NativeTypeName("#define PW_PROTOCOL_MARSHAL_FLAG_IMPL (1 << 0)")]
    public const int PW_PROTOCOL_MARSHAL_FLAG_IMPL = (1 << 0);

    [NativeTypeName("#define PW_VERSION_PROTOCOL_IMPLEMENTATION 1")]
    public const int PW_VERSION_PROTOCOL_IMPLEMENTATION = 1;

    [NativeTypeName("#define PW_VERSION_PROTOCOL_EVENTS 0")]
    public const int PW_VERSION_PROTOCOL_EVENTS = 0;

    [NativeTypeName("#define PW_VERSION_PROXY_EVENTS 1")]
    public const int PW_VERSION_PROXY_EVENTS = 1;

    [NativeTypeName("#define PW_PERM_R 0400")]
    public const int PW_PERM_R = 0400;

    [NativeTypeName("#define PW_PERM_W 0200")]
    public const int PW_PERM_W = 0200;

    [NativeTypeName("#define PW_PERM_X 0100")]
    public const int PW_PERM_X = 0100;

    [NativeTypeName("#define PW_PERM_M 0010")]
    public const int PW_PERM_M = 0010;

    [NativeTypeName("#define PW_PERM_L 0020")]
    public const int PW_PERM_L = 0020;

    [NativeTypeName("#define PW_PERM_RW (PW_PERM_R|PW_PERM_W)")]
    public const int PW_PERM_RW = (0400 | 0200);

    [NativeTypeName("#define PW_PERM_RWX (PW_PERM_RW|PW_PERM_X)")]
    public const int PW_PERM_RWX = ((0400 | 0200) | 0100);

    [NativeTypeName("#define PW_PERM_RWXM (PW_PERM_RWX|PW_PERM_M)")]
    public const int PW_PERM_RWXM = (((0400 | 0200) | 0100) | 0010);

    [NativeTypeName("#define PW_PERM_RWXML (PW_PERM_RWXM|PW_PERM_L)")]
    public const int PW_PERM_RWXML = ((((0400 | 0200) | 0100) | 0010) | 0020);

    [NativeTypeName("#define PW_PERM_ALL PW_PERM_RWXM")]
    public const int PW_PERM_ALL = (((0400 | 0200) | 0100) | 0010);

    [NativeTypeName("#define PW_PERM_INVALID (uint32_t)(0xffffffff)")]
    public const uint PW_PERM_INVALID = (uint)(0xffffffff);

    [NativeTypeName("#define PW_PERMISSION_FORMAT \"%c%c%c%c%c\"")]
    public static ReadOnlySpan<byte> PW_PERMISSION_FORMAT => "%c%c%c%c%c"u8;

    [NativeTypeName("#define PW_TYPE_INTERFACE_Client PW_TYPE_INFO_INTERFACE_BASE \"Client\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INTERFACE_Client => "PipeWire:Interface:Client"u8;

    [NativeTypeName("#define PW_CLIENT_PERM_MASK PW_PERM_RWXM")]
    public const int PW_CLIENT_PERM_MASK = (((0400 | 0200) | 0100) | 0010);

    [NativeTypeName("#define PW_VERSION_CLIENT 3")]
    public const int PW_VERSION_CLIENT = 3;

    [NativeTypeName("#define PW_ID_CLIENT 1")]
    public const int PW_ID_CLIENT = 1;

    [NativeTypeName("#define PW_CLIENT_CHANGE_MASK_PROPS (1 << 0)")]
    public const int PW_CLIENT_CHANGE_MASK_PROPS = (1 << 0);

    [NativeTypeName("#define PW_CLIENT_CHANGE_MASK_ALL ((1 << 1)-1)")]
    public const int PW_CLIENT_CHANGE_MASK_ALL = ((1 << 1) - 1);

    [NativeTypeName("#define PW_CLIENT_EVENT_INFO 0")]
    public const int PW_CLIENT_EVENT_INFO = 0;

    [NativeTypeName("#define PW_CLIENT_EVENT_PERMISSIONS 1")]
    public const int PW_CLIENT_EVENT_PERMISSIONS = 1;

    [NativeTypeName("#define PW_CLIENT_EVENT_NUM 2")]
    public const int PW_CLIENT_EVENT_NUM = 2;

    [NativeTypeName("#define PW_VERSION_CLIENT_EVENTS 0")]
    public const int PW_VERSION_CLIENT_EVENTS = 0;

    [NativeTypeName("#define PW_CLIENT_METHOD_ADD_LISTENER 0")]
    public const int PW_CLIENT_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define PW_CLIENT_METHOD_ERROR 1")]
    public const int PW_CLIENT_METHOD_ERROR = 1;

    [NativeTypeName("#define PW_CLIENT_METHOD_UPDATE_PROPERTIES 2")]
    public const int PW_CLIENT_METHOD_UPDATE_PROPERTIES = 2;

    [NativeTypeName("#define PW_CLIENT_METHOD_GET_PERMISSIONS 3")]
    public const int PW_CLIENT_METHOD_GET_PERMISSIONS = 3;

    [NativeTypeName("#define PW_CLIENT_METHOD_UPDATE_PERMISSIONS 4")]
    public const int PW_CLIENT_METHOD_UPDATE_PERMISSIONS = 4;

    [NativeTypeName("#define PW_CLIENT_METHOD_NUM 5")]
    public const int PW_CLIENT_METHOD_NUM = 5;

    [NativeTypeName("#define PW_VERSION_CLIENT_METHODS 0")]
    public const int PW_VERSION_CLIENT_METHODS = 0;

    [NativeTypeName("#define SPA_JSON_ERROR_FLAG 0x100")]
    public const int SPA_JSON_ERROR_FLAG = 0x100;

    [NativeTypeName("#define PW_TYPE_INTERFACE_Device PW_TYPE_INFO_INTERFACE_BASE \"Device\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INTERFACE_Device => "PipeWire:Interface:Device"u8;

    [NativeTypeName("#define PW_DEVICE_PERM_MASK PW_PERM_RWXM")]
    public const int PW_DEVICE_PERM_MASK = (((0400 | 0200) | 0100) | 0010);

    [NativeTypeName("#define PW_VERSION_DEVICE 3")]
    public const int PW_VERSION_DEVICE = 3;

    [NativeTypeName("#define PW_DEVICE_CHANGE_MASK_PROPS (1 << 0)")]
    public const int PW_DEVICE_CHANGE_MASK_PROPS = (1 << 0);

    [NativeTypeName("#define PW_DEVICE_CHANGE_MASK_PARAMS (1 << 1)")]
    public const int PW_DEVICE_CHANGE_MASK_PARAMS = (1 << 1);

    [NativeTypeName("#define PW_DEVICE_CHANGE_MASK_ALL ((1 << 2)-1)")]
    public const int PW_DEVICE_CHANGE_MASK_ALL = ((1 << 2) - 1);

    [NativeTypeName("#define PW_DEVICE_EVENT_INFO 0")]
    public const int PW_DEVICE_EVENT_INFO = 0;

    [NativeTypeName("#define PW_DEVICE_EVENT_PARAM 1")]
    public const int PW_DEVICE_EVENT_PARAM = 1;

    [NativeTypeName("#define PW_DEVICE_EVENT_NUM 2")]
    public const int PW_DEVICE_EVENT_NUM = 2;

    [NativeTypeName("#define PW_VERSION_DEVICE_EVENTS 0")]
    public const int PW_VERSION_DEVICE_EVENTS = 0;

    [NativeTypeName("#define PW_DEVICE_METHOD_ADD_LISTENER 0")]
    public const int PW_DEVICE_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define PW_DEVICE_METHOD_SUBSCRIBE_PARAMS 1")]
    public const int PW_DEVICE_METHOD_SUBSCRIBE_PARAMS = 1;

    [NativeTypeName("#define PW_DEVICE_METHOD_ENUM_PARAMS 2")]
    public const int PW_DEVICE_METHOD_ENUM_PARAMS = 2;

    [NativeTypeName("#define PW_DEVICE_METHOD_SET_PARAM 3")]
    public const int PW_DEVICE_METHOD_SET_PARAM = 3;

    [NativeTypeName("#define PW_DEVICE_METHOD_NUM 4")]
    public const int PW_DEVICE_METHOD_NUM = 4;

    [NativeTypeName("#define PW_VERSION_DEVICE_METHODS 0")]
    public const int PW_VERSION_DEVICE_METHODS = 0;

    [NativeTypeName("#define SPA_META_HEADER_FLAG_DISCONT (1 << 0)")]
    public const int SPA_META_HEADER_FLAG_DISCONT = (1 << 0);

    [NativeTypeName("#define SPA_META_HEADER_FLAG_CORRUPTED (1 << 1)")]
    public const int SPA_META_HEADER_FLAG_CORRUPTED = (1 << 1);

    [NativeTypeName("#define SPA_META_HEADER_FLAG_MARKER (1 << 2)")]
    public const int SPA_META_HEADER_FLAG_MARKER = (1 << 2);

    [NativeTypeName("#define SPA_META_HEADER_FLAG_HEADER (1 << 3)")]
    public const int SPA_META_HEADER_FLAG_HEADER = (1 << 3);

    [NativeTypeName("#define SPA_META_HEADER_FLAG_GAP (1 << 4)")]
    public const int SPA_META_HEADER_FLAG_GAP = (1 << 4);

    [NativeTypeName("#define SPA_META_HEADER_FLAG_DELTA_UNIT (1 << 5)")]
    public const int SPA_META_HEADER_FLAG_DELTA_UNIT = (1 << 5);

    [NativeTypeName("#define SPA_META_FEATURE_SYNC_TIMELINE_RELEASE (1<<0)")]
    public const int SPA_META_FEATURE_SYNC_TIMELINE_RELEASE = (1 << 0);

    [NativeTypeName("#define SPA_META_SYNC_TIMELINE_UNSCHEDULED_RELEASE (1<<0)")]
    public const int SPA_META_SYNC_TIMELINE_UNSCHEDULED_RELEASE = (1 << 0);

    [NativeTypeName("#define SPA_CHUNK_FLAG_NONE 0")]
    public const int SPA_CHUNK_FLAG_NONE = 0;

    [NativeTypeName("#define SPA_CHUNK_FLAG_CORRUPTED (1u<<0)")]
    public const uint SPA_CHUNK_FLAG_CORRUPTED = (1U << 0);

    [NativeTypeName("#define SPA_CHUNK_FLAG_EMPTY (1u<<1)")]
    public const uint SPA_CHUNK_FLAG_EMPTY = (1U << 1);

    [NativeTypeName("#define SPA_DATA_FLAG_NONE 0")]
    public const int SPA_DATA_FLAG_NONE = 0;

    [NativeTypeName("#define SPA_DATA_FLAG_READABLE (1u<<0)")]
    public const uint SPA_DATA_FLAG_READABLE = (1U << 0);

    [NativeTypeName("#define SPA_DATA_FLAG_WRITABLE (1u<<1)")]
    public const uint SPA_DATA_FLAG_WRITABLE = (1U << 1);

    [NativeTypeName("#define SPA_DATA_FLAG_DYNAMIC (1u<<2)")]
    public const uint SPA_DATA_FLAG_DYNAMIC = (1U << 2);

    [NativeTypeName("#define SPA_DATA_FLAG_READWRITE (SPA_DATA_FLAG_READABLE|SPA_DATA_FLAG_WRITABLE)")]
    public const uint SPA_DATA_FLAG_READWRITE = ((1U << 0) | (1U << 1));

    [NativeTypeName("#define SPA_DATA_FLAG_MAPPABLE (1u<<3)")]
    public const uint SPA_DATA_FLAG_MAPPABLE = (1U << 3);

    [NativeTypeName("#define SPA_TYPE_INTERFACE_Node SPA_TYPE_INFO_INTERFACE_BASE \"Node\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INTERFACE_Node => "Spa:Pointer:Interface:Node"u8;

    [NativeTypeName("#define SPA_VERSION_NODE 0")]
    public const int SPA_VERSION_NODE = 0;

    [NativeTypeName("#define SPA_NODE_CHANGE_MASK_FLAGS (1u<<0)")]
    public const uint SPA_NODE_CHANGE_MASK_FLAGS = (1U << 0);

    [NativeTypeName("#define SPA_NODE_CHANGE_MASK_PROPS (1u<<1)")]
    public const uint SPA_NODE_CHANGE_MASK_PROPS = (1U << 1);

    [NativeTypeName("#define SPA_NODE_CHANGE_MASK_PARAMS (1u<<2)")]
    public const uint SPA_NODE_CHANGE_MASK_PARAMS = (1U << 2);

    [NativeTypeName("#define SPA_NODE_FLAG_RT (1u<<0)")]
    public const uint SPA_NODE_FLAG_RT = (1U << 0);

    [NativeTypeName("#define SPA_NODE_FLAG_IN_DYNAMIC_PORTS (1u<<1)")]
    public const uint SPA_NODE_FLAG_IN_DYNAMIC_PORTS = (1U << 1);

    [NativeTypeName("#define SPA_NODE_FLAG_OUT_DYNAMIC_PORTS (1u<<2)")]
    public const uint SPA_NODE_FLAG_OUT_DYNAMIC_PORTS = (1U << 2);

    [NativeTypeName("#define SPA_NODE_FLAG_IN_PORT_CONFIG (1u<<3)")]
    public const uint SPA_NODE_FLAG_IN_PORT_CONFIG = (1U << 3);

    [NativeTypeName("#define SPA_NODE_FLAG_OUT_PORT_CONFIG (1u<<4)")]
    public const uint SPA_NODE_FLAG_OUT_PORT_CONFIG = (1U << 4);

    [NativeTypeName("#define SPA_NODE_FLAG_NEED_CONFIGURE (1u<<5)")]
    public const uint SPA_NODE_FLAG_NEED_CONFIGURE = (1U << 5);

    [NativeTypeName("#define SPA_NODE_FLAG_ASYNC (1u<<6)")]
    public const uint SPA_NODE_FLAG_ASYNC = (1U << 6);

    [NativeTypeName("#define SPA_PORT_CHANGE_MASK_FLAGS (1u<<0)")]
    public const uint SPA_PORT_CHANGE_MASK_FLAGS = (1U << 0);

    [NativeTypeName("#define SPA_PORT_CHANGE_MASK_RATE (1u<<1)")]
    public const uint SPA_PORT_CHANGE_MASK_RATE = (1U << 1);

    [NativeTypeName("#define SPA_PORT_CHANGE_MASK_PROPS (1u<<2)")]
    public const uint SPA_PORT_CHANGE_MASK_PROPS = (1U << 2);

    [NativeTypeName("#define SPA_PORT_CHANGE_MASK_PARAMS (1u<<3)")]
    public const uint SPA_PORT_CHANGE_MASK_PARAMS = (1U << 3);

    [NativeTypeName("#define SPA_PORT_FLAG_REMOVABLE (1u<<0)")]
    public const uint SPA_PORT_FLAG_REMOVABLE = (1U << 0);

    [NativeTypeName("#define SPA_PORT_FLAG_OPTIONAL (1u<<1)")]
    public const uint SPA_PORT_FLAG_OPTIONAL = (1U << 1);

    [NativeTypeName("#define SPA_PORT_FLAG_CAN_ALLOC_BUFFERS (1u<<2)")]
    public const uint SPA_PORT_FLAG_CAN_ALLOC_BUFFERS = (1U << 2);

    [NativeTypeName("#define SPA_PORT_FLAG_IN_PLACE (1u<<3)")]
    public const uint SPA_PORT_FLAG_IN_PLACE = (1U << 3);

    [NativeTypeName("#define SPA_PORT_FLAG_NO_REF (1u<<4)")]
    public const uint SPA_PORT_FLAG_NO_REF = (1U << 4);

    [NativeTypeName("#define SPA_PORT_FLAG_LIVE (1u<<5)")]
    public const uint SPA_PORT_FLAG_LIVE = (1U << 5);

    [NativeTypeName("#define SPA_PORT_FLAG_PHYSICAL (1u<<6)")]
    public const uint SPA_PORT_FLAG_PHYSICAL = (1U << 6);

    [NativeTypeName("#define SPA_PORT_FLAG_TERMINAL (1u<<7)")]
    public const uint SPA_PORT_FLAG_TERMINAL = (1U << 7);

    [NativeTypeName("#define SPA_PORT_FLAG_DYNAMIC_DATA (1u<<8)")]
    public const uint SPA_PORT_FLAG_DYNAMIC_DATA = (1U << 8);

    [NativeTypeName("#define SPA_RESULT_TYPE_NODE_ERROR 1")]
    public const int SPA_RESULT_TYPE_NODE_ERROR = 1;

    [NativeTypeName("#define SPA_RESULT_TYPE_NODE_PARAMS 2")]
    public const int SPA_RESULT_TYPE_NODE_PARAMS = 2;

    [NativeTypeName("#define SPA_NODE_EVENT_INFO 0")]
    public const int SPA_NODE_EVENT_INFO = 0;

    [NativeTypeName("#define SPA_NODE_EVENT_PORT_INFO 1")]
    public const int SPA_NODE_EVENT_PORT_INFO = 1;

    [NativeTypeName("#define SPA_NODE_EVENT_RESULT 2")]
    public const int SPA_NODE_EVENT_RESULT = 2;

    [NativeTypeName("#define SPA_NODE_EVENT_EVENT 3")]
    public const int SPA_NODE_EVENT_EVENT = 3;

    [NativeTypeName("#define SPA_NODE_EVENT_NUM 4")]
    public const int SPA_NODE_EVENT_NUM = 4;

    [NativeTypeName("#define SPA_VERSION_NODE_EVENTS 0")]
    public const int SPA_VERSION_NODE_EVENTS = 0;

    [NativeTypeName("#define SPA_NODE_CALLBACK_READY 0")]
    public const int SPA_NODE_CALLBACK_READY = 0;

    [NativeTypeName("#define SPA_NODE_CALLBACK_REUSE_BUFFER 1")]
    public const int SPA_NODE_CALLBACK_REUSE_BUFFER = 1;

    [NativeTypeName("#define SPA_NODE_CALLBACK_XRUN 2")]
    public const int SPA_NODE_CALLBACK_XRUN = 2;

    [NativeTypeName("#define SPA_NODE_CALLBACK_NUM 3")]
    public const int SPA_NODE_CALLBACK_NUM = 3;

    [NativeTypeName("#define SPA_VERSION_NODE_CALLBACKS 0")]
    public const int SPA_VERSION_NODE_CALLBACKS = 0;

    [NativeTypeName("#define SPA_NODE_PARAM_FLAG_TEST_ONLY (1 << 0)")]
    public const int SPA_NODE_PARAM_FLAG_TEST_ONLY = (1 << 0);

    [NativeTypeName("#define SPA_NODE_PARAM_FLAG_FIXATE (1 << 1)")]
    public const int SPA_NODE_PARAM_FLAG_FIXATE = (1 << 1);

    [NativeTypeName("#define SPA_NODE_PARAM_FLAG_NEAREST (1 << 2)")]
    public const int SPA_NODE_PARAM_FLAG_NEAREST = (1 << 2);

    [NativeTypeName("#define SPA_NODE_BUFFERS_FLAG_ALLOC (1 << 0)")]
    public const int SPA_NODE_BUFFERS_FLAG_ALLOC = (1 << 0);

    [NativeTypeName("#define SPA_NODE_METHOD_ADD_LISTENER 0")]
    public const int SPA_NODE_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define SPA_NODE_METHOD_SET_CALLBACKS 1")]
    public const int SPA_NODE_METHOD_SET_CALLBACKS = 1;

    [NativeTypeName("#define SPA_NODE_METHOD_SYNC 2")]
    public const int SPA_NODE_METHOD_SYNC = 2;

    [NativeTypeName("#define SPA_NODE_METHOD_ENUM_PARAMS 3")]
    public const int SPA_NODE_METHOD_ENUM_PARAMS = 3;

    [NativeTypeName("#define SPA_NODE_METHOD_SET_PARAM 4")]
    public const int SPA_NODE_METHOD_SET_PARAM = 4;

    [NativeTypeName("#define SPA_NODE_METHOD_SET_IO 5")]
    public const int SPA_NODE_METHOD_SET_IO = 5;

    [NativeTypeName("#define SPA_NODE_METHOD_SEND_COMMAND 6")]
    public const int SPA_NODE_METHOD_SEND_COMMAND = 6;

    [NativeTypeName("#define SPA_NODE_METHOD_ADD_PORT 7")]
    public const int SPA_NODE_METHOD_ADD_PORT = 7;

    [NativeTypeName("#define SPA_NODE_METHOD_REMOVE_PORT 8")]
    public const int SPA_NODE_METHOD_REMOVE_PORT = 8;

    [NativeTypeName("#define SPA_NODE_METHOD_PORT_ENUM_PARAMS 9")]
    public const int SPA_NODE_METHOD_PORT_ENUM_PARAMS = 9;

    [NativeTypeName("#define SPA_NODE_METHOD_PORT_SET_PARAM 10")]
    public const int SPA_NODE_METHOD_PORT_SET_PARAM = 10;

    [NativeTypeName("#define SPA_NODE_METHOD_PORT_USE_BUFFERS 11")]
    public const int SPA_NODE_METHOD_PORT_USE_BUFFERS = 11;

    [NativeTypeName("#define SPA_NODE_METHOD_PORT_SET_IO 12")]
    public const int SPA_NODE_METHOD_PORT_SET_IO = 12;

    [NativeTypeName("#define SPA_NODE_METHOD_PORT_REUSE_BUFFER 13")]
    public const int SPA_NODE_METHOD_PORT_REUSE_BUFFER = 13;

    [NativeTypeName("#define SPA_NODE_METHOD_PROCESS 14")]
    public const int SPA_NODE_METHOD_PROCESS = 14;

    [NativeTypeName("#define SPA_NODE_METHOD_NUM 15")]
    public const int SPA_NODE_METHOD_NUM = 15;

    [NativeTypeName("#define SPA_VERSION_NODE_METHODS 0")]
    public const int SPA_VERSION_NODE_METHODS = 0;

    [NativeTypeName("#define PW_VERSION_MEMPOOL_EVENTS 0")]
    public const int PW_VERSION_MEMPOOL_EVENTS = 0;

    [NativeTypeName("#define PW_BUFFERS_FLAG_NONE 0")]
    public const int PW_BUFFERS_FLAG_NONE = 0;

    [NativeTypeName("#define PW_BUFFERS_FLAG_NO_MEM (1<<0)")]
    public const int PW_BUFFERS_FLAG_NO_MEM = (1 << 0);

    [NativeTypeName("#define PW_BUFFERS_FLAG_SHARED (1<<1)")]
    public const int PW_BUFFERS_FLAG_SHARED = (1 << 1);

    [NativeTypeName("#define PW_BUFFERS_FLAG_DYNAMIC (1<<2)")]
    public const int PW_BUFFERS_FLAG_DYNAMIC = (1 << 2);

    [NativeTypeName("#define PW_BUFFERS_FLAG_IN_PRIORITY (1<<4)")]
    public const int PW_BUFFERS_FLAG_IN_PRIORITY = (1 << 4);

    [NativeTypeName("#define PW_BUFFERS_FLAG_ASYNC (1<<5)")]
    public const int PW_BUFFERS_FLAG_ASYNC = (1 << 5);

    [NativeTypeName("#define PW_TYPE_INTERFACE_Factory PW_TYPE_INFO_INTERFACE_BASE \"Factory\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INTERFACE_Factory => "PipeWire:Interface:Factory"u8;

    [NativeTypeName("#define PW_FACTORY_PERM_MASK PW_PERM_R|PW_PERM_M")]
    public const int PW_FACTORY_PERM_MASK = 0400 | 0010;

    [NativeTypeName("#define PW_VERSION_FACTORY 3")]
    public const int PW_VERSION_FACTORY = 3;

    [NativeTypeName("#define PW_FACTORY_CHANGE_MASK_PROPS (1 << 0)")]
    public const int PW_FACTORY_CHANGE_MASK_PROPS = (1 << 0);

    [NativeTypeName("#define PW_FACTORY_CHANGE_MASK_ALL ((1 << 1)-1)")]
    public const int PW_FACTORY_CHANGE_MASK_ALL = ((1 << 1) - 1);

    [NativeTypeName("#define PW_FACTORY_EVENT_INFO 0")]
    public const int PW_FACTORY_EVENT_INFO = 0;

    [NativeTypeName("#define PW_FACTORY_EVENT_NUM 1")]
    public const int PW_FACTORY_EVENT_NUM = 1;

    [NativeTypeName("#define PW_VERSION_FACTORY_EVENTS 0")]
    public const int PW_VERSION_FACTORY_EVENTS = 0;

    [NativeTypeName("#define PW_FACTORY_METHOD_ADD_LISTENER 0")]
    public const int PW_FACTORY_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define PW_FACTORY_METHOD_NUM 1")]
    public const int PW_FACTORY_METHOD_NUM = 1;

    [NativeTypeName("#define PW_VERSION_FACTORY_METHODS 0")]
    public const int PW_VERSION_FACTORY_METHODS = 0;

    [NativeTypeName("#define PW_KEY_PROTOCOL \"pipewire.protocol\"")]
    public static ReadOnlySpan<byte> PW_KEY_PROTOCOL => "pipewire.protocol"u8;

    [NativeTypeName("#define PW_KEY_ACCESS \"pipewire.access\"")]
    public static ReadOnlySpan<byte> PW_KEY_ACCESS => "pipewire.access"u8;

    [NativeTypeName("#define PW_KEY_CLIENT_ACCESS \"pipewire.client.access\"")]
    public static ReadOnlySpan<byte> PW_KEY_CLIENT_ACCESS => "pipewire.client.access"u8;

    [NativeTypeName("#define PW_KEY_SEC_PID \"pipewire.sec.pid\"")]
    public static ReadOnlySpan<byte> PW_KEY_SEC_PID => "pipewire.sec.pid"u8;

    [NativeTypeName("#define PW_KEY_SEC_UID \"pipewire.sec.uid\"")]
    public static ReadOnlySpan<byte> PW_KEY_SEC_UID => "pipewire.sec.uid"u8;

    [NativeTypeName("#define PW_KEY_SEC_GID \"pipewire.sec.gid\"")]
    public static ReadOnlySpan<byte> PW_KEY_SEC_GID => "pipewire.sec.gid"u8;

    [NativeTypeName("#define PW_KEY_SEC_LABEL \"pipewire.sec.label\"")]
    public static ReadOnlySpan<byte> PW_KEY_SEC_LABEL => "pipewire.sec.label"u8;

    [NativeTypeName("#define PW_KEY_SEC_SOCKET \"pipewire.sec.socket\"")]
    public static ReadOnlySpan<byte> PW_KEY_SEC_SOCKET => "pipewire.sec.socket"u8;

    [NativeTypeName("#define PW_KEY_SEC_ENGINE \"pipewire.sec.engine\"")]
    public static ReadOnlySpan<byte> PW_KEY_SEC_ENGINE => "pipewire.sec.engine"u8;

    [NativeTypeName("#define PW_KEY_SEC_APP_ID \"pipewire.sec.app-id\"")]
    public static ReadOnlySpan<byte> PW_KEY_SEC_APP_ID => "pipewire.sec.app-id"u8;

    [NativeTypeName("#define PW_KEY_SEC_INSTANCE_ID \"pipewire.sec.instance-id\"")]
    public static ReadOnlySpan<byte> PW_KEY_SEC_INSTANCE_ID => "pipewire.sec.instance-id"u8;

    [NativeTypeName("#define PW_KEY_LIBRARY_NAME_SYSTEM \"library.name.system\"")]
    public static ReadOnlySpan<byte> PW_KEY_LIBRARY_NAME_SYSTEM => "library.name.system"u8;

    [NativeTypeName("#define PW_KEY_LIBRARY_NAME_LOOP \"library.name.loop\"")]
    public static ReadOnlySpan<byte> PW_KEY_LIBRARY_NAME_LOOP => "library.name.loop"u8;

    [NativeTypeName("#define PW_KEY_LIBRARY_NAME_DBUS \"library.name.dbus\"")]
    public static ReadOnlySpan<byte> PW_KEY_LIBRARY_NAME_DBUS => "library.name.dbus"u8;

    [NativeTypeName("#define PW_KEY_OBJECT_PATH \"object.path\"")]
    public static ReadOnlySpan<byte> PW_KEY_OBJECT_PATH => "object.path"u8;

    [NativeTypeName("#define PW_KEY_OBJECT_ID \"object.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_OBJECT_ID => "object.id"u8;

    [NativeTypeName("#define PW_KEY_OBJECT_SERIAL \"object.serial\"")]
    public static ReadOnlySpan<byte> PW_KEY_OBJECT_SERIAL => "object.serial"u8;

    [NativeTypeName("#define PW_KEY_OBJECT_LINGER \"object.linger\"")]
    public static ReadOnlySpan<byte> PW_KEY_OBJECT_LINGER => "object.linger"u8;

    [NativeTypeName("#define PW_KEY_OBJECT_REGISTER \"object.register\"")]
    public static ReadOnlySpan<byte> PW_KEY_OBJECT_REGISTER => "object.register"u8;

    [NativeTypeName("#define PW_KEY_OBJECT_EXPORT \"object.export\"")]
    public static ReadOnlySpan<byte> PW_KEY_OBJECT_EXPORT => "object.export"u8;

    [NativeTypeName("#define PW_KEY_CONFIG_PREFIX \"config.prefix\"")]
    public static ReadOnlySpan<byte> PW_KEY_CONFIG_PREFIX => "config.prefix"u8;

    [NativeTypeName("#define PW_KEY_CONFIG_NAME \"config.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_CONFIG_NAME => "config.name"u8;

    [NativeTypeName("#define PW_KEY_CONFIG_OVERRIDE_PREFIX \"config.override.prefix\"")]
    public static ReadOnlySpan<byte> PW_KEY_CONFIG_OVERRIDE_PREFIX => "config.override.prefix"u8;

    [NativeTypeName("#define PW_KEY_CONFIG_OVERRIDE_NAME \"config.override.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_CONFIG_OVERRIDE_NAME => "config.override.name"u8;

    [NativeTypeName("#define PW_KEY_LOOP_NAME \"loop.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_LOOP_NAME => "loop.name"u8;

    [NativeTypeName("#define PW_KEY_LOOP_CLASS \"loop.class\"")]
    public static ReadOnlySpan<byte> PW_KEY_LOOP_CLASS => "loop.class"u8;

    [NativeTypeName("#define PW_KEY_LOOP_RT_PRIO \"loop.rt-prio\"")]
    public static ReadOnlySpan<byte> PW_KEY_LOOP_RT_PRIO => "loop.rt-prio"u8;

    [NativeTypeName("#define PW_KEY_LOOP_CANCEL \"loop.cancel\"")]
    public static ReadOnlySpan<byte> PW_KEY_LOOP_CANCEL => "loop.cancel"u8;

    [NativeTypeName("#define PW_KEY_CONTEXT_PROFILE_MODULES \"context.profile.modules\"")]
    public static ReadOnlySpan<byte> PW_KEY_CONTEXT_PROFILE_MODULES => "context.profile.modules"u8;

    [NativeTypeName("#define PW_KEY_USER_NAME \"context.user-name\"")]
    public static ReadOnlySpan<byte> PW_KEY_USER_NAME => "context.user-name"u8;

    [NativeTypeName("#define PW_KEY_HOST_NAME \"context.host-name\"")]
    public static ReadOnlySpan<byte> PW_KEY_HOST_NAME => "context.host-name"u8;

    [NativeTypeName("#define PW_KEY_CORE_NAME \"core.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_CORE_NAME => "core.name"u8;

    [NativeTypeName("#define PW_KEY_CORE_VERSION \"core.version\"")]
    public static ReadOnlySpan<byte> PW_KEY_CORE_VERSION => "core.version"u8;

    [NativeTypeName("#define PW_KEY_CORE_DAEMON \"core.daemon\"")]
    public static ReadOnlySpan<byte> PW_KEY_CORE_DAEMON => "core.daemon"u8;

    [NativeTypeName("#define PW_KEY_CORE_ID \"core.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_CORE_ID => "core.id"u8;

    [NativeTypeName("#define PW_KEY_CORE_MONITORS \"core.monitors\"")]
    public static ReadOnlySpan<byte> PW_KEY_CORE_MONITORS => "core.monitors"u8;

    [NativeTypeName("#define PW_KEY_CPU_MAX_ALIGN \"cpu.max-align\"")]
    public static ReadOnlySpan<byte> PW_KEY_CPU_MAX_ALIGN => "cpu.max-align"u8;

    [NativeTypeName("#define PW_KEY_CPU_CORES \"cpu.cores\"")]
    public static ReadOnlySpan<byte> PW_KEY_CPU_CORES => "cpu.cores"u8;

    [NativeTypeName("#define PW_KEY_PRIORITY_SESSION \"priority.session\"")]
    public static ReadOnlySpan<byte> PW_KEY_PRIORITY_SESSION => "priority.session"u8;

    [NativeTypeName("#define PW_KEY_PRIORITY_DRIVER \"priority.driver\"")]
    public static ReadOnlySpan<byte> PW_KEY_PRIORITY_DRIVER => "priority.driver"u8;

    [NativeTypeName("#define PW_KEY_REMOTE_NAME \"remote.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_REMOTE_NAME => "remote.name"u8;

    [NativeTypeName("#define PW_KEY_REMOTE_INTENTION \"remote.intention\"")]
    public static ReadOnlySpan<byte> PW_KEY_REMOTE_INTENTION => "remote.intention"u8;

    [NativeTypeName("#define PW_KEY_APP_NAME \"application.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_NAME => "application.name"u8;

    [NativeTypeName("#define PW_KEY_APP_ID \"application.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_ID => "application.id"u8;

    [NativeTypeName("#define PW_KEY_APP_VERSION \"application.version\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_VERSION => "application.version"u8;

    [NativeTypeName("#define PW_KEY_APP_ICON \"application.icon\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_ICON => "application.icon"u8;

    [NativeTypeName("#define PW_KEY_APP_ICON_NAME \"application.icon-name\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_ICON_NAME => "application.icon-name"u8;

    [NativeTypeName("#define PW_KEY_APP_LANGUAGE \"application.language\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_LANGUAGE => "application.language"u8;

    [NativeTypeName("#define PW_KEY_APP_PROCESS_ID \"application.process.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_PROCESS_ID => "application.process.id"u8;

    [NativeTypeName("#define PW_KEY_APP_PROCESS_BINARY \"application.process.binary\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_PROCESS_BINARY => "application.process.binary"u8;

    [NativeTypeName("#define PW_KEY_APP_PROCESS_USER \"application.process.user\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_PROCESS_USER => "application.process.user"u8;

    [NativeTypeName("#define PW_KEY_APP_PROCESS_HOST \"application.process.host\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_PROCESS_HOST => "application.process.host"u8;

    [NativeTypeName("#define PW_KEY_APP_PROCESS_MACHINE_ID \"application.process.machine-id\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_PROCESS_MACHINE_ID => "application.process.machine-id"u8;

    [NativeTypeName("#define PW_KEY_APP_PROCESS_SESSION_ID \"application.process.session-id\"")]
    public static ReadOnlySpan<byte> PW_KEY_APP_PROCESS_SESSION_ID => "application.process.session-id"u8;

    [NativeTypeName("#define PW_KEY_WINDOW_X11_DISPLAY \"window.x11.display\"")]
    public static ReadOnlySpan<byte> PW_KEY_WINDOW_X11_DISPLAY => "window.x11.display"u8;

    [NativeTypeName("#define PW_KEY_CLIENT_ID \"client.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_CLIENT_ID => "client.id"u8;

    [NativeTypeName("#define PW_KEY_CLIENT_NAME \"client.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_CLIENT_NAME => "client.name"u8;

    [NativeTypeName("#define PW_KEY_CLIENT_API \"client.api\"")]
    public static ReadOnlySpan<byte> PW_KEY_CLIENT_API => "client.api"u8;

    [NativeTypeName("#define PW_KEY_NODE_ID \"node.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_ID => "node.id"u8;

    [NativeTypeName("#define PW_KEY_NODE_NAME \"node.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_NAME => "node.name"u8;

    [NativeTypeName("#define PW_KEY_NODE_NICK \"node.nick\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_NICK => "node.nick"u8;

    [NativeTypeName("#define PW_KEY_NODE_DESCRIPTION \"node.description\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_DESCRIPTION => "node.description"u8;

    [NativeTypeName("#define PW_KEY_NODE_PLUGGED \"node.plugged\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_PLUGGED => "node.plugged"u8;

    [NativeTypeName("#define PW_KEY_NODE_SESSION \"node.session\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_SESSION => "node.session"u8;

    [NativeTypeName("#define PW_KEY_NODE_GROUP \"node.group\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_GROUP => "node.group"u8;

    [NativeTypeName("#define PW_KEY_NODE_SYNC_GROUP \"node.sync-group\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_SYNC_GROUP => "node.sync-group"u8;

    [NativeTypeName("#define PW_KEY_NODE_SYNC \"node.sync\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_SYNC => "node.sync"u8;

    [NativeTypeName("#define PW_KEY_NODE_TRANSPORT \"node.transport\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_TRANSPORT => "node.transport"u8;

    [NativeTypeName("#define PW_KEY_NODE_EXCLUSIVE \"node.exclusive\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_EXCLUSIVE => "node.exclusive"u8;

    [NativeTypeName("#define PW_KEY_NODE_AUTOCONNECT \"node.autoconnect\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_AUTOCONNECT => "node.autoconnect"u8;

    [NativeTypeName("#define PW_KEY_NODE_LATENCY \"node.latency\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_LATENCY => "node.latency"u8;

    [NativeTypeName("#define PW_KEY_NODE_MAX_LATENCY \"node.max-latency\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_MAX_LATENCY => "node.max-latency"u8;

    [NativeTypeName("#define PW_KEY_NODE_LOCK_QUANTUM \"node.lock-quantum\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_LOCK_QUANTUM => "node.lock-quantum"u8;

    [NativeTypeName("#define PW_KEY_NODE_FORCE_QUANTUM \"node.force-quantum\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_FORCE_QUANTUM => "node.force-quantum"u8;

    [NativeTypeName("#define PW_KEY_NODE_RATE \"node.rate\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_RATE => "node.rate"u8;

    [NativeTypeName("#define PW_KEY_NODE_LOCK_RATE \"node.lock-rate\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_LOCK_RATE => "node.lock-rate"u8;

    [NativeTypeName("#define PW_KEY_NODE_FORCE_RATE \"node.force-rate\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_FORCE_RATE => "node.force-rate"u8;

    [NativeTypeName("#define PW_KEY_NODE_DONT_RECONNECT \"node.dont-reconnect\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_DONT_RECONNECT => "node.dont-reconnect"u8;

    [NativeTypeName("#define PW_KEY_NODE_ALWAYS_PROCESS \"node.always-process\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_ALWAYS_PROCESS => "node.always-process"u8;

    [NativeTypeName("#define PW_KEY_NODE_WANT_DRIVER \"node.want-driver\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_WANT_DRIVER => "node.want-driver"u8;

    [NativeTypeName("#define PW_KEY_NODE_PAUSE_ON_IDLE \"node.pause-on-idle\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_PAUSE_ON_IDLE => "node.pause-on-idle"u8;

    [NativeTypeName("#define PW_KEY_NODE_SUSPEND_ON_IDLE \"node.suspend-on-idle\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_SUSPEND_ON_IDLE => "node.suspend-on-idle"u8;

    [NativeTypeName("#define PW_KEY_NODE_CACHE_PARAMS \"node.cache-params\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_CACHE_PARAMS => "node.cache-params"u8;

    [NativeTypeName("#define PW_KEY_NODE_TRANSPORT_SYNC \"node.transport.sync\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_TRANSPORT_SYNC => "node.transport.sync"u8;

    [NativeTypeName("#define PW_KEY_NODE_DRIVER \"node.driver\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_DRIVER => "node.driver"u8;

    [NativeTypeName("#define PW_KEY_NODE_SUPPORTS_LAZY \"node.supports-lazy\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_SUPPORTS_LAZY => "node.supports-lazy"u8;

    [NativeTypeName("#define PW_KEY_NODE_SUPPORTS_REQUEST \"node.supports-request\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_SUPPORTS_REQUEST => "node.supports-request"u8;

    [NativeTypeName("#define PW_KEY_NODE_DRIVER_ID \"node.driver-id\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_DRIVER_ID => "node.driver-id"u8;

    [NativeTypeName("#define PW_KEY_NODE_ASYNC \"node.async\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_ASYNC => "node.async"u8;

    [NativeTypeName("#define PW_KEY_NODE_LOOP_NAME \"node.loop.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_LOOP_NAME => "node.loop.name"u8;

    [NativeTypeName("#define PW_KEY_NODE_LOOP_CLASS \"node.loop.class\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_LOOP_CLASS => "node.loop.class"u8;

    [NativeTypeName("#define PW_KEY_NODE_STREAM \"node.stream\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_STREAM => "node.stream"u8;

    [NativeTypeName("#define PW_KEY_NODE_VIRTUAL \"node.virtual\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_VIRTUAL => "node.virtual"u8;

    [NativeTypeName("#define PW_KEY_NODE_PASSIVE \"node.passive\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_PASSIVE => "node.passive"u8;

    [NativeTypeName("#define PW_KEY_NODE_LINK_GROUP \"node.link-group\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_LINK_GROUP => "node.link-group"u8;

    [NativeTypeName("#define PW_KEY_NODE_NETWORK \"node.network\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_NETWORK => "node.network"u8;

    [NativeTypeName("#define PW_KEY_NODE_TRIGGER \"node.trigger\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_TRIGGER => "node.trigger"u8;

    [NativeTypeName("#define PW_KEY_NODE_CHANNELNAMES \"node.channel-names\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_CHANNELNAMES => "node.channel-names"u8;

    [NativeTypeName("#define PW_KEY_NODE_DEVICE_PORT_NAME_PREFIX \"node.device-port-name-prefix\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_DEVICE_PORT_NAME_PREFIX => "node.device-port-name-prefix"u8;

    [NativeTypeName("#define PW_KEY_NODE_PHYSICAL \"node.physical\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_PHYSICAL => "node.physical"u8;

    [NativeTypeName("#define PW_KEY_NODE_TERMINAL \"node.terminal\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_TERMINAL => "node.terminal"u8;

    [NativeTypeName("#define PW_KEY_NODE_RELIABLE \"node.reliable\"")]
    public static ReadOnlySpan<byte> PW_KEY_NODE_RELIABLE => "node.reliable"u8;

    [NativeTypeName("#define PW_KEY_PORT_ID \"port.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_ID => "port.id"u8;

    [NativeTypeName("#define PW_KEY_PORT_NAME \"port.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_NAME => "port.name"u8;

    [NativeTypeName("#define PW_KEY_PORT_DIRECTION \"port.direction\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_DIRECTION => "port.direction"u8;

    [NativeTypeName("#define PW_KEY_PORT_ALIAS \"port.alias\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_ALIAS => "port.alias"u8;

    [NativeTypeName("#define PW_KEY_PORT_PHYSICAL \"port.physical\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_PHYSICAL => "port.physical"u8;

    [NativeTypeName("#define PW_KEY_PORT_TERMINAL \"port.terminal\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_TERMINAL => "port.terminal"u8;

    [NativeTypeName("#define PW_KEY_PORT_CONTROL \"port.control\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_CONTROL => "port.control"u8;

    [NativeTypeName("#define PW_KEY_PORT_MONITOR \"port.monitor\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_MONITOR => "port.monitor"u8;

    [NativeTypeName("#define PW_KEY_PORT_CACHE_PARAMS \"port.cache-params\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_CACHE_PARAMS => "port.cache-params"u8;

    [NativeTypeName("#define PW_KEY_PORT_EXTRA \"port.extra\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_EXTRA => "port.extra"u8;

    [NativeTypeName("#define PW_KEY_PORT_PASSIVE \"port.passive\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_PASSIVE => "port.passive"u8;

    [NativeTypeName("#define PW_KEY_PORT_IGNORE_LATENCY \"port.ignore-latency\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_IGNORE_LATENCY => "port.ignore-latency"u8;

    [NativeTypeName("#define PW_KEY_PORT_GROUP \"port.group\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_GROUP => "port.group"u8;

    [NativeTypeName("#define PW_KEY_PORT_EXCLUSIVE \"port.exclusive\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_EXCLUSIVE => "port.exclusive"u8;

    [NativeTypeName("#define PW_KEY_PORT_RELIABLE \"port.reliable\"")]
    public static ReadOnlySpan<byte> PW_KEY_PORT_RELIABLE => "port.reliable"u8;

    [NativeTypeName("#define PW_KEY_LINK_ID \"link.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_LINK_ID => "link.id"u8;

    [NativeTypeName("#define PW_KEY_LINK_INPUT_NODE \"link.input.node\"")]
    public static ReadOnlySpan<byte> PW_KEY_LINK_INPUT_NODE => "link.input.node"u8;

    [NativeTypeName("#define PW_KEY_LINK_INPUT_PORT \"link.input.port\"")]
    public static ReadOnlySpan<byte> PW_KEY_LINK_INPUT_PORT => "link.input.port"u8;

    [NativeTypeName("#define PW_KEY_LINK_OUTPUT_NODE \"link.output.node\"")]
    public static ReadOnlySpan<byte> PW_KEY_LINK_OUTPUT_NODE => "link.output.node"u8;

    [NativeTypeName("#define PW_KEY_LINK_OUTPUT_PORT \"link.output.port\"")]
    public static ReadOnlySpan<byte> PW_KEY_LINK_OUTPUT_PORT => "link.output.port"u8;

    [NativeTypeName("#define PW_KEY_LINK_PASSIVE \"link.passive\"")]
    public static ReadOnlySpan<byte> PW_KEY_LINK_PASSIVE => "link.passive"u8;

    [NativeTypeName("#define PW_KEY_LINK_FEEDBACK \"link.feedback\"")]
    public static ReadOnlySpan<byte> PW_KEY_LINK_FEEDBACK => "link.feedback"u8;

    [NativeTypeName("#define PW_KEY_LINK_ASYNC \"link.async\"")]
    public static ReadOnlySpan<byte> PW_KEY_LINK_ASYNC => "link.async"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_ID \"device.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_ID => "device.id"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_NAME \"device.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_NAME => "device.name"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_PLUGGED \"device.plugged\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_PLUGGED => "device.plugged"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_NICK \"device.nick\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_NICK => "device.nick"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_STRING \"device.string\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_STRING => "device.string"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_API \"device.api\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_API => "device.api"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_DESCRIPTION \"device.description\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_DESCRIPTION => "device.description"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_BUS_PATH \"device.bus-path\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_BUS_PATH => "device.bus-path"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_SERIAL \"device.serial\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_SERIAL => "device.serial"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_VENDOR_ID \"device.vendor.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_VENDOR_ID => "device.vendor.id"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_VENDOR_NAME \"device.vendor.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_VENDOR_NAME => "device.vendor.name"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_PRODUCT_ID \"device.product.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_PRODUCT_ID => "device.product.id"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_PRODUCT_NAME \"device.product.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_PRODUCT_NAME => "device.product.name"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_CLASS \"device.class\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_CLASS => "device.class"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_FORM_FACTOR \"device.form-factor\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_FORM_FACTOR => "device.form-factor"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_BUS \"device.bus\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_BUS => "device.bus"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_SUBSYSTEM \"device.subsystem\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_SUBSYSTEM => "device.subsystem"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_SYSFS_PATH \"device.sysfs.path\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_SYSFS_PATH => "device.sysfs.path"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_ICON \"device.icon\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_ICON => "device.icon"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_ICON_NAME \"device.icon-name\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_ICON_NAME => "device.icon-name"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_INTENDED_ROLES \"device.intended-roles\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_INTENDED_ROLES => "device.intended-roles"u8;

    [NativeTypeName("#define PW_KEY_DEVICE_CACHE_PARAMS \"device.cache-params\"")]
    public static ReadOnlySpan<byte> PW_KEY_DEVICE_CACHE_PARAMS => "device.cache-params"u8;

    [NativeTypeName("#define PW_KEY_MODULE_ID \"module.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_MODULE_ID => "module.id"u8;

    [NativeTypeName("#define PW_KEY_MODULE_NAME \"module.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_MODULE_NAME => "module.name"u8;

    [NativeTypeName("#define PW_KEY_MODULE_AUTHOR \"module.author\"")]
    public static ReadOnlySpan<byte> PW_KEY_MODULE_AUTHOR => "module.author"u8;

    [NativeTypeName("#define PW_KEY_MODULE_DESCRIPTION \"module.description\"")]
    public static ReadOnlySpan<byte> PW_KEY_MODULE_DESCRIPTION => "module.description"u8;

    [NativeTypeName("#define PW_KEY_MODULE_USAGE \"module.usage\"")]
    public static ReadOnlySpan<byte> PW_KEY_MODULE_USAGE => "module.usage"u8;

    [NativeTypeName("#define PW_KEY_MODULE_VERSION \"module.version\"")]
    public static ReadOnlySpan<byte> PW_KEY_MODULE_VERSION => "module.version"u8;

    [NativeTypeName("#define PW_KEY_MODULE_DEPRECATED \"module.deprecated\"")]
    public static ReadOnlySpan<byte> PW_KEY_MODULE_DEPRECATED => "module.deprecated"u8;

    [NativeTypeName("#define PW_KEY_FACTORY_ID \"factory.id\"")]
    public static ReadOnlySpan<byte> PW_KEY_FACTORY_ID => "factory.id"u8;

    [NativeTypeName("#define PW_KEY_FACTORY_NAME \"factory.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_FACTORY_NAME => "factory.name"u8;

    [NativeTypeName("#define PW_KEY_FACTORY_USAGE \"factory.usage\"")]
    public static ReadOnlySpan<byte> PW_KEY_FACTORY_USAGE => "factory.usage"u8;

    [NativeTypeName("#define PW_KEY_FACTORY_TYPE_NAME \"factory.type.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_FACTORY_TYPE_NAME => "factory.type.name"u8;

    [NativeTypeName("#define PW_KEY_FACTORY_TYPE_VERSION \"factory.type.version\"")]
    public static ReadOnlySpan<byte> PW_KEY_FACTORY_TYPE_VERSION => "factory.type.version"u8;

    [NativeTypeName("#define PW_KEY_STREAM_IS_LIVE \"stream.is-live\"")]
    public static ReadOnlySpan<byte> PW_KEY_STREAM_IS_LIVE => "stream.is-live"u8;

    [NativeTypeName("#define PW_KEY_STREAM_LATENCY_MIN \"stream.latency.min\"")]
    public static ReadOnlySpan<byte> PW_KEY_STREAM_LATENCY_MIN => "stream.latency.min"u8;

    [NativeTypeName("#define PW_KEY_STREAM_LATENCY_MAX \"stream.latency.max\"")]
    public static ReadOnlySpan<byte> PW_KEY_STREAM_LATENCY_MAX => "stream.latency.max"u8;

    [NativeTypeName("#define PW_KEY_STREAM_MONITOR \"stream.monitor\"")]
    public static ReadOnlySpan<byte> PW_KEY_STREAM_MONITOR => "stream.monitor"u8;

    [NativeTypeName("#define PW_KEY_STREAM_DONT_REMIX \"stream.dont-remix\"")]
    public static ReadOnlySpan<byte> PW_KEY_STREAM_DONT_REMIX => "stream.dont-remix"u8;

    [NativeTypeName("#define PW_KEY_STREAM_CAPTURE_SINK \"stream.capture.sink\"")]
    public static ReadOnlySpan<byte> PW_KEY_STREAM_CAPTURE_SINK => "stream.capture.sink"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_TYPE \"media.type\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_TYPE => "media.type"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_CATEGORY \"media.category\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_CATEGORY => "media.category"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_ROLE \"media.role\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_ROLE => "media.role"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_CLASS \"media.class\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_CLASS => "media.class"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_NAME \"media.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_NAME => "media.name"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_TITLE \"media.title\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_TITLE => "media.title"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_ARTIST \"media.artist\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_ARTIST => "media.artist"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_ALBUM \"media.album\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_ALBUM => "media.album"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_COPYRIGHT \"media.copyright\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_COPYRIGHT => "media.copyright"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_SOFTWARE \"media.software\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_SOFTWARE => "media.software"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_LANGUAGE \"media.language\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_LANGUAGE => "media.language"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_FILENAME \"media.filename\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_FILENAME => "media.filename"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_ICON \"media.icon\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_ICON => "media.icon"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_ICON_NAME \"media.icon-name\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_ICON_NAME => "media.icon-name"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_COMMENT \"media.comment\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_COMMENT => "media.comment"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_DATE \"media.date\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_DATE => "media.date"u8;

    [NativeTypeName("#define PW_KEY_MEDIA_FORMAT \"media.format\"")]
    public static ReadOnlySpan<byte> PW_KEY_MEDIA_FORMAT => "media.format"u8;

    [NativeTypeName("#define PW_KEY_FORMAT_DSP \"format.dsp\"")]
    public static ReadOnlySpan<byte> PW_KEY_FORMAT_DSP => "format.dsp"u8;

    [NativeTypeName("#define PW_KEY_AUDIO_CHANNEL \"audio.channel\"")]
    public static ReadOnlySpan<byte> PW_KEY_AUDIO_CHANNEL => "audio.channel"u8;

    [NativeTypeName("#define PW_KEY_AUDIO_RATE \"audio.rate\"")]
    public static ReadOnlySpan<byte> PW_KEY_AUDIO_RATE => "audio.rate"u8;

    [NativeTypeName("#define PW_KEY_AUDIO_CHANNELS \"audio.channels\"")]
    public static ReadOnlySpan<byte> PW_KEY_AUDIO_CHANNELS => "audio.channels"u8;

    [NativeTypeName("#define PW_KEY_AUDIO_FORMAT \"audio.format\"")]
    public static ReadOnlySpan<byte> PW_KEY_AUDIO_FORMAT => "audio.format"u8;

    [NativeTypeName("#define PW_KEY_AUDIO_ALLOWED_RATES \"audio.allowed-rates\"")]
    public static ReadOnlySpan<byte> PW_KEY_AUDIO_ALLOWED_RATES => "audio.allowed-rates"u8;

    [NativeTypeName("#define PW_KEY_VIDEO_RATE \"video.framerate\"")]
    public static ReadOnlySpan<byte> PW_KEY_VIDEO_RATE => "video.framerate"u8;

    [NativeTypeName("#define PW_KEY_VIDEO_FORMAT \"video.format\"")]
    public static ReadOnlySpan<byte> PW_KEY_VIDEO_FORMAT => "video.format"u8;

    [NativeTypeName("#define PW_KEY_VIDEO_SIZE \"video.size\"")]
    public static ReadOnlySpan<byte> PW_KEY_VIDEO_SIZE => "video.size"u8;

    [NativeTypeName("#define PW_KEY_TARGET_OBJECT \"target.object\"")]
    public static ReadOnlySpan<byte> PW_KEY_TARGET_OBJECT => "target.object"u8;

    [NativeTypeName("#define SPA_LOG_TOPIC_DEFAULT NULL")]
    public static readonly void* SPA_LOG_TOPIC_DEFAULT = null;

    [NativeTypeName("#define SPA_TYPE_INTERFACE_Log SPA_TYPE_INFO_INTERFACE_BASE \"Log\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INTERFACE_Log => "Spa:Pointer:Interface:Log"u8;

    [NativeTypeName("#define SPA_VERSION_LOG 0")]
    public const int SPA_VERSION_LOG = 0;

    [NativeTypeName("#define SPA_VERSION_LOG_TOPIC 0")]
    public const int SPA_VERSION_LOG_TOPIC = 0;

    [NativeTypeName("#define SPA_VERSION_LOG_TOPIC_ENUM 0")]
    public const int SPA_VERSION_LOG_TOPIC_ENUM = 0;

    [NativeTypeName("#define SPA_VERSION_LOG_METHODS 1")]
    public const int SPA_VERSION_LOG_METHODS = 1;

    [NativeTypeName("#define SPA_LOG_TOPIC_ENUM_NAME \"spa_log_topic_enum\"")]
    public static ReadOnlySpan<byte> SPA_LOG_TOPIC_ENUM_NAME => "spa_log_topic_enum"u8;

    [NativeTypeName("#define SPA_KEY_LOG_LEVEL \"log.level\"")]
    public static ReadOnlySpan<byte> SPA_KEY_LOG_LEVEL => "log.level"u8;

    [NativeTypeName("#define SPA_KEY_LOG_COLORS \"log.colors\"")]
    public static ReadOnlySpan<byte> SPA_KEY_LOG_COLORS => "log.colors"u8;

    [NativeTypeName("#define SPA_KEY_LOG_FILE \"log.file\"")]
    public static ReadOnlySpan<byte> SPA_KEY_LOG_FILE => "log.file"u8;

    [NativeTypeName("#define SPA_KEY_LOG_TIMESTAMP \"log.timestamp\"")]
    public static ReadOnlySpan<byte> SPA_KEY_LOG_TIMESTAMP => "log.timestamp"u8;

    [NativeTypeName("#define SPA_KEY_LOG_LINE \"log.line\"")]
    public static ReadOnlySpan<byte> SPA_KEY_LOG_LINE => "log.line"u8;

    [NativeTypeName("#define SPA_KEY_LOG_PATTERNS \"log.patterns\"")]
    public static ReadOnlySpan<byte> SPA_KEY_LOG_PATTERNS => "log.patterns"u8;

    [NativeTypeName("#define PW_TYPE_INTERFACE_Link PW_TYPE_INFO_INTERFACE_BASE \"Link\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INTERFACE_Link => "PipeWire:Interface:Link"u8;

    [NativeTypeName("#define PW_LINK_PERM_MASK PW_PERM_R | PW_PERM_X")]
    public const int PW_LINK_PERM_MASK = 0400 | 0100;

    [NativeTypeName("#define PW_VERSION_LINK 3")]
    public const int PW_VERSION_LINK = 3;

    [NativeTypeName("#define PW_LINK_CHANGE_MASK_STATE (1 << 0)")]
    public const int PW_LINK_CHANGE_MASK_STATE = (1 << 0);

    [NativeTypeName("#define PW_LINK_CHANGE_MASK_FORMAT (1 << 1)")]
    public const int PW_LINK_CHANGE_MASK_FORMAT = (1 << 1);

    [NativeTypeName("#define PW_LINK_CHANGE_MASK_PROPS (1 << 2)")]
    public const int PW_LINK_CHANGE_MASK_PROPS = (1 << 2);

    [NativeTypeName("#define PW_LINK_CHANGE_MASK_ALL ((1 << 3)-1)")]
    public const int PW_LINK_CHANGE_MASK_ALL = ((1 << 3) - 1);

    [NativeTypeName("#define PW_LINK_EVENT_INFO 0")]
    public const int PW_LINK_EVENT_INFO = 0;

    [NativeTypeName("#define PW_LINK_EVENT_NUM 1")]
    public const int PW_LINK_EVENT_NUM = 1;

    [NativeTypeName("#define PW_VERSION_LINK_EVENTS 0")]
    public const int PW_VERSION_LINK_EVENTS = 0;

    [NativeTypeName("#define PW_LINK_METHOD_ADD_LISTENER 0")]
    public const int PW_LINK_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define PW_LINK_METHOD_NUM 1")]
    public const int PW_LINK_METHOD_NUM = 1;

    [NativeTypeName("#define PW_VERSION_LINK_METHODS 0")]
    public const int PW_VERSION_LINK_METHODS = 0;

    [NativeTypeName("#define PW_VERSION_MAIN_LOOP_EVENTS 0")]
    public const int PW_VERSION_MAIN_LOOP_EVENTS = 0;

    [NativeTypeName("#define PW_TYPE_INTERFACE_Module PW_TYPE_INFO_INTERFACE_BASE \"Module\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INTERFACE_Module => "PipeWire:Interface:Module"u8;

    [NativeTypeName("#define PW_MODULE_PERM_MASK PW_PERM_R|PW_PERM_M")]
    public const int PW_MODULE_PERM_MASK = 0400 | 0010;

    [NativeTypeName("#define PW_VERSION_MODULE 3")]
    public const int PW_VERSION_MODULE = 3;

    [NativeTypeName("#define PW_MODULE_CHANGE_MASK_PROPS (1 << 0)")]
    public const int PW_MODULE_CHANGE_MASK_PROPS = (1 << 0);

    [NativeTypeName("#define PW_MODULE_CHANGE_MASK_ALL ((1 << 1)-1)")]
    public const int PW_MODULE_CHANGE_MASK_ALL = ((1 << 1) - 1);

    [NativeTypeName("#define PW_MODULE_EVENT_INFO 0")]
    public const int PW_MODULE_EVENT_INFO = 0;

    [NativeTypeName("#define PW_MODULE_EVENT_NUM 1")]
    public const int PW_MODULE_EVENT_NUM = 1;

    [NativeTypeName("#define PW_VERSION_MODULE_EVENTS 0")]
    public const int PW_VERSION_MODULE_EVENTS = 0;

    [NativeTypeName("#define PW_MODULE_METHOD_ADD_LISTENER 0")]
    public const int PW_MODULE_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define PW_MODULE_METHOD_NUM 1")]
    public const int PW_MODULE_METHOD_NUM = 1;

    [NativeTypeName("#define PW_VERSION_MODULE_METHODS 0")]
    public const int PW_VERSION_MODULE_METHODS = 0;

    [NativeTypeName("#define PW_TYPE_INTERFACE_Node PW_TYPE_INFO_INTERFACE_BASE \"Node\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INTERFACE_Node => "PipeWire:Interface:Node"u8;

    [NativeTypeName("#define PW_NODE_PERM_MASK PW_PERM_RWXML")]
    public const int PW_NODE_PERM_MASK = ((((0400 | 0200) | 0100) | 0010) | 0020);

    [NativeTypeName("#define PW_VERSION_NODE 3")]
    public const int PW_VERSION_NODE = 3;

    [NativeTypeName("#define PW_NODE_CHANGE_MASK_INPUT_PORTS (1 << 0)")]
    public const int PW_NODE_CHANGE_MASK_INPUT_PORTS = (1 << 0);

    [NativeTypeName("#define PW_NODE_CHANGE_MASK_OUTPUT_PORTS (1 << 1)")]
    public const int PW_NODE_CHANGE_MASK_OUTPUT_PORTS = (1 << 1);

    [NativeTypeName("#define PW_NODE_CHANGE_MASK_STATE (1 << 2)")]
    public const int PW_NODE_CHANGE_MASK_STATE = (1 << 2);

    [NativeTypeName("#define PW_NODE_CHANGE_MASK_PROPS (1 << 3)")]
    public const int PW_NODE_CHANGE_MASK_PROPS = (1 << 3);

    [NativeTypeName("#define PW_NODE_CHANGE_MASK_PARAMS (1 << 4)")]
    public const int PW_NODE_CHANGE_MASK_PARAMS = (1 << 4);

    [NativeTypeName("#define PW_NODE_CHANGE_MASK_ALL ((1 << 5)-1)")]
    public const int PW_NODE_CHANGE_MASK_ALL = ((1 << 5) - 1);

    [NativeTypeName("#define PW_NODE_EVENT_INFO 0")]
    public const int PW_NODE_EVENT_INFO = 0;

    [NativeTypeName("#define PW_NODE_EVENT_PARAM 1")]
    public const int PW_NODE_EVENT_PARAM = 1;

    [NativeTypeName("#define PW_NODE_EVENT_NUM 2")]
    public const int PW_NODE_EVENT_NUM = 2;

    [NativeTypeName("#define PW_VERSION_NODE_EVENTS 0")]
    public const int PW_VERSION_NODE_EVENTS = 0;

    [NativeTypeName("#define PW_NODE_METHOD_ADD_LISTENER 0")]
    public const int PW_NODE_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define PW_NODE_METHOD_SUBSCRIBE_PARAMS 1")]
    public const int PW_NODE_METHOD_SUBSCRIBE_PARAMS = 1;

    [NativeTypeName("#define PW_NODE_METHOD_ENUM_PARAMS 2")]
    public const int PW_NODE_METHOD_ENUM_PARAMS = 2;

    [NativeTypeName("#define PW_NODE_METHOD_SET_PARAM 3")]
    public const int PW_NODE_METHOD_SET_PARAM = 3;

    [NativeTypeName("#define PW_NODE_METHOD_SEND_COMMAND 4")]
    public const int PW_NODE_METHOD_SEND_COMMAND = 4;

    [NativeTypeName("#define PW_NODE_METHOD_NUM 5")]
    public const int PW_NODE_METHOD_NUM = 5;

    [NativeTypeName("#define PW_VERSION_NODE_METHODS 0")]
    public const int PW_VERSION_NODE_METHODS = 0;

    [NativeTypeName("#define PW_TYPE_INTERFACE_Port PW_TYPE_INFO_INTERFACE_BASE \"Port\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INTERFACE_Port => "PipeWire:Interface:Port"u8;

    [NativeTypeName("#define PW_PORT_PERM_MASK PW_PERM_R|PW_PERM_X|PW_PERM_M")]
    public const int PW_PORT_PERM_MASK = 0400 | 0100 | 0010;

    [NativeTypeName("#define PW_VERSION_PORT 3")]
    public const int PW_VERSION_PORT = 3;

    [NativeTypeName("#define PW_DIRECTION_INPUT SPA_DIRECTION_INPUT")]
    public const int PW_DIRECTION_INPUT = (int)SPA_DIRECTION_INPUT;

    [NativeTypeName("#define PW_DIRECTION_OUTPUT SPA_DIRECTION_OUTPUT")]
    public const int PW_DIRECTION_OUTPUT = (int)SPA_DIRECTION_OUTPUT;

    [NativeTypeName("#define PW_PORT_CHANGE_MASK_PROPS (1 << 0)")]
    public const int PW_PORT_CHANGE_MASK_PROPS = (1 << 0);

    [NativeTypeName("#define PW_PORT_CHANGE_MASK_PARAMS (1 << 1)")]
    public const int PW_PORT_CHANGE_MASK_PARAMS = (1 << 1);

    [NativeTypeName("#define PW_PORT_CHANGE_MASK_ALL ((1 << 2)-1)")]
    public const int PW_PORT_CHANGE_MASK_ALL = ((1 << 2) - 1);

    [NativeTypeName("#define PW_PORT_EVENT_INFO 0")]
    public const int PW_PORT_EVENT_INFO = 0;

    [NativeTypeName("#define PW_PORT_EVENT_PARAM 1")]
    public const int PW_PORT_EVENT_PARAM = 1;

    [NativeTypeName("#define PW_PORT_EVENT_NUM 2")]
    public const int PW_PORT_EVENT_NUM = 2;

    [NativeTypeName("#define PW_VERSION_PORT_EVENTS 0")]
    public const int PW_VERSION_PORT_EVENTS = 0;

    [NativeTypeName("#define PW_PORT_METHOD_ADD_LISTENER 0")]
    public const int PW_PORT_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define PW_PORT_METHOD_SUBSCRIBE_PARAMS 1")]
    public const int PW_PORT_METHOD_SUBSCRIBE_PARAMS = 1;

    [NativeTypeName("#define PW_PORT_METHOD_ENUM_PARAMS 2")]
    public const int PW_PORT_METHOD_ENUM_PARAMS = 2;

    [NativeTypeName("#define PW_PORT_METHOD_NUM 3")]
    public const int PW_PORT_METHOD_NUM = 3;

    [NativeTypeName("#define PW_VERSION_PORT_METHODS 0")]
    public const int PW_VERSION_PORT_METHODS = 0;

    [NativeTypeName("#define PW_VERSION_STREAM_EVENTS 2")]
    public const int PW_VERSION_STREAM_EVENTS = 2;

    [NativeTypeName("#define SPA_STATUS_OK 0")]
    public const int SPA_STATUS_OK = 0;

    [NativeTypeName("#define SPA_STATUS_NEED_DATA (1<<0)")]
    public const int SPA_STATUS_NEED_DATA = (1 << 0);

    [NativeTypeName("#define SPA_STATUS_HAVE_DATA (1<<1)")]
    public const int SPA_STATUS_HAVE_DATA = (1 << 1);

    [NativeTypeName("#define SPA_STATUS_STOPPED (1<<2)")]
    public const int SPA_STATUS_STOPPED = (1 << 2);

    [NativeTypeName("#define SPA_STATUS_DRAINED (1<<3)")]
    public const int SPA_STATUS_DRAINED = (1 << 3);

    [NativeTypeName("#define SPA_IO_CLOCK_FLAG_FREEWHEEL (1u<<0)")]
    public const uint SPA_IO_CLOCK_FLAG_FREEWHEEL = (1U << 0);

    [NativeTypeName("#define SPA_IO_CLOCK_FLAG_XRUN_RECOVER (1u<<1)")]
    public const uint SPA_IO_CLOCK_FLAG_XRUN_RECOVER = (1U << 1);

    [NativeTypeName("#define SPA_IO_CLOCK_FLAG_LAZY (1u<<2)")]
    public const uint SPA_IO_CLOCK_FLAG_LAZY = (1U << 2);

    [NativeTypeName("#define SPA_IO_CLOCK_FLAG_NO_RATE (1u<<3)")]
    public const uint SPA_IO_CLOCK_FLAG_NO_RATE = (1U << 3);

    [NativeTypeName("#define SPA_IO_CLOCK_FLAG_DISCONT (1u<<4)")]
    public const uint SPA_IO_CLOCK_FLAG_DISCONT = (1U << 4);

    [NativeTypeName("#define SPA_IO_VIDEO_SIZE_VALID (1<<0)")]
    public const int SPA_IO_VIDEO_SIZE_VALID = (1 << 0);

    [NativeTypeName("#define SPA_IO_SEGMENT_BAR_FLAG_VALID (1<<0)")]
    public const int SPA_IO_SEGMENT_BAR_FLAG_VALID = (1 << 0);

    [NativeTypeName("#define SPA_IO_SEGMENT_VIDEO_FLAG_VALID (1<<0)")]
    public const int SPA_IO_SEGMENT_VIDEO_FLAG_VALID = (1 << 0);

    [NativeTypeName("#define SPA_IO_SEGMENT_VIDEO_FLAG_DROP_FRAME (1<<1)")]
    public const int SPA_IO_SEGMENT_VIDEO_FLAG_DROP_FRAME = (1 << 1);

    [NativeTypeName("#define SPA_IO_SEGMENT_VIDEO_FLAG_PULL_DOWN (1<<2)")]
    public const int SPA_IO_SEGMENT_VIDEO_FLAG_PULL_DOWN = (1 << 2);

    [NativeTypeName("#define SPA_IO_SEGMENT_VIDEO_FLAG_INTERLACED (1<<3)")]
    public const int SPA_IO_SEGMENT_VIDEO_FLAG_INTERLACED = (1 << 3);

    [NativeTypeName("#define SPA_IO_SEGMENT_FLAG_LOOPING (1<<0)")]
    public const int SPA_IO_SEGMENT_FLAG_LOOPING = (1 << 0);

    [NativeTypeName("#define SPA_IO_SEGMENT_FLAG_NO_POSITION (1<<1)")]
    public const int SPA_IO_SEGMENT_FLAG_NO_POSITION = (1 << 1);

    [NativeTypeName("#define SPA_IO_POSITION_MAX_SEGMENTS 8")]
    public const int SPA_IO_POSITION_MAX_SEGMENTS = 8;

    [NativeTypeName("#define SPA_IO_RATE_MATCH_FLAG_ACTIVE (1 << 0)")]
    public const int SPA_IO_RATE_MATCH_FLAG_ACTIVE = (1 << 0);

    [NativeTypeName("#define PW_VERSION_FILTER_EVENTS 1")]
    public const int PW_VERSION_FILTER_EVENTS = 1;

    [NativeTypeName("#define PW_VERSION_THREAD_LOOP_EVENTS 0")]
    public const int PW_VERSION_THREAD_LOOP_EVENTS = 0;

    [NativeTypeName("#define SPA_TYPE_INFO_Thread SPA_TYPE_INFO_POINTER_BASE \"Thread\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INFO_Thread => "Spa:Pointer:Thread"u8;

    [NativeTypeName("#define SPA_TYPE_INTERFACE_ThreadUtils SPA_TYPE_INFO_INTERFACE_BASE \"ThreadUtils\"")]
    public static ReadOnlySpan<byte> SPA_TYPE_INTERFACE_ThreadUtils => "Spa:Pointer:Interface:ThreadUtils"u8;

    [NativeTypeName("#define SPA_VERSION_THREAD_UTILS 0")]
    public const int SPA_VERSION_THREAD_UTILS = 0;

    [NativeTypeName("#define SPA_VERSION_THREAD_UTILS_METHODS 0")]
    public const int SPA_VERSION_THREAD_UTILS_METHODS = 0;

    [NativeTypeName("#define SPA_KEY_THREAD_NAME \"thread.name\"")]
    public static ReadOnlySpan<byte> SPA_KEY_THREAD_NAME => "thread.name"u8;

    [NativeTypeName("#define SPA_KEY_THREAD_STACK_SIZE \"thread.stack-size\"")]
    public static ReadOnlySpan<byte> SPA_KEY_THREAD_STACK_SIZE => "thread.stack-size"u8;

    [NativeTypeName("#define SPA_KEY_THREAD_AFFINITY \"thread.affinity\"")]
    public static ReadOnlySpan<byte> SPA_KEY_THREAD_AFFINITY => "thread.affinity"u8;

    [NativeTypeName("#define SPA_KEY_THREAD_CREATOR \"thread.creator\"")]
    public static ReadOnlySpan<byte> SPA_KEY_THREAD_CREATOR => "thread.creator"u8;

    [NativeTypeName("#define SPA_KEY_THREAD_RESET_ON_FORK \"thread.reset-on-fork\"")]
    public static ReadOnlySpan<byte> SPA_KEY_THREAD_RESET_ON_FORK => "thread.reset-on-fork"u8;

    [NativeTypeName("#define PW_VERSION_DATA_LOOP_EVENTS 0")]
    public const int PW_VERSION_DATA_LOOP_EVENTS = 0;

    [NativeTypeName("#define PW_API_VERSION \"0.3\"")]
    public static ReadOnlySpan<byte> PW_API_VERSION => "0.3"u8;

    [NativeTypeName("#define PW_MAJOR 1")]
    public const int PW_MAJOR = 1;

    [NativeTypeName("#define PW_MINOR 6")]
    public const int PW_MINOR = 6;

    [NativeTypeName("#define PW_MICRO 8")]
    public const int PW_MICRO = 8;

    [NativeTypeName("#define PW_TYPE_INTERFACE_Metadata PW_TYPE_INFO_INTERFACE_BASE \"Metadata\"")]
    public static ReadOnlySpan<byte> PW_TYPE_INTERFACE_Metadata => "PipeWire:Interface:Metadata"u8;

    [NativeTypeName("#define PW_METADATA_PERM_MASK PW_PERM_RWX")]
    public const int PW_METADATA_PERM_MASK = ((0400 | 0200) | 0100);

    [NativeTypeName("#define PW_VERSION_METADATA 3")]
    public const int PW_VERSION_METADATA = 3;

    [NativeTypeName("#define PW_METADATA_EVENT_PROPERTY 0")]
    public const int PW_METADATA_EVENT_PROPERTY = 0;

    [NativeTypeName("#define PW_METADATA_EVENT_NUM 1")]
    public const int PW_METADATA_EVENT_NUM = 1;

    [NativeTypeName("#define PW_VERSION_METADATA_EVENTS 0")]
    public const int PW_VERSION_METADATA_EVENTS = 0;

    [NativeTypeName("#define PW_METADATA_METHOD_ADD_LISTENER 0")]
    public const int PW_METADATA_METHOD_ADD_LISTENER = 0;

    [NativeTypeName("#define PW_METADATA_METHOD_SET_PROPERTY 1")]
    public const int PW_METADATA_METHOD_SET_PROPERTY = 1;

    [NativeTypeName("#define PW_METADATA_METHOD_CLEAR 2")]
    public const int PW_METADATA_METHOD_CLEAR = 2;

    [NativeTypeName("#define PW_METADATA_METHOD_NUM 3")]
    public const int PW_METADATA_METHOD_NUM = 3;

    [NativeTypeName("#define PW_VERSION_METADATA_METHODS 0")]
    public const int PW_VERSION_METADATA_METHODS = 0;

    [NativeTypeName("#define PW_KEY_METADATA_NAME \"metadata.name\"")]
    public static ReadOnlySpan<byte> PW_KEY_METADATA_NAME => "metadata.name"u8;

    [NativeTypeName("#define PW_KEY_METADATA_VALUES \"metadata.values\"")]
    public static ReadOnlySpan<byte> PW_KEY_METADATA_VALUES => "metadata.values"u8;

    [NativeTypeName("#define SPA_KEY_FORMAT_DSP \"format.dsp\"")]
    public static ReadOnlySpan<byte> SPA_KEY_FORMAT_DSP => "format.dsp"u8;

    [NativeTypeName("#define SPA_AUDIO_MAX_CHANNELS 64u")]
    public const uint SPA_AUDIO_MAX_CHANNELS = 64U;

    [NativeTypeName("#define SPA_AUDIO_FLAG_NONE (0)")]
    public const int SPA_AUDIO_FLAG_NONE = (0);

    [NativeTypeName("#define SPA_AUDIO_FLAG_UNPOSITIONED (1 << 0)")]
    public const int SPA_AUDIO_FLAG_UNPOSITIONED = (1 << 0);

    [NativeTypeName("#define SPA_KEY_AUDIO_FORMAT \"audio.format\"")]
    public static ReadOnlySpan<byte> SPA_KEY_AUDIO_FORMAT => "audio.format"u8;

    [NativeTypeName("#define SPA_KEY_AUDIO_CHANNEL \"audio.channel\"")]
    public static ReadOnlySpan<byte> SPA_KEY_AUDIO_CHANNEL => "audio.channel"u8;

    [NativeTypeName("#define SPA_KEY_AUDIO_CHANNELS \"audio.channels\"")]
    public static ReadOnlySpan<byte> SPA_KEY_AUDIO_CHANNELS => "audio.channels"u8;

    [NativeTypeName("#define SPA_KEY_AUDIO_RATE \"audio.rate\"")]
    public static ReadOnlySpan<byte> SPA_KEY_AUDIO_RATE => "audio.rate"u8;

    [NativeTypeName("#define SPA_KEY_AUDIO_LAYOUT \"audio.layout\"")]
    public static ReadOnlySpan<byte> SPA_KEY_AUDIO_LAYOUT => "audio.layout"u8;

    [NativeTypeName("#define SPA_KEY_AUDIO_POSITION \"audio.position\"")]
    public static ReadOnlySpan<byte> SPA_KEY_AUDIO_POSITION => "audio.position"u8;

    [NativeTypeName("#define SPA_KEY_AUDIO_ALLOWED_RATES \"audio.allowed-rates\"")]
    public static ReadOnlySpan<byte> SPA_KEY_AUDIO_ALLOWED_RATES => "audio.allowed-rates"u8;

    [NativeTypeName("#define SPA_AUDIO_DSD_FLAG_NONE (0)")]
    public const int SPA_AUDIO_DSD_FLAG_NONE = (0);

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_Mono 1")]
    public const int SPA_AUDIO_LAYOUT_Mono = 1;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_Stereo 2")]
    public const int SPA_AUDIO_LAYOUT_Stereo = 2;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_Quad 4")]
    public const int SPA_AUDIO_LAYOUT_Quad = 4;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_Pentagonal 5")]
    public const int SPA_AUDIO_LAYOUT_Pentagonal = 5;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_Hexagonal 6")]
    public const int SPA_AUDIO_LAYOUT_Hexagonal = 6;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_Octagonal 8")]
    public const int SPA_AUDIO_LAYOUT_Octagonal = 8;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_Cube 8")]
    public const int SPA_AUDIO_LAYOUT_Cube = 8;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_1_0 SPA_AUDIO_LAYOUT_Mono")]
    public const int SPA_AUDIO_LAYOUT_MPEG_1_0 = 1;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_2_0 SPA_AUDIO_LAYOUT_Stereo")]
    public const int SPA_AUDIO_LAYOUT_MPEG_2_0 = 2;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_3_0A 3")]
    public const int SPA_AUDIO_LAYOUT_MPEG_3_0A = 3;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_3_0B 3")]
    public const int SPA_AUDIO_LAYOUT_MPEG_3_0B = 3;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_4_0A 4")]
    public const int SPA_AUDIO_LAYOUT_MPEG_4_0A = 4;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_4_0B 4")]
    public const int SPA_AUDIO_LAYOUT_MPEG_4_0B = 4;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_5_0A 5")]
    public const int SPA_AUDIO_LAYOUT_MPEG_5_0A = 5;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_5_0B 5")]
    public const int SPA_AUDIO_LAYOUT_MPEG_5_0B = 5;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_5_0C 5")]
    public const int SPA_AUDIO_LAYOUT_MPEG_5_0C = 5;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_5_0D 5")]
    public const int SPA_AUDIO_LAYOUT_MPEG_5_0D = 5;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_5_1A 6")]
    public const int SPA_AUDIO_LAYOUT_MPEG_5_1A = 6;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_5_1B 6")]
    public const int SPA_AUDIO_LAYOUT_MPEG_5_1B = 6;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_5_1C 6")]
    public const int SPA_AUDIO_LAYOUT_MPEG_5_1C = 6;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_5_1D 6")]
    public const int SPA_AUDIO_LAYOUT_MPEG_5_1D = 6;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_6_1A 7")]
    public const int SPA_AUDIO_LAYOUT_MPEG_6_1A = 7;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_7_1A 8")]
    public const int SPA_AUDIO_LAYOUT_MPEG_7_1A = 8;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_7_1B 8")]
    public const int SPA_AUDIO_LAYOUT_MPEG_7_1B = 8;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_MPEG_7_1C 8")]
    public const int SPA_AUDIO_LAYOUT_MPEG_7_1C = 8;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_2_1 3")]
    public const int SPA_AUDIO_LAYOUT_2_1 = 3;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_2RC 3")]
    public const int SPA_AUDIO_LAYOUT_2RC = 3;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_2FC 3")]
    public const int SPA_AUDIO_LAYOUT_2FC = 3;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_3_1 4")]
    public const int SPA_AUDIO_LAYOUT_3_1 = 4;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_4_0 4")]
    public const int SPA_AUDIO_LAYOUT_4_0 = 4;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_2_2 4")]
    public const int SPA_AUDIO_LAYOUT_2_2 = 4;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_4_1 5")]
    public const int SPA_AUDIO_LAYOUT_4_1 = 5;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_5_0 5")]
    public const int SPA_AUDIO_LAYOUT_5_0 = 5;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_5_0R 5")]
    public const int SPA_AUDIO_LAYOUT_5_0R = 5;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_5_1 6")]
    public const int SPA_AUDIO_LAYOUT_5_1 = 6;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_5_1R 6")]
    public const int SPA_AUDIO_LAYOUT_5_1R = 6;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_6_0 6")]
    public const int SPA_AUDIO_LAYOUT_6_0 = 6;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_6_0F 6")]
    public const int SPA_AUDIO_LAYOUT_6_0F = 6;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_6_1 7")]
    public const int SPA_AUDIO_LAYOUT_6_1 = 7;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_6_1F 7")]
    public const int SPA_AUDIO_LAYOUT_6_1F = 7;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_7_0 7")]
    public const int SPA_AUDIO_LAYOUT_7_0 = 7;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_7_0F 7")]
    public const int SPA_AUDIO_LAYOUT_7_0F = 7;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_7_1 8")]
    public const int SPA_AUDIO_LAYOUT_7_1 = 8;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_7_1W 8")]
    public const int SPA_AUDIO_LAYOUT_7_1W = 8;

    [NativeTypeName("#define SPA_AUDIO_LAYOUT_7_1WR 8")]
    public const int SPA_AUDIO_LAYOUT_7_1WR = 8;

    [NativeTypeName("#define SPA_VIDEO_MAX_PLANES 4")]
    public const int SPA_VIDEO_MAX_PLANES = 4;

    [NativeTypeName("#define SPA_VIDEO_MAX_COMPONENTS 4")]
    public const int SPA_VIDEO_MAX_COMPONENTS = 4;
}
