using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Models
{
    [Serializable]
    public class UserModel
    {
        [SerializeField] 
        public int ID;
        [SerializeField]
        private UserProfileModel _profile;
    }
}