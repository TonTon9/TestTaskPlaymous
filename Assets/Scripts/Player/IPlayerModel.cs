using UniRx;

namespace Player.Movement
{
    public interface IPlayerModel
    {
        ReactiveProperty<int> Health { get; }
        ReactiveProperty<float> Speed { get; }
        ReactiveProperty<bool> IsAlive { get; }
    }
}
