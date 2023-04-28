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
    }
}
