using System;
using DG.Tweening;
using Interface;
using Models;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Presenter.View.Widget
{
    public class BaseMessageWidget : MonoBehaviour, IMessageWidget
    {
        const float _defaultTextFieldWidth = 300f;
        const float _defaultTextFieldHeight = 110f;

        const float _defaultMessageWidth = 370f;
        const float _defaultMessageHeight = 200f;
        const float _minMessageHeight = 120f;
        const float _minMessageWidth = 200f;

        [SerializeField]
        bool _canValidate;

        [SerializeField]
        GameObject _message;
        [SerializeField]
        Image _profileImage;
        [SerializeField]
        TextMeshProUGUI _messageText;
        [SerializeField]
        TextMeshProUGUI _timeSent;
        [SerializeField]
        Button _deleteButton;

        float _heightChange;
        
        public event Action<int, float> OnDeleteMessage;
        
        public bool IsDestroyed { get; private set; }

        [ShowInInspector]
        [ReadOnly]
        int _messageIndex;

        void OnDisable()
        {
            _deleteButton.onClick.RemoveListener(OnDeleteHandler);
        }

        void OnDestroy()
        {
            _deleteButton.onClick.RemoveListener(OnDeleteHandler);
            OnDeleteMessage = null;
        }

        void OnValidate()
        {
            if (!_canValidate) return;

            if (_messageText == null || _message == null) return;

            var messageSize = _messageText.GetPreferredValues(_messageText.text);
            AdjustMessageSize(messageSize);
        }

        public virtual float InitializeMessage(Message message, Sprite profileImage, int index,
            bool isEditMode = false)
        {
            _messageText.GetPreferredValues(message.MessageText);
            _messageText.text = message.MessageText;
            _timeSent.text = message.TimeSent;
            _profileImage.sprite = profileImage;

            var messageSize = _messageText.GetPreferredValues(message.MessageText);
            AdjustMessageSize(messageSize);

            _deleteButton.gameObject.SetActive(isEditMode);
            _deleteButton.onClick.AddListener(OnDeleteHandler);
            _messageIndex = index;

            AnimateAppearance();

            return _heightChange;
        }

        public void EnableEditMode()
        {
            _deleteButton.gameObject.SetActive(true);
        }

        public void DisableEditMode()
        {
            _deleteButton.gameObject.SetActive(false);
        }

        public void SetMessageIndex(int index)
        {
            _messageIndex = index;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        void AdjustMessageSize(Vector2 messageSize)
        {
            var scaleRatioWidth = messageSize.x / _defaultTextFieldWidth;
            var scaleRatioHeight = messageSize.y / _defaultTextFieldHeight;

            var newWidth = _defaultMessageWidth * scaleRatioWidth;
            var newHeight = _defaultMessageHeight * scaleRatioHeight;

            if (newHeight < _minMessageHeight) newHeight = _minMessageHeight;

            _heightChange = newHeight;

            if (newWidth < _minMessageWidth) newWidth = _minMessageWidth;

            _message.GetComponent<LayoutElement>().preferredHeight = newHeight;
            _message.GetComponent<LayoutElement>().preferredWidth = newWidth;
            _message.GetComponent<LayoutElement>().minHeight = newHeight;
            _message.GetComponent<LayoutElement>().minWidth = newWidth;
        }

        void OnDeleteHandler()
        {
            OnDeleteMessage?.Invoke(_messageIndex, _heightChange);
            IsDestroyed = true;
            gameObject.SetActive(false);
        }

        void AnimateAppearance()
        {
            _messageText.gameObject.SetActive(false);
            _timeSent.gameObject.SetActive(false);
            gameObject.transform.localScale = Vector3.zero;
            gameObject.transform.DOScale(Vector3.one, 0.5f);
            _messageText.gameObject.SetActive(true);
            _timeSent.gameObject.SetActive(true);
        }
    }
}