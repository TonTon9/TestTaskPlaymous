using UnityEngine;
using UnityEngine.UI;

namespace Component.Base
{
    public abstract class BaseButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClickAction);
        }

        protected abstract void OnButtonClickAction();

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClickAction);
        }
    }
}
