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
        ChatScrollRect _chatScrollRect;
        [SerializeField]
        TMP_InputField _messageInputField;
        [SerializeField]
        Button _sendButton;
        [SerializeField]
        CurrentUserMessageWidget _defaultMessageWidget;
        [SerializeField]
        GameObject _chatContent;
        [SerializeField]
        Button _editModeButton;
        
        public event Action<string> OnSendMessage;
        public event Action OnClickEditModeButton;

        void OnEnable()
        {
            _sendButton.onClick.AddListener(SendMessage);
            _defaultMessageWidget.gameObject.SetActive(false);
            _editModeButton.onClick.AddListener(OnClickEditModeButtonHandler);
        }

        void OnDisable()
        {
            _sendButton.onClick.RemoveListener(SendMessage);
            _editModeButton.onClick.RemoveListener(OnClickEditModeButtonHandler);
        }

        void SendMessage()
        {
            OnSendMessage?.Invoke(_messageInputField.text);
        }

        public IMessageWidget AddMessage(Message message, GameObject messageWidgetPrefab, Sprite profileImage,
            int index, bool isEditMode = false, bool isLastMessageOfUser = false)
        {
            var messageWidget = Instantiate(messageWidgetPrefab, _chatScrollRect.content);
            messageWidget.SetActive(true);
            var messageWidgetComponent = messageWidget.GetComponent<IMessageWidget>();
            var heightIncrease =
                messageWidgetComponent.InitializeMessage(message, profileImage, index, isEditMode, isLastMessageOfUser);
            messageWidgetComponent.OnDeleteMessage += OnDeleteMessageHandler;
            IncreaseChatContentHeight(heightIncrease);
            _chatScrollRect.ScrollToBottom();
            return messageWidgetComponent;
        }

        public void UnsubscribeMessageWidget(IMessageWidget baseMessageWidget)
        {
            baseMessageWidget.OnDeleteMessage -= OnDeleteMessageHandler;
        }

        void IncreaseChatContentHeight(float heightChange)
        {
            _chatContent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, heightChange);
        }

        void DecreaseChatContentHeight(float heightChange)
        {
            _chatContent.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, heightChange);
        }

        void OnDeleteMessageHandler(int messageIndex, float heightDecrease)
        {
            DecreaseChatContentHeight(heightDecrease);
        }

        void OnClickEditModeButtonHandler()
        {
            OnClickEditModeButton?.Invoke();
        }
    }
}