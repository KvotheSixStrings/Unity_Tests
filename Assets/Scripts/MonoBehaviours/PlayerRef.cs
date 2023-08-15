using UnityEngine;

namespace TMG.Zombies
{
    public class PlayerRef : MonoBehaviour
    {
        public static GameObject Player;
        public static Transform Transform;
        
        private void Awake()
        {
            Player = gameObject;
            Transform = transform;
        }
    }
}