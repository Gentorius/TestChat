using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class UserStorage 
    {
        [SerializeField]
        internal User[] Users;
        [SerializeField]
        internal int ActiveUserId;
        
        internal User ActiveUser;
        
    }
}