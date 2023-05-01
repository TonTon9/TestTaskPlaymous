using Player.Entity;

namespace Boosters
{
    public class SpeedBooster : Booster
    {
        private float _moveSpeedMultiplier;
        public SpeedBooster(IPlayerView view, BoostType boostType, float duration, float moveSpeedMultiplier) : base(view, boostType, duration)
        {
            _moveSpeedMultiplier = moveSpeedMultiplier;
        }
        
        private float _maxSpeed;
        public override void Activate()
        {
            _maxSpeed = PlayerView.PlayerModel.MaxSpeed.Value;
            
            PlayerView.PlayerModel.SetMaxSpeed(PlayerView.PlayerModel.MaxSpeed.Value * _moveSpeedMultiplier);
        }

        public override void Deactivate()
        {
            PlayerView.PlayerModel.SetMaxSpeed(_maxSpeed);
        }
    }
}
