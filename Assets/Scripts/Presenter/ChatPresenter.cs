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
        private ChatConfigController _chatConfigController;
        private ProjectContext _projectContext;
        private DataStream _dataStream;
        private ChatHistoryModel _oldChatHistoryModel;
        private GameObject _messageWidgetPrefab;
        
        protected override void OnShow()
        {
            _projectContext = Object.FindAnyObjectByType<ProjectContext>();
            _userConfigController = _projectContext.UserConfigController;
            _chatConfigController = _projectContext.ChatConfigController;
            _dataStream = _projectContext.DataStream;
            _messageWidgetPrefab = _projectContext.GetComponentInChildren<AssetReferenceObject>().GetReference<MessageWidget>();
            
            _oldChatHistoryModel = _chatConfigController.ChatHistory;
            
            LoadChatView(_chatConfigController.ChatHistory);
            
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
        
        private void UpdateChatView(ChatHistoryModel chatHistoryModel)
        {
            var newMessages = chatHistoryModel.Messages.Except(_oldChatHistoryModel.Messages).ToList();
            foreach (var message in newMessages)
            {
                View.AddMessage(message, _messageWidgetPrefab, _userConfigController.GetUserById(message.SenderId), message.SenderId == _userConfigController.UserStorage.ActiveUser.ID);
            }
            
            _oldChatHistoryModel = chatHistoryModel;
        }

        private void LoadChatView(ChatHistoryModel chatHistoryModel)
        {
            foreach (var message in chatHistoryModel.Messages)
            {
                View.AddMessage(message, _messageWidgetPrefab, _userConfigController.GetUserById(message.SenderId), message.SenderId == _userConfigController.UserStorage.ActiveUser.ID);
            }
            
            _oldChatHistoryModel = chatHistoryModel;
        }
        
    }
}