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
        public ChatHistoryModel ChatHistory;
        [ShowInInspector]
        private string _jsonFilePath;

        public void SaveToJson()
        {
            if (ChatHistory.IsUnityNull())
            {
                Debug.LogError($"ChatConfig cannot be saved because {nameof(ChatHistory)} equals null");
                return;
            }

            if (ChatHistory.SerializedMessages.Length < ChatHistory.Messages.Count)
                ChatHistory.SerializedMessages = ChatHistory.Messages.ToArray();

            if (ChatHistory.Messages.Count < ChatHistory.SerializedMessages.Length)
                ChatHistory.Messages = ChatHistory.SerializedMessages.ToList();
            
            var chatHistoryData = JsonUtility.ToJson(ChatHistory);
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
            ChatHistory = JsonUtility.FromJson<ChatHistoryModel>(storageData);
            ChatHistory.Messages = ChatHistory.SerializedMessages.ToList();
            Debug.Log($"Data loaded successfully from: {_jsonFilePath}");
            
            SetNewMessageIndex();
        }

        private void SetNewMessageIndex()
        {
            var lastMessageIndex = ChatHistory.Messages[^1].MessageIndex;
            ChatHistory.IndexOfNewMessage = lastMessageIndex + 1;
        }
        
        public void UpdateChatHistory(string serverChatHistoryJSON)
        {
            ChatHistory = JsonUtility.FromJson<ChatHistoryModel>(serverChatHistoryJSON);
            ChatHistory.Messages = ChatHistory.SerializedMessages.ToList();
            SaveToJson();
        }
        
        public void AddMessageFromJson(string message)
        {
            var messageModel = JsonUtility.FromJson<MessageModel>(message);
            
            ChatHistory.Messages.Add(messageModel);
            SaveToJson();
        }
    }
}