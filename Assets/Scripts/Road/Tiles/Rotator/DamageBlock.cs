using Component.Base;
using Player.Entity;
using UnityEngine;

namespace Component
{
    public class DamageBlock : BaseOnTriggerAction
    {
        private const int DAMAGE = 1;
        
        protected override void ActionOnTriggerEnter(Collider collider)
        {
            if (!_isActivated &&  collider.TryGetComponent(out IPlayerView view))
            {
                _isActivated = true;
                view.TakeDamage(DAMAGE);
            }
        }
    }
}
