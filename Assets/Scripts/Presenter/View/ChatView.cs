using System;
using Models;
using Presenter.View.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.View
{
    public class ChatView : BasicView
    {
        [SerializeField]
        private ScrollRect _chatScrollRect;
        [SerializeField]
        private ScrollRect _usersScrollRect;
        [SerializeField]
        private InputField _messageInputField;
        [SerializeField]
        private Button _sendButton;
        
        public event Action<string> OnSendMessage; 

        private void OnEnable()
        {
            _sendButton.onClick.AddListener(SendMessage);
        }

        private void SendMessage()
        {
            OnSendMessage?.Invoke(_messageInputField.text);
        }

        private void OnDisable()
        {
            _sendButton.onClick.RemoveListener(SendMessage);
        }
        
        public void LoadChat(ChatHistoryModel chatHistoryModel)
        {
            
        }
        
        public void UpdateChatView(MessageModel message)
        {
            
        }
    }
}