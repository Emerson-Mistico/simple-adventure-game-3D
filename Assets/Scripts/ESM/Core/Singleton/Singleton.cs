using UnityEngine;

namespace ESM.Core.Singleton
{
    [DefaultExecutionOrder(-10000)]
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Object.FindAnyObjectByType<T>();
                    // _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }

        [Header("Singleton")]
        [SerializeField] private bool _dontDestroyOnLoad = true;

        protected virtual void Awake()
        {           
            // Information: singleton is Root?
            Debug.Log($"{name} isRoot? {transform.parent == null}");

            if (_instance == null)
            {
                _instance = this as T;
                if (_dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }

            }
            else if (_instance != this as T)
            {
                Destroy(gameObject);
            }
        }
    }
}

/*** OLD VERSION
using UnityEngine;

namespace ESM.Core.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        private void Awake()
        {
            Instance = GetComponent<T>();
        }
    }

}
*****/