using System.Collections;
using Unity.Entities;
using UnityEngine;

namespace TMG.Zombies
{
    public class PlayerAuthorAuthoring : MonoBehaviour
    {
        public float MovementSpeed;
        public float RotationSpeed;
        public float ScaleSpeed;
        
        public GraveyardAuthor Graveyard;
        
        private class PlayerAuthorBaker : Baker<PlayerAuthorAuthoring>
        {
            public override void Bake(PlayerAuthorAuthoring authoring)
            {
                var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
                var graveyard = authoring.Graveyard;
                
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
                    ObjectToSpawn = GetEntity(graveyard.TombstonePrefab, TransformUsageFlags.Dynamic), 
                    NumberToSpawn = graveyard.NumberTombstonesToSpawn
                });
            }
        }
    }
}