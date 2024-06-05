using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Models
{
    [Serializable]
    public class UserProfileModel
    {
        [SerializeField]
        private string _nickname;
        [SerializeField]
        private Sprite _profileImage;
    }
}