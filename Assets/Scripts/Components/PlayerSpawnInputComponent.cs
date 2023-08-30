using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace TMG.Zombies
{
    public struct PlayerSpawnInputComponent : IComponentData
    {
        public int NumberToSpawn;
    }
}