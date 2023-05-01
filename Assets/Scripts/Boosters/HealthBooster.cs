using Player.Entity;

namespace Boosters
{
    public class HealthBooster : Booster
    {
        public HealthBooster(IPlayerView view, BoostType boostType, float duration) : base(view, boostType, duration)
        {
        }
        
        public override void Activate()
        {
            PlayerView.PlayerModel.SetHealthToMax();
        }

        public override void Deactivate()
        {
        }
    }
}
