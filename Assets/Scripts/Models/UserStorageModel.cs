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

        public void SetActiveUser(UserModel newActiveUser)
        {
            _activeUser = newActiveUser;
        }
    }
}