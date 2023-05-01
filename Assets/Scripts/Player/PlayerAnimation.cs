using Player.Entity;
using UnityEngine;

namespace Player.Animations
{
    public class PlayerAnimation : IPlayerAnimation
    {
        private const string JUMP_TRIGGER_NAME = "IsJump";
        
        private Animator _animator;
        private IPlayerView _view;

        public void Init(GameObject playerGameObject ,IPlayerView view)
        {
            _animator = playerGameObject.GetComponentInChildren<Animator>();
            _view = view;
            _view.OnTap += PlayJumpAnimation;
        }

        private void PlayJumpAnimation()
        {
            _animator.SetTrigger(JUMP_TRIGGER_NAME);
        }
        
        ~PlayerAnimation()
        {
            if (_view != null)
            {
                _view.OnTap -= PlayJumpAnimation;
            }
        }
    }
}
