using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Road;
using UnityEngine;

namespace UI
{
    public class ListOfCompletedTilesUI : MonoBehaviour
    {
        private const string COMPLETED_TILES_UI_ITEM_PREFAB_PATH = "UI/ComplitedTileItem";
        
        [SerializeField]
        private Transform _parrent;

        [SerializeField]
        private TileType[] _tilesTypesForShow;

        private List<CompletedTileUIItem> _spawnedItems = new ();

        private CompletedTileUIItem completedTileUIItemPrefab;

        private void Awake()
        {
            LoadPrefabFromResources();
        }

        public async void Init(Dictionary<TileType, List<SimpleTile>> completedTiles)
        {
            await UniTask.WaitUntil(() => completedTileUIItemPrefab != null);
            DeleteAllItems();
            SpawnItems(completedTiles);
        }
        
        private void LoadPrefabFromResources()
        {
            completedTileUIItemPrefab = Resources.Load<CompletedTileUIItem>(COMPLETED_TILES_UI_ITEM_PREFAB_PATH);
        }

        private void SpawnItems(Dictionary<TileType, List<SimpleTile>> completedTiles)
        {
            foreach (var value in completedTiles)
            {
                if(!_tilesTypesForShow.Contains(value.Key)) continue;
                
                SpawnItem(value.Value[0], value.Value.Count);
            }
        }

        private void DeleteAllItems()
        {
            foreach (var spawnedItem in _spawnedItems)
            {
                Destroy(spawnedItem.gameObject);
            }
            _spawnedItems.Clear();
        }

        private void SpawnItem(SimpleTile tile, int count)
        {
            var itemInstance = Instantiate(completedTileUIItemPrefab, _parrent);
            _spawnedItems.Add(itemInstance);
            itemInstance.Init(tile, count);
        }
    }
}
