using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Models
{
    [Serializable]
    public class UserModel
    {
        [SerializeField] 
        private int id;
        [SerializeField]
        private UserProfileModel _profile;

        public int ID => id;
        public UserProfileModel Profile => _profile;
    }
}