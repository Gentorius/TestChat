using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Models
{
    [Serializable]
    public class User
    {
        [SerializeField] 
        private int _id;
        [SerializeField]
        private UserProfile _profile;

        public int ID => _id;
        public UserProfile Profile => _profile;
    }
}