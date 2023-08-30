using System.Collections;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace TMG.Zombies
{
    public class PlayerAuthorAuthoring : MonoBehaviour
    {
        public float MovementSpeed;
        public float RotationSpeed;
        public float ScaleSpeed;
        public int NumberToSpawn;
        
        private class PlayerAuthorBaker : Baker<PlayerAuthorAuthoring>
        {
            public override void Bake(PlayerAuthorAuthoring authoring)
            {
                var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<PlayerTagComponent>(playerEntity);
                AddComponent(playerEntity, new PlayerDataComponent
                {
                    MovementSpeed = authoring.MovementSpeed,
                    RotationSpeed = authoring.RotationSpeed,
                    ScaleSpeed = authoring.ScaleSpeed
                });

                AddComponent<PlayerSpawnTagInputComponent>(playerEntity);
                SetComponentEnabled<PlayerSpawnTagInputComponent>(playerEntity, false);
                
                AddComponent(playerEntity,new PlayerSpawnInputComponent
                {
                    NumberToSpawn = authoring.NumberToSpawn
                });
            }
        }
    }
}