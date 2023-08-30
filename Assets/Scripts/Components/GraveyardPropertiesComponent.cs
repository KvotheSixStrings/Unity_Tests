using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace TMG.Zombies
{
    [ChunkSerializable]
    public struct GraveyardPropertiesComponent : IComponentData
    {
        public float3 FieldDimensions;
        public int NumberTombstonesToSpawn;
        public int TombstoneCount;
    }
}