using System;
using Interface;
using Models;
using Presenter.View.Scroll;
using Presenter.View.Widget;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
        private BaseMessageWidget _defaultMessageWidget;
        [SerializeField]
        private GameObject _chatContent;
        [SerializeField]
        private Button _editModeButton;
        
        public event Action<string> OnSendMessage;
        public event Action OnClickEditModeButton;

        private void OnEnable()
        {
            _sendButton.onClick.AddListener(SendMessage);
            _defaultMessageWidget.gameObject.SetActive(false);
            _editModeButton.onClick.AddListener(OnClickEditModeButtonHandler);
        }

        private void OnDisable()
        {
            _sendButton.onClick.RemoveListener(SendMessage);
        }

        private void SendMessage()
        {
            OnSendMessage?.Invoke(_messageInputField.text);
        }

        public IMessageWidget AddMessage(Message message, GameObject messageWidgetPrefab, Sprite profileImage,
            int index, bool isEditMode = false)
        {
            var messageWidget = Instantiate(messageWidgetPrefab, _chatScrollRect.content);
            messageWidget.SetActive(true);
            var messageWidgetComponent = messageWidget.GetComponent<IMessageWidget>();
            var heightIncrease =
                messageWidgetComponent.InitializeMessage(message, profileImage, index, isEditMode);
            messageWidgetComponent.OnDeleteMessage += OnDeleteMessageHandler;
            IncreaseChatContentHeight(heightIncrease);
            _chatScrollRect.ScrollToBottom();
            return messageWidgetComponent;
        }

        public void UnsubscribeMessageWidget(IMessageWidget baseMessageWidget)
        {
            baseMessageWidget.OnDeleteMessage -= OnDeleteMessageHandler;
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
        
        private void OnClickEditModeButtonHandler()
        {
            OnClickEditModeButton?.Invoke();
        }
    }
}