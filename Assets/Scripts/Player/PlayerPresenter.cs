using System;
using Boosters;
using Component;
using Cysharp.Threading.Tasks;
using Game;
using Player.Animations;
using Player.Movement;

namespace Player.Entity
{
    public class PlayerPresenter : IPlayerPresenter
    {
        private const float SPEED_INCREASE_SPEED = 0.06f;
        
        public event Action<RotateType> OnRotate = delegate {};
        
        private IPlayerView _playerView;
        private IPlayerModel _playerModel;
        private IMove _move;
        private IPlayerBoosters _playerBoosters;
        private IPlayerAnimation _playerAnimation;

        private PlayerStatsDto _playerStats;

        public async void Init(IPlayerView view, IPlayerModel model)
        {
            _playerView = view;
            _playerModel = model;
            _playerBoosters = _playerView.GetOwnGameObject().GetComponent<IPlayerBoosters>();
            _playerBoosters.InitBoosters(_playerView);
            
            _move = new PlayerMovement(_playerView.GetOwnGameObject(), _playerModel.Speed);
            _playerAnimation = new PlayerAnimation();
            _playerAnimation.Init(_playerView.GetOwnGameObject(), view);
            
            _playerView.OnTap += _move.Jump;
            _playerView.OnRun += _move.Move;
            
            await UniTask.WaitUntil(() => GameStateManager.Instance != null);
            GameStateManager.Instance.OnGameСontinued += RespawnPlayer;
        }

        public void ApplyBooster(BoosterType boosterType)
        {
            _playerBoosters.ApplyBooster(boosterType);
        }

        public void Rotate(RotateType rotateType)
        {
            _move.Rotate(rotateType);
            OnRotate?.Invoke(rotateType);
        }

        private void RespawnPlayer()
        {
            _playerModel.SetHealthToMax();
            SlowlyIncreaseSpeed();
        }

        private async void SlowlyIncreaseSpeed()
        {
            _playerModel.SetSpeed(0);
            while (_playerModel.Speed.Value < _playerModel.MaxSpeed.Value)
            {
                _playerModel.SetSpeed(_playerModel.Speed.Value += SPEED_INCREASE_SPEED);
                await UniTask.WaitForFixedUpdate();
            }
        }
        
        public void TakeDamage(int damage)
        {
            _playerModel.SetHealth(_playerModel.Health.Value - damage);
        }

        ~PlayerPresenter()
        {
            _playerView.OnTap -= _move.Jump;
            _playerView.OnRun -= _move.Move;
        }
    }
}
