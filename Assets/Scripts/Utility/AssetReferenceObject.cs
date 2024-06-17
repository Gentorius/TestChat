using System;
using Models;
using UnityEngine;

namespace Utility
{
    public class AssetReferenceObject : MonoBehaviour
    {
        [SerializeField] 
        private GameObject[] _items;
        
        public GameObject GetReference<T>() where T : MonoBehaviour
        {
            Component component;
            var index = Array.FindIndex(_items, x => x.TryGetComponent(typeof(T), out component));
            return index != -1 ? _items[index] : null;
        }
        
    }
}