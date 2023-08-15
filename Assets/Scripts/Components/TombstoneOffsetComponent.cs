using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace TMG.Zombies
{
    [MaterialProperty("TombstoneOffset")]
    public struct TombstoneOffsetComponent : IComponentData
    {
        public float2 Value;
    }
}