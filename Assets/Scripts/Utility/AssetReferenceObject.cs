using UnityEngine;

namespace Utility
{
    public class AssetReferenceObject : MonoBehaviour
    {
        [SerializeField] 
        private GameObject[] _items;
        
        public GameObject GetReference<T>() where T : MonoBehaviour
        {
            
            for (var i = 0; i < _items.Length; i++)
            {
                var x = _items[i];
                if (x.TryGetComponent(typeof(T), out var component))
                    return x;
            }
            return null;
        }
        
    }
}