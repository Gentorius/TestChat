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
        
        public event Action<string> OnSendMessage; 

        private void OnEnable()
        {
            _sendButton.onClick.AddListener(SendMessage);
            _defaultMessageWidget.gameObject.SetActive(false);
        }

        private void SendMessage()
        {
            OnSendMessage?.Invoke(_messageInputField.text);
        }

        private void OnDisable()
        {
            _sendButton.onClick.RemoveListener(SendMessage);
        }
        
        public void AddMessage(Message message, GameObject messageWidgetPrefab, User user, bool isCurrentUser)
        {
            var messageWidget = Instantiate(messageWidgetPrefab, _chatScrollRect.content);
            var heightIncrease = messageWidget.GetComponent<MessageWidget>().InitializeMessage(message, user, isCurrentUser);
            _chatScrollRect.ScrollToBottom();
            IncreaseChatContentHeight(heightIncrease);
        }
        
        private void IncreaseChatContentHeight(float heightChange)
        {
            _chatContent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, heightChange);
        }
        
    }
}