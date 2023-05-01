using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Road
{
    public class RoadGenerator : BaseManager<RoadGenerator>, IManager
    {
        private const string TILE_PREFABS_PATH = "RoadTiles/Variants";
        private const string ROAD_SETTINGS_PATH = "Dtos/RoadGeneratorSettings";
        
        public event Action<Dictionary<TileType, List<SimpleTile>>> OnFinishTileReached = delegate {};
        public event Action<SimpleTile> OnTileSpawned = delegate {  };

        public Dictionary<TileType, List<SimpleTile>> CompletedTiles = new();

        private List<SimpleTile> _tilesPrefabs = new ();
        private List<SimpleTile> _spawnedTiles = new ();

        private RoadGeneratorSettings _roadSettings;
        private SimpleTile _lastTile;
        private int _countOfTilesAfterLastCorner;
        private int _completedTilesCount;
        private int _countOfSpawnedTiles;

        public override void Initialize()
        {
            base.Initialize();
            LoadTilesAndSettingsFromResources();
            var road = GetTilePrefabByType(TileType.SimpleRoad);
            for (int i = 0; i < _roadSettings.CountOfSimpleRoadTilesInTheStart; i++)
            {
                SpawnNewTile(road);
            }
            for (int i = 0; i < _roadSettings.StartCountOfTiles; i++)
            {
                SpawnRandomTile();
            }
            IsInitialize = true;
        }
        
        private void SpawnRandomTile()
        {
            if(_lastTile.TileType == TileType.Finish) return;
            
            if (_countOfSpawnedTiles >= _roadSettings.RoadLenght)
            {
                var finish = GetTilePrefabByType(TileType.Finish);
                SpawnNewTile(finish);
                return;
            }
            SpawnNewTile(GetRandomTile());
        }

        //todo: Change instantiate to object pool
        private void SpawnNewTile(SimpleTile tile)
        {
            if (_lastTile == null)
            {
                _lastTile = Instantiate(tile);    
            } else
            {
                _lastTile = Instantiate(tile, _lastTile.NextTileSpawnPoint.position, _lastTile.NextTileSpawnPoint.rotation);   
            }
            _countOfSpawnedTiles++;
            _spawnedTiles.Add(_lastTile);
            OnTileSpawned?.Invoke(_lastTile);

            if (_lastTile.TileType == TileType.Finish)
            {
                _lastTile.OnPlayerExitTile += ReachFinishTile;
            } else
            {
                _lastTile.OnPlayerExitTile += ActionOnPlayerExitTile;
            }
            
            if (_lastTile.TileType == TileType.LeftCorner || _lastTile.TileType == TileType.RightCorner)
            {
                _countOfTilesAfterLastCorner = 0;
            } else
            {
                _countOfTilesAfterLastCorner++;
            }
        }

        private void ActionOnPlayerExitTile(SimpleTile tile)
        {
            _completedTilesCount++;
            if (!CompletedTiles.ContainsKey(tile.TileType))
            {
                CompletedTiles.Add(tile.TileType, new List<SimpleTile>());
            } 
            CompletedTiles[tile.TileType].Add(tile);
            
            if(_completedTilesCount < _roadSettings.CountOfTilesBeforeSpawningNewTiles) return;
            DeleteFirstTile();
            SpawnRandomTile();
        }

        private void DeleteFirstTile()
        {
            var tile = _spawnedTiles[0];
            tile.OnPlayerExitTile -= ActionOnPlayerExitTile;
            _spawnedTiles.Remove(tile);
            Destroy(tile.gameObject);
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
                if (_countOfTilesAfterLastCorner < _roadSettings.MinCountOfTilesBtwCorners)
                {
                    newTile = GetRandomTile();
                }
            }
            return newTile;
        }
        
        private void ReachFinishTile(SimpleTile tile)
        {
            OnFinishTileReached?.Invoke(CompletedTiles);
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
        
        private void LoadTilesAndSettingsFromResources()
        {
            _tilesPrefabs = Resources.LoadAll<SimpleTile>(TILE_PREFABS_PATH).ToList();
            _roadSettings = Resources.Load<RoadGeneratorSettings>(ROAD_SETTINGS_PATH);
        }

        private void OnDestroy()
        {
            foreach (var tile in _spawnedTiles)
            {
                if (tile.TileType == TileType.Finish)
                {
                    tile.OnPlayerExitTile -= ReachFinishTile;
                } else
                {
                    tile.OnPlayerExitTile -= ActionOnPlayerExitTile;
                }
            }
        }
    }
}
