using Player.Entity;

namespace Boosters
{
    public interface IPlayerBoosters
    {
        void ApplyBooster(BoosterType type);

        void InitBoosters(IPlayerView playerView);
    }
}
