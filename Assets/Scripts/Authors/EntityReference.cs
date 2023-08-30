using System;
using Unity.Collections;
using Unity.Entities;

namespace TMG.Zombies
{
    [Serializable]
    public struct EntityReference : IBufferElementData
    {
        public static implicit operator Entity(EntityReference e)
        {
            return e.Value;
        }

        public static implicit operator EntityReference(Entity e)
        {
            return new EntityReference { Value = e };
        }
    
        public FixedString128Bytes Name;
        public Entity Value;
    }
}
