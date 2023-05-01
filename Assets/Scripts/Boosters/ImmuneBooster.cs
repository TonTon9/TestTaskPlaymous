using Player.Entity;

namespace Boosters
{
    public class ImmuneBooster : Booster
    {
        public ImmuneBooster(IPlayerView view, BoostType boostType, float duration) : base(view, boostType, duration)
        {
        }
        
        public override void Activate()
        {
            PlayerView.PlayerModel.IsImmune.Value = true;
        }

        public override void Deactivate()
        {
            PlayerView.PlayerModel.IsImmune.Value = false;
        }
    }
}
