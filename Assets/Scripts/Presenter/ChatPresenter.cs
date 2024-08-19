using System.Globalization;
using System.Linq;
using Attributes;
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
        [Inject]
        private IDataSender _dataSender;
        
        private ChatHistory _oldChatHistory;
        private GameObject _messageWidgetPrefab;
        
        protected override void OnShow()
        {
            _messageWidgetPrefab = _projectContext.WindowReferenceServicePrefab.GetReference<MessageWidget>();

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
                TimeSent = System.DateTime.Now.ToString(CultureInfo.CurrentCulture)
            });
            _dataSender.SendMessage(messageJson);
        }
        
        public void UpdateChatView(ChatHistory chatHistory)
        {
            foreach (var message in chatHistory.Messages.Skip(_oldChatHistory.Messages.Count))
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