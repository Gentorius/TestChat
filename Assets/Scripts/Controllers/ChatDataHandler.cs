using System;
using System.Collections;
using Attributes;
using Interface;
using Models;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Controllers
{
    [Serializable]
    public class ChatDataHandler : IChatDataHandler
    {
        [SerializeField]
        protected ChatHistory ChatHistory;
        
        [Inject]
        protected IDataHandler DataHandler;
        
        [ShowInInspector]
        private const string _filePathEnding = "ChatHistoryData.json";
        
        public ChatHistory LoadHistory()
        {
            ChatHistory = DataHandler.LoadData<ChatHistory>(_filePathEnding);
            return DataHandler.LoadData<ChatHistory>(_filePathEnding);
        }

        public void SaveChatHistory()
        {
            if (ChatHistory == null)
            {
                Debug.LogError($"{nameof(ChatHistory)} cannot be saved as it is null");
                return;
            }
            
            DataHandler.SaveData(ChatHistory, _filePathEnding);
        }
        
        public void ClearChatHistory()
        {
            ChatHistory.Messages.Clear();
            SaveChatHistory();
        }
    }
}