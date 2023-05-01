using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game;
using Road;
using UnityEngine;

namespace UI
{
    public class LoseGamePopup : BaseScreen
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
            
            GameStateManager.Instance.OnLoseGame += ShowLoseGamePopup;
            GameStateManager.Instance.OnGameСontinued += Hide;
        }

        private void ShowLoseGamePopup(Dictionary<TileType, List<SimpleTile>> completedTiles)
        {
            Show();
            _listOfCompletedTiles.Init(completedTiles);
        }

        private void OnDestroy()
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.OnWinGame -= ShowLoseGamePopup;    
                GameStateManager.Instance.OnGameСontinued -= Hide;
            }
        }
    }
}
