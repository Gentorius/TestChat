using System;
using Attributes;
using Interface;
using Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    [Serializable]
    public class UserDataHandler : IUserDataHandler
    {
        [SerializeField] 
        private UserStorage _userStorage;
        
        [Inject]
        private IDataHandler _dataHandler;

        [ShowInInspector]
        private const string _filePathEnding = "/UserStorageData.json";

        public void SaveUserData()
        {
            if (_userStorage == null)
            {
                Debug.LogError($"User Data cannot be saved because {nameof(_userStorage)} is null");
                return;
            }

            _dataHandler.SaveData(_userStorage, _filePathEnding);
            SetActiveUser();
        }

        public void LoadUserData()
        {
            _userStorage = _dataHandler.LoadData<UserStorage>(_filePathEnding);
            SetActiveUser();
        }
        
        public User GetUserById(int id)
        {
            foreach (var user in _userStorage.Users)
            {
                if (id == user.ID)
                {
                    return user;
                }
            }

            Debug.LogError($"User id {id} could not be found");
            return null;
        }

        public int GetActiveUserId()
        {
            return _userStorage.ActiveUserId;
        }

        private void SetActiveUser()
        {
            if (_userStorage.ActiveUserId < 1)
            {
                Debug.LogError($"Active user id cannot be less than 1");
                return;
            }

            foreach (var user in _userStorage.Users)
            {
                if (_userStorage.ActiveUserId == user.ID)
                {
                    _userStorage.ActiveUser = user;
                    Debug.Log($"Active user has been set");
                    return;
                }
            }
            
            Debug.LogError($"User id {_userStorage.ActiveUserId} could not be found");
        }
        
    }
}