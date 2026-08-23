/*
 * Reference struct sizes for NativeBindingTests. Prints what the C compiler
 * says about the types the managed bindings mirror, so the checked-in
 * expectations can be regenerated after a submodule bump:
 *
 *   clang -std=gnu17 -I external/pipewire/src -I external/pipewire/spa/include \
 *         -I eng/include -o /tmp/layout-reference eng/layout-reference.c
 *   /tmp/layout-reference
 */
#include <stdio.h>
#include <time.h>
#include <pipewire/pipewire.h>
#include <spa/param/audio/format.h>

#define P(t) printf("%-28s %zu\n", #t, sizeof(struct t))

int main(void)
{
    printf("%-28s %zu\n", "timespec", sizeof(struct timespec));
    P(spa_pod);
    P(spa_pod_bool);
    P(spa_pod_int);
    P(spa_pod_long);
    P(spa_pod_double);
    P(spa_pod_rectangle);
    P(spa_pod_fraction);
    P(spa_pod_array);
    P(spa_pod_array_body);
    P(spa_pod_choice);
    P(spa_pod_choice_body);
    P(spa_pod_object);
    P(spa_pod_object_body);
    P(spa_pod_prop);
    P(spa_pod_control);
    P(spa_pod_sequence);
    P(spa_pod_sequence_body);
    P(spa_dict);
    P(spa_dict_item);
    P(spa_interface);
    P(spa_callbacks);
    P(spa_hook);
    P(spa_list);
    P(spa_rectangle);
    P(spa_fraction);
    P(spa_buffer);
    P(spa_data);
    P(spa_chunk);
    P(spa_audio_info_raw);
    P(spa_param_info);
    P(pw_buffer);
    P(pw_time);
    P(pw_properties);
    P(pw_core_events);
    P(pw_core_methods);
    P(pw_registry_events);
    P(pw_registry_methods);
    P(pw_node_events);
    P(pw_node_methods);
    P(pw_stream_events);
    P(pw_filter_events);
    P(pw_proxy_events);
    P(spa_loop_control_methods);
    return 0;
}
