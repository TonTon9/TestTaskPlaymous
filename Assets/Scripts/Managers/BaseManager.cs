using UnityEngine;

namespace Game
{
    public class BaseManager<T> : MonoBehaviour where T : BaseManager<T>, IManager
    {
        public static T Instance { get; set; }
        
        public bool IsInitialize { get; protected set; }

        public virtual void Initialize()
        {
            Instance = GetComponent<T>();
        }
    }
}
