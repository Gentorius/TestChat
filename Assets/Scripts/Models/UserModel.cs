using UnityEngine;
using UnityEngine.UIElements;

namespace Models
{
    public class UserModel
    {
        [SerializeField]
        private int _id;
        [SerializeField]
        private UserProfileModel _profile;
        
        public UserModel()
        {
            
        }
    }
}