using Interface;
using UnityEngine;

namespace Utility
{
    public class ProjectContext : MonoBehaviour, IService
    {
        [SerializeField] 
        public AssetReferenceObject WindowReferenceServicePrefab;
    }
}