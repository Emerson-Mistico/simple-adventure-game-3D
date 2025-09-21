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