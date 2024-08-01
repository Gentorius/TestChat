using System.IO;
using System.Linq;
using Interface;
using Models;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Utility;

namespace Controllers
{
    public class ChatDataHandler : IService
    {
        [SerializeField]
        public ChatHistory ChatHistory;
        [ShowInInspector]
        private string _jsonFilePath;
        
        private IDataHandler _dataHandler;
        private const string _filePathEnding = "ChatHistoryData.json";

        public void Initialize()
        {
            //_dataHandler = ServiceLocator.FindService<IDataHandler>(new DataHandler());
        }

        public void SaveHistory()
        {
            SaveToJson();
            SetNewMessageIndex();
        }
        
        public ChatHistory LoadHistory()
        {
            ChatHistory = _dataHandler.LoadData<ChatHistory>(_filePathEnding);
            SetNewMessageIndex();
            return ChatHistory;
        }
        
        public void SaveToJson()
        {
            if (ChatHistory.IsUnityNull())
            {
                Debug.LogError($"ChatConfig cannot be saved because {nameof(ChatHistory)} equals null");
                return;
            }

            if (ChatHistory.LastChangeWasByUser)
            {
                ChatHistory.SerializedMessages = ChatHistory.Messages.ToArray();
            }
            else
            {
                ChatHistory.Messages = ChatHistory.SerializedMessages.ToList();
            }
            
            _dataHandler.SaveData(ChatHistory, _filePathEnding);
        }

        private void SetNewMessageIndex()
        {
            var lastMessageIndex = ChatHistory.Messages[^1].MessageIndex;
            ChatHistory.IndexOfNewMessage = lastMessageIndex + 1;
        }
        
        public void AddMessageFromJson(string message)
        {
            var messageModel = JsonUtility.FromJson<MessageModel>(message);
            
            ChatHistory.Messages.Add(messageModel);
            SaveToJson();
        }
    }
}