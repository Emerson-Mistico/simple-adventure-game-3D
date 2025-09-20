using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESM_TOOLS.Core.Singleton
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