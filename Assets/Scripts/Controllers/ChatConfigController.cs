using System.IO;
using System.Linq;
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

            if (_chatHistory.SerializedMessages.Length < _chatHistory.Messages.Count)
                _chatHistory.SerializedMessages = _chatHistory.Messages.ToArray();

            if (_chatHistory.Messages.Count < _chatHistory.SerializedMessages.Length)
                _chatHistory.Messages = _chatHistory.SerializedMessages.ToList();
            
            var chatHistoryData = JsonUtility.ToJson(_chatHistory);
            _jsonFilePath = Application.persistentDataPath + "/ChatHistoryData.json";
            File.WriteAllText(_jsonFilePath, chatHistoryData);
            Debug.Log($"Data saved successfully at: {_jsonFilePath}");

            SetNewMessageIndex();
        }
        
        public void LoadFromJson()
        {
            _jsonFilePath = Application.persistentDataPath + "/ChatHistoryData.json";

            if (!File.Exists(_jsonFilePath))
            {
                Debug.LogError("The file does not exist in the specified path");
                return;
            }
            var storageData = File.ReadAllText(_jsonFilePath);
            _chatHistory = JsonUtility.FromJson<ChatHistoryModel>(storageData);
            _chatHistory.Messages = _chatHistory.SerializedMessages.ToList();
            Debug.Log($"Data loaded successfully from: {_jsonFilePath}");
            
            SetNewMessageIndex();
        }

        private void SetNewMessageIndex()
        {
            var lastMessageIndex = _chatHistory.Messages[^1].MessageIndex;
            _chatHistory.IndexOfNewMessage = lastMessageIndex + 1;
        }
    }
}