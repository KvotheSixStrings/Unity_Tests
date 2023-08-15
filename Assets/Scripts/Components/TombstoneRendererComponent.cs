using Unity.Entities;

namespace TMG.Zombies
{
    public struct TombstoneRendererComponent : IComponentData
    {
        public Entity Value;
    }
}