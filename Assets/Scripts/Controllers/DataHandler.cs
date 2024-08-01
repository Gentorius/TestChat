using Interface;
using UnityEngine;

namespace Controllers
{
    public class DataHandler : IDataHandler
    {
        public void SaveData<T>(T data, string filePathEnding)
        {
            var jsonData = JsonUtility.ToJson(data);
            var filePath = $"{Application.persistentDataPath}/{filePathEnding}";
            System.IO.File.WriteAllText(filePath, jsonData);
            Debug.Log($"Data type of {typeof(T)} saved successfully at: {filePath}");
        }

        public T LoadData<T>(string filePathEnding)
        {
            var filePath = $"{Application.persistentDataPath}/{filePathEnding}";
            if (!System.IO.File.Exists(filePath))
            {
                Debug.LogError("The file does not exist in the specified path");
                return default;
            }
            var storageData = System.IO.File.ReadAllText(filePath);
            var data = JsonUtility.FromJson<T>(storageData);
            Debug.Log($"Data type of {typeof(T)} loaded successfully from: {filePath}");
            return data;
        }
    }
}