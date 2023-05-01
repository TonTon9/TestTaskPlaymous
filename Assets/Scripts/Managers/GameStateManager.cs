using System;
using System.Collections.Generic;
using Creator;
using Cysharp.Threading.Tasks;
using Road;
using UniRx;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameStateManager : BaseManager<GameStateManager>, IManager
    {
        private const string GAME_SCENE_NAME = "GameScene";
        
        public event Action<Dictionary<TileType, List<SimpleTile>>> OnLoseGame;

        public event Action<Dictionary<TileType, List<SimpleTile>>> OnWinGame;
        
        public GameStates CurrentGameState { get; private set; }

        private IDisposable _playerHealthChangeSubscription;

        public event Action OnGameСontinued;

        private ReactiveProperty<int> _playerHeath;

        public override async void Initialize()
        {
            base.Initialize();
            await UniTask.WaitUntil(() => PlayerManager.Instance != null && RoadGenerator.Instance != null);

            CurrentGameState = GameStates.Game;
            _playerHeath = PlayerManager.Instance.PlayerView.PlayerModel.Health;
            _playerHealthChangeSubscription = _playerHeath.Subscribe(PlayerHeathChanged);

            RoadGenerator.Instance.OnFinishTileReached += WinGame;
            IsInitialize = true;
        }

        private void PlayerHeathChanged(int newHealth)
        {
            if (newHealth == 0)
            {
                LoseGame(RoadGenerator.Instance.CompletedTiles);
            }
        }

        public void LoseGame(Dictionary<TileType, List<SimpleTile>> listOfCompletedTiles)
        {
            CurrentGameState = GameStates.Lose;
            OnLoseGame?.Invoke(listOfCompletedTiles);
        }

        public void WinGame(Dictionary<TileType, List<SimpleTile>> listOfCompletedTiles)
        {
            CurrentGameState = GameStates.Win;
            OnWinGame?.Invoke(listOfCompletedTiles);
        }
        
        public void ContinueGame()
        {
            CurrentGameState = GameStates.Game;
            OnGameСontinued?.Invoke();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
        }

        private void OnDestroy()
        {
            _playerHealthChangeSubscription?.Dispose();
            if (RoadGenerator.Instance != null)
            {
                RoadGenerator.Instance.OnFinishTileReached -= WinGame;
            }
        }
    }
}
