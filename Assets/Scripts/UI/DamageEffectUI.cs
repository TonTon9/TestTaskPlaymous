using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DamageEffect
{
    public class DamageEffectUI : MonoBehaviour, IDamageEffectUI
    {
        private IDisposable _healthChangeSubscription;
        
        [SerializeField]
        private Image _damageEffectImage;

        private bool _isInitialized;

        public void Init(ReactiveProperty<int> health)
        {
            _healthChangeSubscription = health.Subscribe(PlayEffect);
            _isInitialized = true;
        }

        private void PlayEffect(int newHealth)
        {
            if(!_isInitialized) return;
            Sequence fadeSequence = DOTween.Sequence();
            fadeSequence.Append(_damageEffectImage.DOFade(1, 0.01f));
            fadeSequence.Append(_damageEffectImage.DOFade(0, 1F));
        }

        private void OnDestroy()
        {
            _healthChangeSubscription?.Dispose();
        }
    }
}
