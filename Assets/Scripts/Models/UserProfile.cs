using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Models
{
    [Serializable]
    public class UserProfile
    {
        [SerializeField]
        private string _nickname;
        [SerializeField]
        private Sprite _profileImage;

        public string Nickname => _nickname;
        public Sprite ProfileImage => _profileImage;
    }
}