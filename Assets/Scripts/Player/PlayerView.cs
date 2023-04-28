using System;
using DG.Tweening;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        private IPlayerModel _playerModel;

        public event Action OnTap = delegate { };
        public event Action OnDoubleTap = delegate { };
        public event Action OnRun;

        private float _lastTapTime;
        private float _doubleTapThreshold = 0.3f;
        
        public void Init(IPlayerModel model)
        {
            _playerModel = model;
        }
        
        public void Rotate(RotateType rotateType)
        {
            if (rotateType == RotateType.Left)
            {
                transform.DORotate(new Vector3(0,90f,0), 0.5f);
            } else
            {
                transform.DORotate(new Vector3(0,-90f,0), 0.5f);
            }
        }

        private void Update()
        {
            OnRun?.Invoke();
            if (!_playerModel.IsAlive.Value)
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
