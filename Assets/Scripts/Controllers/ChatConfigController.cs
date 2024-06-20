using System.IO;
using Models;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers
{
    public class ChatConfigController
    {
        [SerializeField]
        private ChatHistoryModel _chatHistory;
        [ShowInInspector]
        private string _jsonFilePath;

        public void SaveToJson()
        {
            if (_chatHistory.IsUnityNull())
            {
                Debug.LogError($"ChatConfig cannot be saved because {nameof(_chatHistory)} equals null");
                return;
            }

            var chatHistoryData = JsonUtility.ToJson(_chatHistory);
            _jsonFilePath = Application.persistentDataPath + "/ChatHistoryData.json";
            File.WriteAllText(_jsonFilePath, chatHistoryData);
            Debug.Log($"Data saved successfully at: {_jsonFilePath}");
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
            _chatHistory = JsonUtility.FromJson<ChatHistoryModel>(storageData);
            Debug.Log($"Data loaded successfully from: {_jsonFilePath}");
            
            SetNewMessageIndex();
        }

        public void SetNewMessageIndex()
        {
            var lastMessageIndex = _chatHistory.Messages[^1].messageIndex;
            _chatHistory.IndexOfNewMessage = lastMessageIndex + 1;
        }
    }
}