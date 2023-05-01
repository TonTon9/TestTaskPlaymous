using Game;
using Player;
using Player.Camera;
using Player.Entity;
using Player.Movement;
using UnityEngine;

namespace Creator
{
    public class PlayerManager : BaseManager<PlayerManager>, IManager
    {
        private const string PLAYER_CHARACTER_PREFAB_PATH = "PlayerCharacter/PlayerCharacter";
        private const string PLAYER_STATS_DTO_PATH = "Dtos/PlayerStats";
        private const string PLAYER_CAMERA_HOLDER_PATH = "PlayerCharacter/PlayerCameraHolder";
        
        public IPlayerView PlayerView { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            Instance = this;
            SpawnPlayerAndInit();
        }

        private void SpawnPlayerAndInit()
        {
            var playerCharacterPrefab = Resources.Load<PlayerView>(PLAYER_CHARACTER_PREFAB_PATH);
            var playerStatsDto = Resources.Load<PlayerStatsDto>(PLAYER_STATS_DTO_PATH);

            var view = Instantiate(playerCharacterPrefab);
            var playerPresenter = new PlayerPresenter();
            IPlayerModel playerModel = new PlayerModel(playerStatsDto.Health, playerStatsDto.Speed);
            
            view.Init(playerPresenter, playerModel);
            playerPresenter.Init(view, playerModel);
            
            SpawnPlayerCamera(view);

            PlayerView = view;
            IsInitialize = true;
        }

        private void SpawnPlayerCamera(IPlayerView view)
        {
            var playerCameraHolder = Resources.Load<PlayerCameraHolder>(PLAYER_CAMERA_HOLDER_PATH);
            var cameraHolder = Instantiate(playerCameraHolder);
            cameraHolder.Init(view);
        }
    }
}
