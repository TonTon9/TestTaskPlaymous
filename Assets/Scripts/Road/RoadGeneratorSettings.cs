using UnityEngine;

namespace Road
{
    [CreateAssetMenu(fileName = "RoadGeneratorSettings", menuName = "Road/Settings")]
    public class RoadGeneratorSettings : ScriptableObject
    {
        [field: SerializeField]
        public int MinCountOfTilesBtwCorners { get; private set; }
        
        [field: SerializeField]
        public int StartCountOfTiles { get; private set; }
        
        [field: SerializeField]
        public int CountOfSimpleRoadTilesInTheStart { get; private set; }
        
        [field: SerializeField]
        public int CountOfTilesBeforeSpawningNewTiles { get; private set; }
        
        [field: SerializeField]
        public int RoadLenght { get; private set; }
    }
}
