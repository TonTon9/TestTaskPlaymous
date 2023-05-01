using Player.Entity;
using UnityEngine;

namespace Boosters
{
    public abstract class Booster
    {
        [field: SerializeField]
        public float Duration { get; }

        protected IPlayerView PlayerView { get; }
        
        public BoostType BoostType { get; }

        protected Booster(IPlayerView view , BoostType boostType, float duration)
        {
            PlayerView = view;
            BoostType = boostType;
            Duration = duration;
        }

        public abstract void Activate();
        public abstract void Deactivate();
    }
}
