using System;
using Component;
using Player.Entity;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public IPlayerModel PlayerModel { get; private set; }
        private IPlayerPresenter _playerPresenter;

        public event Action OnTap = delegate { };
        public event Action OnDoubleTap = delegate { };
        public event Action<RotateType> OnRotate = delegate {};
        public event Action OnRun;

        private float _lastTapTime;
        private float _doubleTapThreshold = 0.3f;
        
        public void Init(IPlayerPresenter playerPresenter, IPlayerModel model)
        {
            _playerPresenter = playerPresenter;
            PlayerModel = model;
        }

        public void Rotate(RotateType rotateType)
        {
            OnRotate?.Invoke(rotateType);
        }
        
        public void TakeDamage(int damage)
        {
            _playerPresenter.TakeDamage(damage);
        }

        private void Update()
        {
            OnRun?.Invoke();
            if (!PlayerModel.IsAlive.Value)
            {
                return;
            }
            
            if (IsMobilePlayer())
            {
                if (Input.touchCount > 0)
                {
                    var touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        DetectTap();
                    }
                }
            } else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    DetectTap();
                }
            }
        }

        private void DetectTap()
        {
            OnTap?.Invoke();
            CheckOnDoubleTap();
        }

        private void CheckOnDoubleTap()
        {
            if (Time.time - _lastTapTime <= _doubleTapThreshold)
            {
                _lastTapTime = 0;
                OnDoubleTap?.Invoke();
            } else
            {
                _lastTapTime = Time.time;
            }
        }

        private bool IsMobilePlayer()
        {
            return Application.platform == RuntimePlatform.Android &&
                   Application.platform == RuntimePlatform.IPhonePlayer;
        }
        
        public GameObject GetOwnGameObject()
        {
            return gameObject;
        }
    }
}
