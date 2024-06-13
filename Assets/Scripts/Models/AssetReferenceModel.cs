using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class AssetReferenceModel
    {
        [SerializeField]
        protected internal string m_AssetGUID = "";
        public AssetReferenceModel(string guid)
        {
            m_AssetGUID = guid;
        }
    }
}