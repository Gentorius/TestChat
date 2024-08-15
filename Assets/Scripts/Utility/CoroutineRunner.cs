using Interface;
using UnityEngine;

namespace Utility
{
    public class CoroutineRunner : MonoBehaviour, IService
    {
        public static CoroutineRunner Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}