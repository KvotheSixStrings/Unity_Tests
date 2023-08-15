using Unity.Entities;
using UnityEngine;

namespace TMG.Zombies
{
    public class TombstoneAuthor : MonoBehaviour
    {
        public GameObject Renderer;
    }

    public class TombstoneBaker : Baker<TombstoneAuthor>
    {
        public override void Bake(TombstoneAuthor authoring)
        {
            DependsOn(authoring.Renderer);
            
            var tombstoneEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(tombstoneEntity, new TombstoneRendererComponent
            {
                Value = GetEntity(authoring.Renderer, TransformUsageFlags.Dynamic)
            });
        }
    }
}