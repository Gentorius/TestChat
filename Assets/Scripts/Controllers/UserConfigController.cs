using System.IO;
using Models;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers
{
    public class UserConfigController
    {
        [SerializeField] 
        public UserStorageModel UserStorage;

        [SerializeField]
        private string _jsonFilePath;

        public void SaveToJson()
        {
            if (UserStorage.IsUnityNull())
            {
                Debug.LogError("UserConfig cannot be saved because UserStorage equals null");
                return;
            }

            var userStorageData = JsonUtility.ToJson(UserStorage);
            _jsonFilePath = Application.persistentDataPath + "/UserStorageData.json";
            File.WriteAllText(_jsonFilePath, userStorageData);
            Debug.Log($"Data saved successfully at: {_jsonFilePath}");

            SetActiveUser();
        }

        public void LoadFromJson()
        {
            _jsonFilePath = Application.persistentDataPath + "/UserStorageData.json";

            if (!File.Exists(_jsonFilePath))
            {
                Debug.LogError("The file does not exist in the specified path");
                return;
            }
            var storageData = File.ReadAllText(_jsonFilePath);
            UserStorage = JsonUtility.FromJson<UserStorageModel>(storageData);
            Debug.Log($"Data loaded successfully from: {_jsonFilePath}");

            SetActiveUser();
        }
        
        public UserModel GetUserById(int id)
        {
            foreach (var user in UserStorage.Users)
            {
                if (id == user.ID)
                {
                    return user;
                }
            }

            Debug.LogError($"User id {id} could not be found");
            return null;
        }

        private void SetActiveUser()
        {
            if (UserStorage.ActiveUserId < 1)
            {
                Debug.LogError($"Active user id cannot be less than 1");
                return;
            }

            foreach (var user in UserStorage.Users)
            {
                if (UserStorage.ActiveUserId == user.ID)
                {
                    UserStorage.ActiveUser = user;
                    Debug.Log($"Active user has been set");
                    return;
                }
            }
            
            Debug.LogError($"User id {UserStorage.ActiveUserId} could not be found");
        }
        
    }
}