using Player.Entity;

namespace Player.Movement
{
    public class PlayerPresenter : IPlayerPresenter
    {
        private IPlayerView _playerView;
        private IPlayerModel _playerModel;
        private IMove _move;

        public void Init(IPlayerView view, IPlayerModel model)
        {
            _playerView = view;
            _playerModel = model;
            _move = new PlayerMovement(_playerView.GetOwnGameObject(), _playerModel.Speed);
            _playerView.OnTap += _move.Jump;
            _playerView.OnRun += _move.Move;
            _playerView.OnRotate += _move.Rotate;
        }

        ~PlayerPresenter()
        {
            _playerView.OnTap -= _move.Jump;
            _playerView.OnRun -= _move.Move;
            _playerView.OnRotate -= _move.Rotate;
        }

        public void TakeDamage(int damage)
        {
            _playerModel.SetHealth(_playerModel.Health.Value - damage);
        }
    }
}
