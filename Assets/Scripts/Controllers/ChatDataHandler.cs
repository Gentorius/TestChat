using System;
using System.Collections;
using Attributes;
using Interface;
using Models;
using Sirenix.OdinInspector;
using UnityEngine;
using Utility;

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
            CoroutineRunner.Instance.StartCoroutine(WaitForChatManager());
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
        
        private IEnumerator WaitForChatManager()
        {
            while (_chatManager == null)
            {
                yield return null;
            }
            
            SubscribeToChatManager();
        }
        
        private void SubscribeToChatManager()
        {
            _chatManager.OnChatHistoryChanged += SaveChatHistory;
        }
    }
}