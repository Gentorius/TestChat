using System.IO;
using Models;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers
{
    public class UserConfigController : ScriptableObject
    {
        [field: SerializeField] private UserStorageModel UserStorage { get; set; }

        private string jsonFilePath;
        
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
            jsonFilePath = Application.persistentDataPath + "/UserStorageData.json";
            
            System.IO.File.WriteAllText(jsonFilePath, userStorageData);
            Debug.Log("Data save successfully at: " + jsonFilePath);
        }

        public void LoadFromJson()
        {
            if (jsonFilePath.IsUnityNull())
            {
                Debug.LogError("Load path is not set");
                return;
            }

            if (File.Exists(jsonFilePath))
            {
                Debug.LogError("The file does not exist in the specified path");
                return;
            }
            string storageData = System.IO.File.ReadAllText(jsonFilePath);
            UserStorage = JsonUtility.FromJson<UserStorageModel>(storageData);
        }
        
    }
}