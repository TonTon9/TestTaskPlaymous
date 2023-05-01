using UnityEngine;

namespace Boosters
{
    [CreateAssetMenu(fileName = "ImmuneBoosterSetting", menuName = "Boosters/Immune")]
    public class ImmuneBoosterSetting : ScriptableObject
    {
        [field: SerializeField]
        public float Duration { get; private set; }
    }
}
