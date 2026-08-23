/*
 * Reference vectors for SpaPodBuilderTests. Builds the same PODs the managed
 * SpaPodBuilder builds, using the real spa_pod_builder from external/pipewire,
 * and prints them as hex. SPA's POD API is header-only, so this needs no
 * libraries:
 *
 *   clang -std=gnu17 -I external/pipewire/spa/include -o /tmp/pod-reference eng/pod-reference.c
 *   /tmp/pod-reference
 *
 * Paste the output into SpaPodBuilderTests after bumping the submodule.
 */
#include <stdio.h>
#include <string.h>
#include <spa/pod/builder.h>
#include <spa/param/audio/format-utils.h>
#include <spa/param/props.h>
#include <spa/control/control.h>

static uint8_t buf[4096];

static void dump(const char *name, struct spa_pod_builder *b)
{
    printf("%s ", name);
    for (uint32_t i = 0; i < b->state.offset; i++) printf("%02x", buf[i]);
    printf("\n");
}

int main(void)
{
    struct spa_pod_builder b;
    struct spa_pod_frame f[2];

    /* 1: audio EnumFormat object, the canonical stream param */
    spa_pod_builder_init(&b, buf, sizeof(buf));
    spa_pod_builder_push_object(&b, &f[0], SPA_TYPE_OBJECT_Format, SPA_PARAM_EnumFormat);
    spa_pod_builder_prop(&b, SPA_FORMAT_mediaType, 0);
    spa_pod_builder_id(&b, SPA_MEDIA_TYPE_audio);
    spa_pod_builder_prop(&b, SPA_FORMAT_mediaSubtype, 0);
    spa_pod_builder_id(&b, SPA_MEDIA_SUBTYPE_raw);
    spa_pod_builder_prop(&b, SPA_FORMAT_AUDIO_format, 0);
    spa_pod_builder_id(&b, SPA_AUDIO_FORMAT_F32);
    spa_pod_builder_prop(&b, SPA_FORMAT_AUDIO_rate, 0);
    spa_pod_builder_int(&b, 48000);
    spa_pod_builder_prop(&b, SPA_FORMAT_AUDIO_channels, 0);
    spa_pod_builder_int(&b, 2);
    spa_pod_builder_pop(&b, &f[0]);
    dump("enumformat", &b);

    /* 2: struct of mixed primitives */
    spa_pod_builder_init(&b, buf, sizeof(buf));
    spa_pod_builder_push_struct(&b, &f[0]);
    spa_pod_builder_bool(&b, true);
    spa_pod_builder_int(&b, -7);
    spa_pod_builder_long(&b, 0x1122334455667788L);
    spa_pod_builder_float(&b, 0.5f);
    spa_pod_builder_double(&b, 0.25);
    spa_pod_builder_string(&b, "hello");
    spa_pod_builder_bytes(&b, "\x01\x02\x03", 3);
    spa_pod_builder_rectangle(&b, 1920, 1080);
    spa_pod_builder_fraction(&b, 30000, 1001);
    spa_pod_builder_fd(&b, 42);
    spa_pod_builder_none(&b);
    spa_pod_builder_pop(&b, &f[0]);
    dump("struct", &b);

    /* 3: array of ints */
    spa_pod_builder_init(&b, buf, sizeof(buf));
    {
        int32_t vals[] = { 1, 2, 3, 4, 5 };
        spa_pod_builder_array(&b, sizeof(int32_t), SPA_TYPE_Int, 5, vals);
    }
    dump("array", &b);

    /* 4: array built element by element (push/pop path) */
    spa_pod_builder_init(&b, buf, sizeof(buf));
    spa_pod_builder_push_array(&b, &f[0]);
    spa_pod_builder_int(&b, 10);
    spa_pod_builder_int(&b, 20);
    spa_pod_builder_int(&b, 30);
    spa_pod_builder_pop(&b, &f[0]);
    dump("array_push", &b);

    /* 5: empty pushed array */
    spa_pod_builder_init(&b, buf, sizeof(buf));
    spa_pod_builder_push_array(&b, &f[0]);
    spa_pod_builder_pop(&b, &f[0]);
    dump("array_empty", &b);

    /* 6: choice range inside an object property */
    spa_pod_builder_init(&b, buf, sizeof(buf));
    spa_pod_builder_push_object(&b, &f[0], SPA_TYPE_OBJECT_Format, SPA_PARAM_EnumFormat);
    spa_pod_builder_prop(&b, SPA_FORMAT_AUDIO_rate, 0);
    spa_pod_builder_push_choice(&b, &f[1], SPA_CHOICE_Range, 0);
    spa_pod_builder_int(&b, 48000);
    spa_pod_builder_int(&b, 8000);
    spa_pod_builder_int(&b, 192000);
    spa_pod_builder_pop(&b, &f[1]);
    spa_pod_builder_prop(&b, SPA_FORMAT_AUDIO_position, 0);
    {
        uint32_t pos[] = { SPA_AUDIO_CHANNEL_FL, SPA_AUDIO_CHANNEL_FR };
        spa_pod_builder_array(&b, sizeof(uint32_t), SPA_TYPE_Id, 2, pos);
    }
    spa_pod_builder_pop(&b, &f[0]);
    dump("choice_object", &b);

    /* 7: choice enum of ids */
    spa_pod_builder_init(&b, buf, sizeof(buf));
    spa_pod_builder_push_choice(&b, &f[0], SPA_CHOICE_Enum, 0);
    spa_pod_builder_id(&b, SPA_AUDIO_FORMAT_F32);
    spa_pod_builder_id(&b, SPA_AUDIO_FORMAT_S16);
    spa_pod_builder_id(&b, SPA_AUDIO_FORMAT_S32);
    spa_pod_builder_pop(&b, &f[0]);
    dump("choice_enum", &b);

    /* 8: nested struct in struct */
    spa_pod_builder_init(&b, buf, sizeof(buf));
    spa_pod_builder_push_struct(&b, &f[0]);
    spa_pod_builder_int(&b, 1);
    spa_pod_builder_push_struct(&b, &f[1]);
    spa_pod_builder_string(&b, "nested");
    spa_pod_builder_int(&b, 2);
    spa_pod_builder_pop(&b, &f[1]);
    spa_pod_builder_string(&b, "");
    spa_pod_builder_pop(&b, &f[0]);
    dump("nested", &b);

    /* 9: Props object with float and bool */
    spa_pod_builder_init(&b, buf, sizeof(buf));
    spa_pod_builder_push_object(&b, &f[0], SPA_TYPE_OBJECT_Props, SPA_PARAM_Props);
    spa_pod_builder_prop(&b, SPA_PROP_volume, 0);
    spa_pod_builder_float(&b, 0.75f);
    spa_pod_builder_prop(&b, SPA_PROP_mute, 0);
    spa_pod_builder_bool(&b, false);
    spa_pod_builder_pop(&b, &f[0]);
    dump("props", &b);

    /* 10: sequence with controls */
    spa_pod_builder_init(&b, buf, sizeof(buf));
    spa_pod_builder_push_sequence(&b, &f[0], 0);
    spa_pod_builder_control(&b, 0, SPA_CONTROL_Properties);
    spa_pod_builder_int(&b, 5);
    spa_pod_builder_control(&b, 128, SPA_CONTROL_Midi);
    spa_pod_builder_bytes(&b, "\x90\x40\x7f", 3);
    spa_pod_builder_pop(&b, &f[0]);
    dump("sequence", &b);

    return 0;
}
