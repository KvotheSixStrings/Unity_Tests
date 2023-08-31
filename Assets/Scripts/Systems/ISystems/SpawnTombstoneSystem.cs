using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace TMG.Zombies
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraveyardPropertiesComponent>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardPropertiesComponent>();
            var graveyard = SystemAPI.GetAspect<GraveyardAspect>(graveyardEntity);
            
            if (graveyard.NumberOfBatchesToSpawn == 0)
            {
                state.Enabled = false;
                return;
            }

            var prefabOptions = SystemAPI.GetBuffer<EntityReference>(graveyardEntity);
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            int amountToSpawn = graveyard.TombstonesSpawnedThisFrame;
            
            for (var i = 0; i < amountToSpawn; i++)
            {
                var newTombstone = ecb.Instantiate(prefabOptions[graveyard.GetRandomInt(0, prefabOptions.Length)]);
                var newTombstoneTransform = graveyard.GetRandomTombstoneTransform();
                ecb.SetComponent(newTombstone, newTombstoneTransform);
                
                ecb.AddComponent(newTombstone, new TombstoneRotationSpeedComponent{Value = graveyard.GetRandomRotationSpeed()});
                ecb.AddComponent(newTombstone, new TombstoneNameComponent { Value = graveyard.NextName});
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}