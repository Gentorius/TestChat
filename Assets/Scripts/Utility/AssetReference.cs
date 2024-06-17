using System;
using Models;
using UnityEngine;

namespace Utility
{
    public class AssetReference : MonoBehaviour
    {
        [SerializeField] 
        private AssetReferenceModel[] _items;
        
        public AssetReferenceModel GetReference<T>() where T : MonoBehaviour
        {
            return GetReference(typeof(T));
        }

        private AssetReferenceModel GetReference(Type type)
        {
            Component component;
            var index = Array.FindIndex(_items, t => t.ReferenceAsset.TryGetComponent(type, out component));
            return index != -1 ? _items[index] : null;
        }
    }
}