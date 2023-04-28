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
        }

        public void Update()
        {
            if (!_playerModel.IsAlive.Value)
            {
                return;
            }
            _move.Move();
        }

        ~PlayerPresenter()
        {
            _playerView.OnTap -= _move.Jump;
            _playerView.OnRun -= _move.Move;
        }
    }
}
