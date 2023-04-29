using Game;
using Player;
using Player.Camera;
using Player.Entity;
using Player.Movement;
using UnityEngine;

namespace Creator
{
    public class PlayerCreator : MonoBehaviour, ICreator
    {
        public static PlayerCreator Instance { get; private set; }

        public bool IsInitialize { get; private set; }

        public IPlayerView PlayerView { get; private set; }

        public void Create()
        {
            Instance = this;
            SpawnPlayerAndInit();
        }

        private void SpawnPlayerAndInit()
        {
            var playerCharacterPrefab = Resources.Load<PlayerView>("PlayerCharacter/PlayerCharacter");
            var playerStarsDto = Resources.Load<PlayerStatsDto>("Dtos/PlayerStats");
            
            var view = Instantiate(playerCharacterPrefab);
            var playerPresenter = new PlayerPresenter();
            IPlayerModel playerModel = new PlayerModel(playerStarsDto.Health, playerStarsDto.Speed);
            
            view.Init(playerPresenter, playerModel);
            playerPresenter.Init(view, playerModel);
            
            SpawnPlayerCamera(view);

            PlayerView = view;
            IsInitialize = true;
        }

        private void SpawnPlayerCamera(IPlayerView view)
        {
            var playerCameraHolder = Resources.Load<PlayerCameraHolder>("PlayerCharacter/PlayerCameraHolder");
            var cameraHolder = Instantiate(playerCameraHolder);
            cameraHolder.Init(view);
        }
    }
}
