using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace TMG.Zombies
{
    public partial struct UpdatePlayerDataSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerDataComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerDataComponent>();
            var playerAspect = SystemAPI.GetAspect<PlayerAspect>(playerEntity);
            
            playerAspect.SetPosition(playerAspect.Position);
            playerAspect.SetRotation(playerAspect.Rotation);
            playerAspect.SetScale(playerAspect.Scale);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}