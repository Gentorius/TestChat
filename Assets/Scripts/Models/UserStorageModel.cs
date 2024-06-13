using UnityEngine;

namespace Models
{
    public class UserStorageModel 
    {
        [SerializeField]
        public UserModel[] Users;
        [SerializeField]
        public int ActiveUserId;

        [HideInInspector]
        public UserModel ActiveUser;
        
    }
}