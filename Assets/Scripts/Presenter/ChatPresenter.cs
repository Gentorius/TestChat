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
        private readonly List<MessageWidget> _messageWidgets = new();
        [Inject]
        private IChatDataHandler _chatDataHandler;
        [Inject]
        private IDataSender _dataSender;
        private bool _isEditMode;
        private GameObject _messageWidgetPrefab;

        private ChatHistory _oldChatHistory;
        [Inject]
        private ProjectContext _projectContext;
        [Inject]
        private IUserDataHandler _userDataHandler;

        protected override void OnShow()
        {
            _messageWidgetPrefab = _projectContext.WindowReferenceServicePrefab.GetReference<MessageWidget>();

            _oldChatHistory = _chatDataHandler.LoadHistory();
            LoadChatView(_oldChatHistory);

            View.OnSendMessage += OnSendMessageHandler;
            View.OnExitEditMode += OnExitEditModeHandler;
        }

        private void OnExitEditModeHandler()
        {
            if (!_isEditMode) return;

            _isEditMode = false;

            foreach (var messageWidget in _messageWidgets.ToList()) messageWidget.DisableEditMode();
        }

        private void OnDeleteMessageHandler(int messageIndex, float heightChange)
        {
            View.UnsubscribeMessageWidget(_messageWidgets[messageIndex]);
            _dataSender.DeleteMessage(messageIndex);
            _messageWidgets.RemoveAt(messageIndex);
        }

        protected override void OnHide()
        {
            View.OnSendMessage -= OnSendMessageHandler;
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
            var newWidget = View.AddMessage(message, _messageWidgetPrefab,
                _userDataHandler.GetUserById(message.SenderId),
                message.SenderId == _userDataHandler.GetActiveUserId(), index, _isEditMode);
            newWidget.OnEditModeStart += OnEnableEditModeHandler;
            newWidget.OnDeleteMessage += OnDeleteMessageHandler;
            _messageWidgets.Add(newWidget);
        }

        private void LoadChatView(ChatHistory chatHistory)
        {
            for (var index = 0; index < chatHistory.Messages.Count; index++) AddMessage(chatHistory.Messages, index);

            _oldChatHistory = chatHistory;
        }

        private void OnEnableEditModeHandler()
        {
            if (_isEditMode) return;

            _isEditMode = true;

            foreach (var messageWidget in _messageWidgets.ToList()) messageWidget.EnableEditMode();
        }
    }
}