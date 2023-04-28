using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Road
{
    public class RoadGenerator : MonoBehaviour, IRoadGenerator
    {
        private const int START_COUNT_OF_TILES = 20;
        private const int COINT_OF_SIMPLE_ROAD_TILES_IN_THE_START = 4;
        
        private List<SimpleTile> _tilesPrefabs = new ();
        private List<GameObject> _spawnedTiles = new ();

        private SimpleTile _lastTile;
        private int _countOfTilesAfterLastCorner;
        public bool IsInitialize { get; private set; }

        public void GenerateRoad()
        {
            LoadTilesFromResources();
            var road = GetTilePrefabByType(TileType.SimpleRoad);
            for (int i = 0; i < COINT_OF_SIMPLE_ROAD_TILES_IN_THE_START; i++)
            {
                SpawnNewTile(road);
            }
            for (int i = 0; i < START_COUNT_OF_TILES; i++)
            {
                SpawnNewTile(GetRandomTile());
            }
            IsInitialize = true;
        }
        
        private void LoadTilesFromResources()
        {
            _tilesPrefabs = Resources.LoadAll<SimpleTile>("RoadTiles/Variants").ToList();
        }

        private void SpawnNewTile(SimpleTile tile)
        {
            if (_lastTile == null)
            {
                _lastTile = Instantiate(tile);    
            } else
            {
                _lastTile = Instantiate(tile, _lastTile.NextTileSpawnPoint.position, _lastTile.NextTileSpawnPoint.rotation);   
            }
            
            _spawnedTiles.Add(_lastTile.gameObject);
            if (_lastTile.TileType == TileType.LeftCorner || _lastTile.TileType == TileType.RightCorner)
            {
                _countOfTilesAfterLastCorner = 0;
            } else
            {
                _countOfTilesAfterLastCorner++;
            }
            
        }
        
        private SimpleTile GetRandomTile()
        {
            var tileType = _lastTile.nextTiles[Random.Range(0, _lastTile.nextTiles.Count)];
            SimpleTile newTile = GetTilePrefabByType(tileType);
            
            if (newTile.TileType == _lastTile.TileType)
            {
                newTile = GetRandomTile();
            }

            if (newTile.TileType == TileType.LeftCorner || newTile.TileType == TileType.RightCorner)
            {
                if (_countOfTilesAfterLastCorner < 10)
                {
                    newTile = GetRandomTile();
                }
            }
            return newTile;
        }

        private SimpleTile GetTilePrefabByType(TileType tileType)
        {
            var tile = _tilesPrefabs.FirstOrDefault(t => t.TileType.Equals(tileType));
            if (tile != null)
            {
                return tile;
            } 
            throw new Exception("Have no tile prefab with this type");
        }
    }

}
