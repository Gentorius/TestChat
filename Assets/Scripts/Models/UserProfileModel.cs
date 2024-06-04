using UnityEngine;
using UnityEngine.UIElements;

namespace Models
{
    public class UserProfileModel
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private Sprite _profileImage;
        
        public UserProfileModel()
        {
            
        }
    }
}