using System;
using Models;
using Presenter.View.Scroll;
using Presenter.View.Widget;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.View
{
    public class ChatView : BasicView
    {
        [SerializeField]
        private ChatScrollRect _chatScrollRect;
        [SerializeField]
        private TMP_InputField _messageInputField;
        [SerializeField]
        private Button _sendButton;
        [SerializeField]
        private MessageWidget _defaultMessageWidget;
        [SerializeField]
        private GameObject _chatContent;
        [SerializeField]
        private Button _exitEditModeButton;
        
        public event Action<string> OnSendMessage;
        public event Action OnExitEditMode;

        private void OnEnable()
        {
            _sendButton.onClick.AddListener(SendMessage);
            _defaultMessageWidget.gameObject.SetActive(false);
            _exitEditModeButton.gameObject.SetActive(false);
            _exitEditModeButton.onClick.AddListener(OnExitEditModeHandler);
        }

        private void OnDisable()
        {
            _sendButton.onClick.RemoveListener(SendMessage);
        }

        private void SendMessage()
        {
            OnSendMessage?.Invoke(_messageInputField.text);
        }

        public MessageWidget AddMessage(Message message, GameObject messageWidgetPrefab, User user, bool isCurrentUser,
            int index, bool isEditMode = false)
        {
            var messageWidget = Instantiate(messageWidgetPrefab, _chatScrollRect.content);
            messageWidget.SetActive(true);
            var messageWidgetComponent = messageWidget.GetComponent<MessageWidget>();
            var heightIncrease =
                messageWidgetComponent.InitializeMessage(message, user, isCurrentUser, index, isEditMode);
            messageWidgetComponent.OnEditModeStart += () => _exitEditModeButton.gameObject.SetActive(true);
            messageWidgetComponent.OnDeleteMessage += OnDeleteMessageHandler;
            IncreaseChatContentHeight(heightIncrease);
            _chatScrollRect.ScrollToBottom();
            return messageWidgetComponent;
        }

        public void UnsubscribeMessageWidget(MessageWidget messageWidget)
        {
            messageWidget.OnDeleteMessage -= OnDeleteMessageHandler;
        }

        private void IncreaseChatContentHeight(float heightChange)
        {
            _chatContent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, heightChange);
        }

        private void DecreaseChatContentHeight(float heightChange)
        {
            _chatContent.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, heightChange);
        }

        private void OnDeleteMessageHandler(int messageIndex, float heightDecrease)
        {
            DecreaseChatContentHeight(heightDecrease);
        }
        
        private void OnExitEditModeHandler()
        {
            OnExitEditMode?.Invoke();
            _exitEditModeButton.gameObject.SetActive(false);
        }
    }
}