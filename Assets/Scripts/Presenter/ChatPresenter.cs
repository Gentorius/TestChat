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
        readonly IList<IMessageWidget> _messageWidgets = new List<IMessageWidget>();
        bool _isEditMode;
        ChatHistory _oldChatHistory;
        IList<int> _messagesToDelete = new List<int>(); 
        
        [Inject]
        IChatDataHandler _chatDataHandler;
        [Inject]
        IDataSender _dataSender;
        [Inject]
        ProjectContext _projectContext;
        [Inject]
        IUserDataHandler _userDataHandler;

        protected override void OnShow()
        {
            _oldChatHistory = _chatDataHandler.LoadHistory();
            LoadChatView(_oldChatHistory);

            View.OnSendMessage += OnSendMessageHandler;
            View.OnClickEditModeButton += OnClickEditModeButtonHandler;
        }

        void OnClickEditModeButtonHandler()
        {
            if (!_isEditMode)
            {
                StartEditMode();
                return;
            }
            
            EndEditMode();
        }

        void OnDeleteMessageHandler(int messageIndex, float heightChange)
        {
            View.UnsubscribeMessageWidget(_messageWidgets[messageIndex]);
            _messageWidgets[messageIndex].OnDeleteMessage -= OnDeleteMessageHandler;
            _messagesToDelete.Add(messageIndex);
        }

        void SetNewMessageIndexes()
        {
            _oldChatHistory = _chatDataHandler.LoadHistory();
            for (var index = 0; index < _messageWidgets.Count; index++)
            {
                _messageWidgets[index].SetMessageIndex(index);
            }
        }
        
        void UpdateLastMessageStatuses()
        {
            for (var index = 0; index < _messageWidgets.Count; index++)
            {
                _messageWidgets[index].SetLastMessageStatus(IsLastMessageOfUser(index, _oldChatHistory.Messages));
            }
        }

        protected override void OnHide()
        {
            View.OnSendMessage -= OnSendMessageHandler;
            View.OnClickEditModeButton -= OnClickEditModeButtonHandler;
        }

        void OnSendMessageHandler(string message)
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
            for (var index = 0; index < newMessages.Count; index++) AddMessage(newMessages[index], index, 
                IsLastMessageOfUser(index, chatHistory.Messages));
            
            _oldChatHistory = chatHistory;
            UpdateLastMessageStatuses();
        }

        void AddMessage(Message message, int index, bool isLastMessageOfUser = false)
        {
            var profileImage = _userDataHandler.GetUserById(message.SenderId).Profile.ProfileImage;
            var messageWidgetPrefab =
                _projectContext.WindowReferenceServicePrefab.GetReference<OtherUserMessageWidget>();

            if (message.SenderId == _userDataHandler.GetActiveUserId())
            {
                messageWidgetPrefab = _projectContext.WindowReferenceServicePrefab.GetReference<CurrentUserMessageWidget>();
            }
            
            var newWidget = View.AddMessage(message, messageWidgetPrefab,
                profileImage, index, _isEditMode, isLastMessageOfUser);
            
            newWidget.OnDeleteMessage += OnDeleteMessageHandler;
            _messageWidgets.Add(newWidget);
        }

        void LoadChatView(ChatHistory chatHistory)
        {
            for (var index = 0; index < chatHistory.Messages.Count; index++) AddMessage(chatHistory.Messages[index], index, 
                IsLastMessageOfUser(index, chatHistory.Messages));

            _oldChatHistory = chatHistory;
        }

        void StartEditMode()
        {
            if (_isEditMode) return;

            _isEditMode = true;

            foreach (var messageWidget in _messageWidgets.ToList()) messageWidget.EnableEditMode();
        }

        void EndEditMode()
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
            UpdateLastMessageStatuses();

            foreach (var messageWidget in _messageWidgets.ToList()) messageWidget.DisableEditMode();
        }

        static bool IsLastMessageOfUser(int index, List<Message> messages)
        {
            if (index + 1 >= messages.Count) return true;
            return messages[index + 1].SenderId != messages[index].SenderId;
        }
    }
}