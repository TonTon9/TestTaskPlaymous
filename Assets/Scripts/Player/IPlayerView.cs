using System;
using Component;
using UnityEngine;

namespace Player.Entity
{
    public interface IPlayerView : IDamagable
    {
        public event Action OnTap;
        public event Action OnDoubleTap;
        public event Action OnRun;

        IPlayerModel PlayerModel { get; }

        public event Action<RotateType> OnRotate;

        public GameObject GetOwnGameObject();

        public void Rotate(RotateType rotateType);
    }

}
