using PipeWire.Native;

namespace PipeWire.Spa;

public static unsafe class SpaInterface
{
    public static bool TryGetMethods<TMethods>(spa_interface* iface, uint minVersion, out TMethods* methods, out void* data)
        where TMethods : unmanaged
    {
        if (iface is null || iface->cb.funcs is null)
        {
            methods = null;
            data = null;
            return false;
        }

        methods = (TMethods*)iface->cb.funcs;
        data = iface->cb.data;

        return *(uint*)methods >= minVersion;
    }

    public static TMethods* GetMethods<TMethods>(spa_interface* iface, uint minVersion, string method, out void* data)
        where TMethods : unmanaged
    {
        if (!TryGetMethods(iface, minVersion, out TMethods* methods, out data))
        {
            throw new NotSupportedException(
                $"The interface does not implement {method}: it needs a {typeof(TMethods).Name} of version {minVersion} or newer.");
        }

        return methods;
    }

    public static void RemoveHook(spa_hook* hook)
    {
        if (hook is null)
        {
            return;
        }

        if (hook->link.prev is not null)
        {
            spa_list* link = &hook->link;
            link->prev->next = link->next;
            link->next->prev = link->prev;
            link->prev = null;
            link->next = null;
        }

        if (hook->removed is not null)
        {
            hook->removed(hook);
            hook->removed = null;
        }
    }
}
