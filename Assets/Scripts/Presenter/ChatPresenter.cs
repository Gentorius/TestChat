using System.Linq;
using Controllers;
using Models;
using Presenter.View;
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
        
        protected override void OnShow()
        {
            _projectContext = Object.FindAnyObjectByType<ProjectContext>();
            _userConfigController = _projectContext.UserConfigController;
            _chatConfigController = _projectContext.ChatConfigController;
            _dataStream = _projectContext.DataStream;
            
            View.LoadChat(_chatConfigController.ChatHistory, _userConfigController.UserStorage.ActiveUser.ID);
            _oldChatHistoryModel = _chatConfigController.ChatHistory;
            
            View.OnSendMessage += OnSendMessageHandler;
        }
        
        protected override void OnHide()
        {
            View.OnSendMessage -= OnSendMessageHandler;
        }
        
        private void OnSendMessageHandler(string message)
        {
            var messageJson = JsonUtility.ToJson(new MessageModel
            {
                Message = message,
                SenderId = _userConfigController.UserStorage.ActiveUser.ID,
                TimeSent = System.DateTime.Now
            });
            _dataStream.SendData(messageJson);
        }
        
        public void UpdateChatView(ChatHistoryModel chatHistoryModel)
        {
            if (_oldChatHistoryModel == chatHistoryModel)
            {
                return;
            }

            foreach (var message in chatHistoryModel.Messages.Where(x => x.MessageIndex > _oldChatHistoryModel.Messages.Count))
            {
                View.UpdateChatView(message);
            }
            
            _oldChatHistoryModel = chatHistoryModel;
        }
        
    }
}