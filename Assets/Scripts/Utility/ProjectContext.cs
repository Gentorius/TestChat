using System;
using Interface;
using UnityEngine;

namespace Utility
{
    public class ProjectContext : MonoBehaviour, IService
    {
        [SerializeField] 
        public AssetReferenceObject WindowReferenceServicePrefab;

        private void Awake()
        {
            if (WindowReferenceServicePrefab == null)
                throw new NullReferenceException("WindowReferenceServicePrefab is null in ProjectContext");
            Instantiate(WindowReferenceServicePrefab.gameObject, transform);
        }
    }
}