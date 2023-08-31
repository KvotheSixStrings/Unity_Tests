using System.Collections.Generic;
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
        public int BatchSize;
        public uint SpawningRandomSeed;
        public uint PositionRandomSeed;
        public uint RotationRandomSeed;
        public uint ScaleRandomSeed;
        public uint RotateByRandomSeed;
        public bool UseLod0;
        public bool UseLod1;
        public bool UseLod2;
        public bool UseLodGroup;
        public List<GameObject> TombstonePrefabs;
    }

    public class GraveyardBaker : Baker<GraveyardAuthor>
    {
        public override void Bake(GraveyardAuthor authoring)
        {
            var graveyardEntity = GetEntity(TransformUsageFlags.Dynamic);

            DynamicBuffer<EntityReference> tombstonePrefabs = AddBuffer<EntityReference>(graveyardEntity);

            int loopStart = 0;
            int prefabSetCount = 4;
            int loopEnd = loopStart + prefabSetCount;
            
            if (authoring.UseLod0)
            {
                for (var i = loopStart; i < loopEnd; i++)
                {
                    var go = authoring.TombstonePrefabs[i];
                    var objectAsEntity = GetEntity(go, TransformUsageFlags.Dynamic);
                    tombstonePrefabs.Add(objectAsEntity);
                }
            }

            if (authoring.UseLod1)
            {
                loopStart = prefabSetCount;
                loopEnd = loopStart + prefabSetCount;
                
                for (var i = loopStart; i < loopEnd; i++)
                {
                    var go = authoring.TombstonePrefabs[i];
                    var objectAsEntity = GetEntity(go, TransformUsageFlags.Dynamic);
                    tombstonePrefabs.Add(objectAsEntity);
                }
            }

            if (authoring.UseLod2)
            {
                loopStart = prefabSetCount * 2;
                loopEnd = loopStart + prefabSetCount;
                
                for (var i = loopStart; i < loopEnd; i++)
                {
                    var go = authoring.TombstonePrefabs[i];
                    var objectAsEntity = GetEntity(go, TransformUsageFlags.Dynamic);
                    tombstonePrefabs.Add(objectAsEntity);
                }

                loopStart += 3;
                loopEnd += prefabSetCount;
            }

            if (authoring.UseLodGroup)
            {
                loopStart = prefabSetCount * 3;
                loopEnd = loopStart + prefabSetCount;
                
                for (var i = loopStart; i < loopEnd; i++)
                {
                    var go = authoring.TombstonePrefabs[i];
                    var objectAsEntity = GetEntity(go, TransformUsageFlags.Dynamic);
                    tombstonePrefabs.Add(objectAsEntity);
                }
            }

            AddComponent(graveyardEntity, new GraveyardRandomComponent
            {
                SpawningRandomSeed = Random.CreateFromIndex(authoring.SpawningRandomSeed),
                PositionRandomSeed = Random.CreateFromIndex(authoring.PositionRandomSeed),
                RotationRandomSeed = Random.CreateFromIndex(authoring.RotationRandomSeed),
                ScaleRandomSeed = Random.CreateFromIndex(authoring.ScaleRandomSeed),
                RotateByRandomSeed = Random.CreateFromIndex(authoring.RotateByRandomSeed)
            });
            
            AddComponent(graveyardEntity, new GraveyardPropertiesComponent
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberTombstonesToSpawn = authoring.NumberTombstonesToSpawn,
                TombstoneBatchSize = authoring.BatchSize
            });
        }
    }
}