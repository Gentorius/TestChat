using System;
using Attributes;
using Interface;
using Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    [Serializable]
    public class ChatDataHandler : IChatDataHandler
    {
        [SerializeField] [Inject]
        private ChatHistory _chatHistory;
        
        [Inject]
        private IDataHandler _dataHandler;
        [Inject]
        private IChatManager _chatManager;
        
        [ShowInInspector]
        private const string _filePathEnding = "ChatHistoryData.json";
        
        public ChatDataHandler()
        {
            SubscribeToChatManager();
        }
        
        public ChatHistory LoadHistory()
        {
            _chatHistory = _dataHandler.LoadData<ChatHistory>(_filePathEnding);
            return _chatHistory;
        }
        
        public void SaveChatHistory()
        {
            if (_chatHistory == null)
            {
                Debug.LogError($"{nameof(_chatHistory)} cannot be saved as it is null");
                return;
            }
            
            _dataHandler.SaveData(_chatHistory, _filePathEnding);
        }
        
        private void SubscribeToChatManager()
        {
            _chatManager.OnChatHistoryChanged += SaveChatHistory;
        }
    }
}