using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Road
{
    public class SimpleTile : MonoBehaviour
    {
        [field: SerializeField]
        public Transform NextTileSpawnPoint { get; private set; }

        [field: SerializeField]
        public TileType TileType { get; private set; }

        [SerializeField]
        public List<TileType> nextTiles = new();
    }
}
