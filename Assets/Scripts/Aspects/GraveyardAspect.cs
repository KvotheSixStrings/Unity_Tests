using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.Zombies
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _transform;
        private LocalTransform Transform => _transform.ValueRO;

        private readonly RefRW<GraveyardPropertiesComponent> _graveyardProperties;
        private readonly RefRW<GraveyardRandomComponent> _graveyardRandom;

        private int _tombstoneCount
        {
            get
            {
                _graveyardProperties.ValueRW.TombstoneCount++;
                return _graveyardProperties.ValueRO.TombstoneCount;
            }
        }
        
        public int NextName => _tombstoneCount;

        public int NumberTombstonesToSpawn => _graveyardProperties.ValueRO.NumberTombstonesToSpawn;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

        public LocalTransform GetRandomTombstoneTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = GetRandomRotation(),
                Scale = GetRandomScale(0.25f)
            };
        }

        public float2 GetRandomOffset()
        {
            return _graveyardRandom.ValueRW.Value.NextFloat2();
        }

        public float GetRandomRotationSpeed()
        {
            return _graveyardRandom.ValueRW.Value.NextFloat(0, 1f);
        }

        public float3 Position => Transform.Position;
        
        private float3 GetRandomPosition()
        {
            return _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
        }
        
        private float3 MinCorner => Transform.Position - HalfDimensions;
        private float3 MaxCorner => Transform.Position + HalfDimensions;
        private float3 HalfDimensions => new()
        {
            x = _graveyardProperties.ValueRO.FieldDimensions.x * 0.5f,
            y = _graveyardProperties.ValueRO.FieldDimensions.y * 0.5f,
            z = _graveyardProperties.ValueRO.FieldDimensions.z * 0.5f
        };

        private quaternion GetRandomRotation() => quaternion.RotateY(_graveyardRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));
        private float GetRandomScale(float min) => _graveyardRandom.ValueRW.Value.NextFloat(min, 1f);
    }
}