using System.Linq;
using Attributes;
using Controllers;
using Interface;
using Models;
using Presenter.View;
using Presenter.View.Widget;
using UnityEngine;
using Utility;


namespace Presenter
{
    public class ChatPresenter : BasicPresenter<ChatView>
    {
        [Inject]
        private IUserDataHandler _userDataHandler;
        [Inject]
        private IChatDataHandler _chatDataHandler;
        [Inject]
        private ProjectContext _projectContext;
        private DataStream _dataStream;
        private ChatHistory _oldChatHistory;
        private GameObject _messageWidgetPrefab;
        
        protected override void OnShow()
        {
            _messageWidgetPrefab = _projectContext.GetComponentInChildren<AssetReferenceObject>().GetReference<MessageWidget>();
            
            _oldChatHistory = _chatDataHandler.LoadHistory();
            LoadChatView(_oldChatHistory);
            
            View.OnSendMessage += OnSendMessageHandler;
        }
        
        protected override void OnHide()
        {
            View.OnSendMessage -= OnSendMessageHandler;
        }
        
        private void OnSendMessageHandler(string message)
        {
            if(message == string.Empty) return;
            if (message == null) return;
            
            var messageJson = JsonUtility.ToJson(new Message
            {
                MessageText = message,
                SenderId = _userDataHandler.GetActiveUserId(),
                TimeSent = System.DateTime.Now
            });
            _dataStream.SendMessage(messageJson);
        }
        
        private void UpdateChatView(ChatHistory chatHistory)
        {
            var newMessages = chatHistory.Messages.Except(_oldChatHistory.Messages).ToList();
            foreach (var message in newMessages)
            {
                View.AddMessage(message, _messageWidgetPrefab, _userDataHandler.GetUserById(message.SenderId), 
                    message.SenderId == _userDataHandler.GetActiveUserId());
            }
            
            _oldChatHistory = chatHistory;
        }

        private void LoadChatView(ChatHistory chatHistory)
        {
            foreach (var message in chatHistory.Messages)
            {
                View.AddMessage(message, _messageWidgetPrefab, _userDataHandler.GetUserById(message.SenderId), 
                    message.SenderId == _userDataHandler.GetActiveUserId());
            }
            
            _oldChatHistory = chatHistory;
        }
        
    }
}