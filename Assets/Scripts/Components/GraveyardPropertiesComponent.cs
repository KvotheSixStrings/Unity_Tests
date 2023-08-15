using Unity.Entities;
using Unity.Mathematics;

namespace TMG.Zombies
{
    public struct GraveyardPropertiesComponent : IComponentData
    {
        public float3 FieldDimensions;
        public int NumberTombstonesToSpawn;
        public Entity TombstonePrefab;
        public int TombstoneCount;
    }
}