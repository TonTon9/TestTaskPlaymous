using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DamageEffect
{
    public class DamageEffectUI : MonoBehaviour, IDamageEffectUI
    {
        [SerializeField]
        private Image _damageEffectImage;
        
        private IDisposable _healthChangeSubscription;

        private bool _isInitialized;

        private Sequence _fadeSequence;

        public void Init(ReactiveProperty<int> health)
        {
            _healthChangeSubscription = health.Subscribe(PlayEffect);
            _isInitialized = true;
        }

        private void PlayEffect(int newHealth)
        {
            if(!_isInitialized ) return;
            _fadeSequence = DOTween.Sequence();
            _fadeSequence.Append(_damageEffectImage.DOFade(1, 0.01f));
            _fadeSequence.Append(_damageEffectImage.DOFade(0, 1F));
        }

        private void OnDestroy()
        {
            if (_fadeSequence != null)
            {
                _fadeSequence.Kill();
            }
            _healthChangeSubscription?.Dispose();
        }
    }
}
