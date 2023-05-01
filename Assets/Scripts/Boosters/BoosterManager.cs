using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game;
using Road;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Boosters
{
    public class BoosterManager : BaseManager<BoosterManager>, IManager
    {
        private List<BoosterGameObject> _boostersPrefabs = new ();
        private BoostersGeneratorSettings _settings;
        private int _tilesSinceLastBooster;

        public override async void Initialize()
        {
            base.Initialize();
            await UniTask.WaitUntil(() => RoadGenerator.Instance != null);
            LoadTilesFromResources();
            RoadGenerator.Instance.OnTileSpawned += RoadGeneratorOnTileSpawned;
            IsInitialize = true;
        }

        private void RoadGeneratorOnTileSpawned(SimpleTile simpleTile)
        {
            var random = Random.Range(0f, 100f);
            
            if (random < _settings.ChanceToGenerateBoosterOnTile)
            {
                SpawnBooster(simpleTile);
            }
        }

        private void SpawnBooster(SimpleTile simpleTile)
        {
            var boosterPrefab = GetTilePrefabByType(GetRandomBoosterType());
            var boosterInstance = Instantiate(boosterPrefab, simpleTile.BoosterSpawnPoint.position, simpleTile.BoosterSpawnPoint.rotation);
            boosterInstance.Init(simpleTile);
        }

        private BoosterType GetRandomBoosterType()
        {
            var chance = 100f;
            foreach (var booster in _settings.BoostersAndChancesToDrop)
            {
                var random = Random.Range(0f, chance);
                if (booster.Value >= random)
                {
                    return booster.Key;
                }
                chance -= booster.Value;
            }
            throw new Exception("The problem with getting a booster");
        }
        
        private BoosterGameObject GetTilePrefabByType(BoosterType boosterType)
        {
            var booster = _boostersPrefabs.FirstOrDefault(t => t.BoosterType.Equals(boosterType));
            if (booster != null)
            {
                return booster;
            } 
            throw new Exception("Have no booster prefab with this type");
        }
        
        private void LoadTilesFromResources()
        {
            _boostersPrefabs = Resources.LoadAll<BoosterGameObject>("Boosters").ToList();
            _settings = Resources.Load<BoostersGeneratorSettings>("Dtos/BoostersGeneratorSettings");
        }

        private void OnDestroy()
        {
            if (RoadGenerator.Instance != null)
            {
                RoadGenerator.Instance.OnTileSpawned -= RoadGeneratorOnTileSpawned;
            }
        }
    }
}
