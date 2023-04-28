using System;
using UnityEngine;

namespace Player.Movement
{
    public interface IPlayerView
    {
        public event Action OnTap;
        public event Action OnDoubleTap;
        public event Action OnRun;

        public GameObject GetOwnGameObject();

        public void Rotate(RotateType rotateType);
    }
}
