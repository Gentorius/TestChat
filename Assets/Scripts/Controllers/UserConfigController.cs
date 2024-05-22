using System.IO;
using Models;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers
{
    public class UserConfigController : ScriptableObject
    {
        [SerializeField]
        public UserStorageModel UserStorage { get; set; }

        private string _jsonFilePath;
        
        public UserConfigController()
        {
            UserStorage = new UserStorageModel();
            
        }

        public void SaveToJson()
        {
            if (UserStorage.IsUnityNull())
            {
                Debug.LogError("UserConfig cannot be saved because UserStorage equals null");
                return;
            }

            string userStorageData = JsonUtility.ToJson(UserStorage);
            _jsonFilePath = Application.persistentDataPath + "/UserStorageData.json";
            
            System.IO.File.WriteAllText(_jsonFilePath, userStorageData);
            Debug.Log("Data save successfully at: " + _jsonFilePath);
        }

        public void LoadFromJson()
        {
            if (_jsonFilePath.IsUnityNull())
            {
                Debug.LogError("Load path is not set");
                return;
            }

            if (File.Exists(_jsonFilePath))
            {
                Debug.LogError("The file does not exist in the specified path");
                return;
            }
            string storageData = System.IO.File.ReadAllText(_jsonFilePath);
            UserStorage = JsonUtility.FromJson<UserStorageModel>(storageData);
        }
        
    }
}