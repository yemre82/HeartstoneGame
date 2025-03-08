using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Core
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;

        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("CoroutineRunner");
                    _instance = go.AddComponent<CoroutineRunner>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        public void StartRoutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        public void StopRoutine(IEnumerator routine)
        {
            StopCoroutine(routine);
        }
    }
}
