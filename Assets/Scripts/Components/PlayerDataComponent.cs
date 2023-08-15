using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace TMG.Zombies
{
    public struct PlayerDataComponent : IComponentData
    {
        public float3 Position;
        public float MovementSpeed;
        public quaternion Rotation;
        public float RotationSpeed;
        public float3 Scale;
        public float ScaleSpeed;
    }
}