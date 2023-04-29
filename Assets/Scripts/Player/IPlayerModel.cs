using UniRx;

namespace Player.Entity
{
    public interface IPlayerModel
    {
        ReactiveProperty<int> Health { get; }
        ReactiveProperty<float> Speed { get; }
        ReactiveProperty<bool> IsAlive { get; }

        void SetHealth(int value);
    }
}
