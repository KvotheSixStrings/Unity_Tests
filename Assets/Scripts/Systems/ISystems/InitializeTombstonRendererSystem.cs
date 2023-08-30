using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace TMG.Zombies
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(SpawnTombstoneSystem))]
    public partial struct InitializeTombstoneRendererSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.Enabled = false;
            state.RequireForUpdate<GraveyardPropertiesComponent>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            var graveyard = SystemAPI.GetAspect<GraveyardAspect>(SystemAPI.GetSingletonEntity<GraveyardPropertiesComponent>());

            foreach (var tombstoneRenderer in SystemAPI.Query<RefRW<TombstoneRendererComponent>>())
            {
                ecb.AddComponent(tombstoneRenderer.ValueRW.Value,
                    new TombstoneOffsetComponent { Value = graveyard.GetRandomOffset() });
            }

            ecb.Playback(state.EntityManager);
        }
    }
}