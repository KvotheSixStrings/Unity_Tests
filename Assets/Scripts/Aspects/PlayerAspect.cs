using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.Zombies
{
    public readonly partial struct PlayerAspect : IAspect
    {
        public readonly Entity Player;

        private readonly RefRW<LocalTransform> _transform;

        private readonly RefRW<PlayerDataComponent> _PlayerData;
        
        public float3 Position => _PlayerData.ValueRW.Position;
        public float MovementSpeed => _PlayerData.ValueRW.MovementSpeed;
        public quaternion Rotation => _PlayerData.ValueRW.Rotation;
        public float RotationSpeed => _PlayerData.ValueRW.RotationSpeed;
        public float3 Scale => _PlayerData.ValueRW.Scale;
        public float ScaleSpeed => _PlayerData.ValueRW.ScaleSpeed;
        
        public void SetPosition(float3 position)
        {
            _transform.ValueRW.Position = position;
        }
        
        public void SetRotation(quaternion rotation)
        {
            _transform.ValueRW.Rotation = rotation;
        }
        
        public void SetScale(float3 scale)
        {
            _transform.ValueRW.Scale = scale.x;
        }
        
        public void SetTransform(LocalTransform transform)
        {
            _transform.ValueRW = transform;
        }
    }
}