using UnityEngine;

namespace Models
{
    public class UserStorageModel 
    {
        [SerializeField]
        private UserModel[] _users;
        [SerializeField]
        private int _activeUser;

        public UserStorageModel()
        {
            
        }
    }
}