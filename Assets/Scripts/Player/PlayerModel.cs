using Player.Entity;
using UniRx;

namespace Player.Movement
{
    public class PlayerModel : IPlayerModel
    {
        public ReactiveProperty<int> Health { get; }
        public ReactiveProperty<float> Speed { get; }
        public ReactiveProperty<bool> IsAlive { get; }

        public PlayerModel(int health, float speed)
        {
            Health = new ReactiveProperty<int>(health);
            Speed = new ReactiveProperty<float>(speed);
            IsAlive = new ReactiveProperty<bool>(true);
        }

        public void SetHealth(int newValue)
        {
            if (newValue < 0)
            {
                Health.Value = 0;
            } else
            {
                Health.Value = newValue;
            }
        }

        public void SetSpeed(int newValue)
        {
            if (newValue < 0)
            {
                Speed.Value = 0;
            }
        }
    }
}
