using UniRx;

namespace UI.DamageEffect
{
    public interface IDamageEffectUI
    {
        void Init(ReactiveProperty<int> health);
    }
}
