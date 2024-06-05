using UnityEngine;

namespace Models
{
    public class UserStorageModel 
    {
        [SerializeField]
        public UserModel[] Users;
        [SerializeField]
        public int ActiveUserId;

        private UserModel _activeUser;

        public UserModel SetActiveUser(UserModel newActiveUser)
        {
            _activeUser = newActiveUser;
            return _activeUser;
        }
    }
}