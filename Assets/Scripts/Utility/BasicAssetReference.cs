using System;
using Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utility
{
    public class BasicAssetReference : SerializedScriptableObject
    {
        [SerializeField] 
        private AssetReferenceModel[] _items;

        [SerializeField] 
        private Type[] types;
        
        public AssetReferenceModel GetReference<T>() where T : MonoBehaviour
        {
            return GetReference(typeof(T));
        }

        public AssetReferenceModel GetReference(Type type)
        {
            int index = Array.FindIndex(types, t => t == type);
            return index != -1 ? _items[index] : null;
        }
    }
}