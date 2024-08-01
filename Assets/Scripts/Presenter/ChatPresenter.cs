using System.Linq;
using Controllers;
using Models;
using Presenter.View;
using Presenter.View.Widget;
using UnityEngine;
using Utility;


namespace Presenter
{
    public class ChatPresenter : BasicPresenter<ChatView>
    {
        private UserConfigController _userConfigController;
        private ChatDataHandler _chatDataHandler;
        private ProjectContext _projectContext;
        private DataStream _dataStream;
        private ChatHistory _oldChatHistory;
        private GameObject _messageWidgetPrefab;
        
        protected override void OnShow()
        {
            _projectContext = Object.FindAnyObjectByType<ProjectContext>();
            _userConfigController = _projectContext.UserConfigController;
            _chatDataHandler = _projectContext.ChatDataHandler;
            _dataStream = _projectContext.DataStream;
            _messageWidgetPrefab = _projectContext.GetComponentInChildren<AssetReferenceObject>().GetReference<MessageWidget>();
            
            _oldChatHistory = _chatDataHandler.ChatHistory;
            
            LoadChatView(_chatDataHandler.ChatHistory);
            
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
            
            var messageJson = JsonUtility.ToJson(new MessageModel
            {
                Message = message,
                SenderId = _userConfigController.UserStorage.ActiveUser.ID,
                TimeSent = System.DateTime.Now
            });
            _dataStream.SendData(messageJson);
        }
        
        private void UpdateChatView(ChatHistory chatHistory)
        {
            var newMessages = chatHistory.Messages.Except(_oldChatHistory.Messages).ToList();
            foreach (var message in newMessages)
            {
                View.AddMessage(message, _messageWidgetPrefab, _userConfigController.GetUserById(message.SenderId), message.SenderId == _userConfigController.UserStorage.ActiveUser.ID);
            }
            
            _oldChatHistory = chatHistory;
        }

        private void LoadChatView(ChatHistory chatHistory)
        {
            foreach (var message in chatHistory.Messages)
            {
                View.AddMessage(message, _messageWidgetPrefab, _userConfigController.GetUserById(message.SenderId), message.SenderId == _userConfigController.UserStorage.ActiveUser.ID);
            }
            
            _oldChatHistory = chatHistory;
        }
        
    }
}