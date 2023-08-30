using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace TMG.Zombies
{
    [BurstCompile]
    [UpdateBefore(typeof(RotateTombstoneSystem))]
    public partial struct RotationTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.Enabled = false;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var rand = Random.CreateFromIndex(1);
            foreach (var tombstone in SystemAPI.Query<RefRW<TombstoneRotationSpeedComponent>>())
            {
                var tombstoneRotationSpeed = rand.NextFloat(0, 1f);
                tombstone.ValueRW.Value = tombstoneRotationSpeed;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}