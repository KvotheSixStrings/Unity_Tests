using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace TMG.Zombies
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerSpawnTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraveyardPropertiesComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardPropertiesComponent>();
            var graveyard = SystemAPI.GetAspect<GraveyardAspect>(graveyardEntity);
            
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (spawnInput, transform) in
                     SystemAPI.Query<PlayerSpawnInputComponent, LocalTransform>().WithAll<PlayerSpawnTagInputComponent>())
            {
                for (var i = 0; i < spawnInput.NumberToSpawn; i++)
                {
                    var newTombstone = ecb.Instantiate(spawnInput.ObjectToSpawn);
                    var newTombstoneTransform = graveyard.GetRandomTombstoneTransform();
                    ecb.SetComponent(newTombstone, newTombstoneTransform);

                    ecb.AddComponent(newTombstone, new TombstoneRotationSpeedComponent { Value = graveyard.GetRandomRotationSpeed() });
                }
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}