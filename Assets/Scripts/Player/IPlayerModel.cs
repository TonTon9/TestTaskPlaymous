using UniRx;

namespace Player.Entity
{
    public interface IPlayerModel
    {
        ReactiveProperty<int> Health { get; }
        ReactiveProperty<float> Speed { get; }
        ReactiveProperty<bool> IsAlive { get; }
        ReactiveProperty<bool> IsImmune { get; }
        
        ReactiveProperty<int> MaxHealth { get; }
        ReactiveProperty<float> MaxSpeed { get; }

        void SetHealth(int value);

        void SetSpeed(float speed);

        void SetMaxSpeed(float speed);

        void SetHealthToMax();
    }
}
