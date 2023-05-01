using System;
using Component;
using Cysharp.Threading.Tasks;
using Game;
using Player.Entity;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public IPlayerModel PlayerModel { get; private set; }
        public IPlayerPresenter PlayerPresenter { get; private set; }

        public event Action OnTap = delegate { };
        public event Action OnRun;

        public void Init(IPlayerPresenter playerPresenter, IPlayerModel model)
        {
            PlayerPresenter = playerPresenter;
            PlayerModel = model;
        }

        private void Update()
        {
            if(GameStateManager.Instance == null ||
               GameStateManager.Instance.CurrentGameState != GameStates.Game ||
               !PlayerModel.IsAlive.Value) return;

            OnRun?.Invoke();
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

        private async void DetectTap()
        {
            await UniTask.WaitUntil(() => GameStateManager.Instance != null);
            
            if(GameStateManager.Instance.CurrentGameState != GameStates.Game || !PlayerModel.IsAlive.Value) return;
            OnTap?.Invoke();
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
