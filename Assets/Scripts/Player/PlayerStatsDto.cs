using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Stats")]
    public class PlayerStatsDto : ScriptableObject
    {
        [field: SerializeField]
        public int Health { get; private set; }
        
        [field: SerializeField]
        public float Speed { get; private set; }
    }
}
