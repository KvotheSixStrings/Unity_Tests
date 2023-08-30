using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace TMG.Zombies
{
    public class GraveyardAuthor : MonoBehaviour
    {
        public float3 FieldDimensions;
        public int NumberTombstonesToSpawn;
        public GameObject[] TombstonePrefabs;
        public uint RandomSeed;
    }

    public class GraveyardBaker : Baker<GraveyardAuthor>
    {
        public override void Bake(GraveyardAuthor authoring)
        {
            var graveyardEntity = GetEntity(TransformUsageFlags.Dynamic);

            DynamicBuffer<EntityReference> tombstonePrefabs = AddBuffer<EntityReference>(graveyardEntity);
            
            foreach (var go in authoring.TombstonePrefabs)
            {
                var objectAsEntity = GetEntity(go, TransformUsageFlags.Dynamic);
                tombstonePrefabs.Add(objectAsEntity);
            }

            AddComponent(graveyardEntity, new GraveyardPropertiesComponent
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberTombstonesToSpawn = authoring.NumberTombstonesToSpawn
            });
            
            AddComponent(graveyardEntity, new GraveyardRandomComponent
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
        }
    }
}