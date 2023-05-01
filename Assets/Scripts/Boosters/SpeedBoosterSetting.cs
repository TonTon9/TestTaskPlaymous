using UnityEngine;

namespace Boosters
{
    [CreateAssetMenu(fileName = "SpeedBoosterSetting", menuName = "Boosters/Speed")]
    public class SpeedBoosterSetting : ScriptableObject
    {
        [field: SerializeField]
        public float Duration { get; private set; }
        
        [field: SerializeField]
        public float SpeedMultiplier { get; private set; }
    }
}
