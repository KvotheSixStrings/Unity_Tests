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
        public GameObject TombstonePrefab;
        public uint RandomSeed;
    }

    public class GraveyardBaker : Baker<GraveyardAuthor>
    {
        public override void Bake(GraveyardAuthor authoring)
        {
            DependsOn(authoring.TombstonePrefab);
            
            var graveyardEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(graveyardEntity, new GraveyardPropertiesComponent
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberTombstonesToSpawn = authoring.NumberTombstonesToSpawn,
                TombstonePrefab = GetEntity(authoring.TombstonePrefab, TransformUsageFlags.Dynamic)
            });
            
            AddComponent(graveyardEntity, new GraveyardRandomComponent
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });

            
        }
    }
}