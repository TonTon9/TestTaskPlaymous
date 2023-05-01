using System.Collections.Generic;
using Boosters;
using Road;
using UnityEngine;

namespace Boosters
{
    [CreateAssetMenu(fileName = "BoostersGeneratorSettings", menuName = "BoosterGenerator/Settings")]
    public class BoostersGeneratorSettings : ScriptableObject
    {
        [field: SerializeField]
        public int ChanceToGenerateBoosterOnTile { get; private set; }
        
        [field: SerializeField]
        public List<CustomDictionary<BoosterType, int>> BoostersAndChancesToDrop { get; private set; }
    }
}
