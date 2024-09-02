using System;
using System.Collections.Generic;
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
        private readonly IList<IMessageWidget> _messageWidgets = new List<IMessageWidget>();
        private bool _isEditMode;
        private ChatHistory _oldChatHistory;
        private IList<int> _messagesToDelete = new List<int>(); 
        
        [Inject]
        private IChatDataHandler _chatDataHandler;
        [Inject]
        private IDataSender _dataSender;
        [Inject]
        private ProjectContext _projectContext;
        [Inject]
        private IUserDataHandler _userDataHandler;

        protected override void OnShow()
        {
            _oldChatHistory = _chatDataHandler.LoadHistory();
            LoadChatView(_oldChatHistory);

            View.OnSendMessage += OnSendMessageHandler;
            View.OnClickEditModeButton += OnClickEditModeButtonHandler;
        }

        private void OnClickEditModeButtonHandler()
        {
            if (!_isEditMode)
            {
                StartEditMode();
                return;
            }
            
            EndEditMode();
        }

        private void OnDeleteMessageHandler(int messageIndex, float heightChange)
        {
            View.UnsubscribeMessageWidget(_messageWidgets[messageIndex]);
            _messageWidgets[messageIndex].OnDeleteMessage -= OnDeleteMessageHandler;
            _messagesToDelete.Add(messageIndex);
        }
        
        private void SetNewMessageIndexes()
        {
            _oldChatHistory = _chatDataHandler.LoadHistory();
            for (var index = 0; index < _messageWidgets.Count; index++)
            {
                _messageWidgets[index].SetMessageIndex(index);
            }
        }

        protected override void OnHide()
        {
            View.OnSendMessage -= OnSendMessageHandler;
            View.OnClickEditModeButton -= OnClickEditModeButtonHandler;
        }

        private void OnSendMessageHandler(string message)
        {
            if (message == string.Empty) return;
            if (message == null) return;

            var messageJson = JsonUtility.ToJson(new Message
            {
                MessageText = message,
                SenderId = _userDataHandler.GetActiveUserId(),
                TimeSent = DateTime.Now.ToString(CultureInfo.CurrentCulture)
            });
            _dataSender.SendMessage(messageJson);
        }

        public void UpdateChatView(ChatHistory chatHistory)
        {
            var newMessages = chatHistory.Messages.Skip(_oldChatHistory.Messages.Count).ToList();
            for (var index = 0; index < newMessages.Count; index++) AddMessage(newMessages, index);

            _oldChatHistory = chatHistory;
        }

        private void AddMessage(List<Message> newMessages, int index)
        {
            var message = newMessages[index];
            var profileImage = _userDataHandler.GetUserById(message.SenderId).Profile.ProfileImage;
            var messageWidgetPrefab =
                _projectContext.WindowReferenceServicePrefab.GetReference<OtherUserMessageWidget>();

            if (message.SenderId == _userDataHandler.GetActiveUserId())
            {
                messageWidgetPrefab = _projectContext.WindowReferenceServicePrefab.GetReference<CurrentUserMessageWidget>();
            }
            
            var newWidget = View.AddMessage(message, messageWidgetPrefab,
                profileImage, index, _isEditMode);
            
            newWidget.OnDeleteMessage += OnDeleteMessageHandler;
            _messageWidgets.Add(newWidget);
        }

        private void LoadChatView(ChatHistory chatHistory)
        {
            for (var index = 0; index < chatHistory.Messages.Count; index++) AddMessage(chatHistory.Messages, index);

            _oldChatHistory = chatHistory;
        }

        private void StartEditMode()
        {
            if (_isEditMode) return;

            _isEditMode = true;

            foreach (var messageWidget in _messageWidgets.ToList()) messageWidget.EnableEditMode();
        }
        
        private void EndEditMode()
        {
            if (!_isEditMode) return;

            _isEditMode = false;
            
            _messagesToDelete = _messagesToDelete.OrderByDescending(x => x).ToList();
            
            foreach (var messageIndex in _messagesToDelete)
            {
                View.UnsubscribeMessageWidget(_messageWidgets[messageIndex]);
                _messageWidgets[messageIndex].OnDeleteMessage -= OnDeleteMessageHandler;
                _messageWidgets[messageIndex].Destroy();
                _messageWidgets.RemoveAt(messageIndex);
                _dataSender.DeleteMessage(messageIndex);
            }
            
            SetNewMessageIndexes();

            foreach (var messageWidget in _messageWidgets.ToList()) messageWidget.DisableEditMode();
        }
    }
}