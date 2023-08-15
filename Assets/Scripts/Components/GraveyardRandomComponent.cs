using Unity.Entities;
using Unity.Mathematics;

namespace TMG.Zombies
{
    public struct GraveyardRandomComponent : IComponentData
    {
        public Random Value;
    }
}