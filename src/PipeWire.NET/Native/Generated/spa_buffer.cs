#pragma warning disable CS1591

namespace PipeWire.Native;

public unsafe partial struct spa_buffer
{
    [NativeTypeName("uint32_t")]
    public uint n_metas;

    [NativeTypeName("uint32_t")]
    public uint n_datas;

    [NativeTypeName("struct spa_meta *")]
    public spa_meta* metas;

    [NativeTypeName("struct spa_data *")]
    public spa_data* datas;
}
