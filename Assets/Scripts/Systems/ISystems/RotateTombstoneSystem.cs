using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace TMG.Zombies
{
    [BurstCompile]
    public partial struct RotateTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var mod = Time.deltaTime;
            foreach (var tombstone in SystemAPI.Query<RefRW<TombstoneRotationSpeedComponent>, RefRW<LocalTransform>>())
            {
                var tombstoneRotationSpeed = tombstone.Item1.ValueRW.Value;
                tombstone.Item2.ValueRW.Rotation = tombstone.Item2.ValueRW.RotateY(tombstoneRotationSpeed * mod).Rotation;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}