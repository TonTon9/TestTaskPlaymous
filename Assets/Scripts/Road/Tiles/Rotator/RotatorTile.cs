using Component.Base;
using Player.Entity;
using UnityEngine;

namespace Component
{
    public class RotatorTile : BaseOnTriggerAction
    {
        [SerializeField]
        private RotateType _rotateType;

        protected override void ActionOnTriggerEnter(Collider collider)
        {
            if (!_isActivated &&  collider.TryGetComponent(out IPlayerView view))
            {
                _isActivated = true;
                view.PlayerPresenter.Rotate(_rotateType);
            }
        }
    }
}
