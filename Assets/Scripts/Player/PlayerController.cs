using Road;
using UnityEngine;
namespace Player.Movement
{
    public class PlayerController : MonoBehaviour, IInitialize
    {
        private IPlayerModel _model;
        private IPlayerView _view;
        private IPlayerPresenter _presenter;
        public bool IsInitialize { get; private set; }

        private void Start()
        {
            SpawnPlayerAndInit();
        }

        public void SpawnPlayerAndInit()
        {
            var playerCharacterPrefab = Resources.Load<PlayerView>("PlayerCharacter/PlayerCharacter");
            var playerStarsDto = Resources.Load<PlayerStatsDto>("Dtos/PlayerStats");
            var view = Instantiate(playerCharacterPrefab);
            var playerPresenter = new PlayerPresenter();
            IPlayerModel playerModel = new PlayerModel(playerStarsDto.Health, playerStarsDto.Speed);
            
            view.Init(playerModel);
            playerPresenter.Init(view, playerModel);
            view.Init(playerModel);
            
            _model = playerModel;
            _view = view;
            _presenter = playerPresenter;
            IsInitialize = true;
        }

        private void Update()
        {
            _presenter.Update();
        }
    }
}
