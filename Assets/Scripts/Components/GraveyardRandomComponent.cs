using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace TMG.Zombies
{
    public struct GraveyardRandomComponent : IComponentData
    {
        public Random SpawningRandomSeed;
        public Random PositionRandomSeed;
        public Random RotationRandomSeed;
        public Random ScaleRandomSeed;
        public Random RotateByRandomSeed;
    }
}