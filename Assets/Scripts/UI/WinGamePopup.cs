using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game;
using Road;
using UnityEngine;

namespace UI
{
    public class WinGamePopup : BaseScreen
    {
        [SerializeField]
        private ListOfCompletedTilesUI _listOfCompletedTiles;

        private void Awake()
        {
            Init();
        }

        private async void Init()
        {
            await UniTask.WaitUntil(() => GameStateManager.Instance != null);
            
            GameStateManager.Instance.OnWinGame += ShowWinGamePopup;
        }

        private void ShowWinGamePopup(Dictionary<TileType, List<SimpleTile>> completedTiles)
        {
            Show();
            _listOfCompletedTiles.Init(completedTiles);
        }

        private void OnDestroy()
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.OnWinGame -= ShowWinGamePopup;    
            }
        }
    }
}
