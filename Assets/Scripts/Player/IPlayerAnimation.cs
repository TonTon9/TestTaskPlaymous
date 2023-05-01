using Player.Entity;
using UnityEngine;

namespace Player.Animations
{
    public interface IPlayerAnimation
    {
        void Init(GameObject playerGameObject ,IPlayerView view);
    }
}
