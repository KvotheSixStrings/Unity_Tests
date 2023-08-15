using Unity.Entities;

namespace TMG.Zombies
{
    public struct PlayerSpawnInputComponent : IComponentData
    {
        public Entity ObjectToSpawn;
        public int NumberToSpawn;
    }
}