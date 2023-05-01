using System;
using UnityEngine;

namespace Player.Entity
{
    public interface IPlayerView
    {
        public event Action OnTap;
        public event Action OnRun;

        IPlayerModel PlayerModel { get; }
        IPlayerPresenter PlayerPresenter { get; }

        public GameObject GetOwnGameObject();
    }
}
