using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models
{
    [Serializable]
    public class AssetReferenceModel
    {
        public string AssetGuid { get; private set; }
        [SerializeField] 
        public GameObject ReferenceAsset;

        public void LoadGUIDsOfAssets()
        {
            AssetGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(ReferenceAsset));
        }
    }
}