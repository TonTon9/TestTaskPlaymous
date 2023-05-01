using UnityEngine;

namespace Component.Base
{
    public abstract class BaseOnTriggerAction : MonoBehaviour
    {
        protected bool _isActivated;
        
        private void OnTriggerEnter(Collider other)
        {
            ActionOnTriggerEnter(other);
        }

        protected abstract void ActionOnTriggerEnter(Collider collider);
    }
}
