using UnityEngine;

namespace UI
{
    public class BaseScreen : MonoBehaviour
    {
        [SerializeField]
        private GameObject _content;

        protected void Show()
        {
            _content.SetActive(true);
            
        }

        protected void Hide()
        {
            _content.SetActive(false);
        }
    }
}
