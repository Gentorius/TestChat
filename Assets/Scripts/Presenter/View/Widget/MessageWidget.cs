using System;
using System.Collections;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Action = System.Action;
using Image = UnityEngine.UI.Image;

namespace Presenter.View.Widget
{
    public class MessageWidget : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private const float _defaultTextFieldWidth = 300f;
        private const float _defaultTextFieldHeight = 110f;

        private const float _defaultMessageWidth = 370f;
        private const float _defaultMessageHeight = 200f;
        private const float _minMessageHeight = 120f;
        private const float _minMessageWidth = 200f;

        [SerializeField]
        private bool _canValidate;
        [SerializeField]
        private GameObject _otherUserMessageContainer;
        [SerializeField]
        private GameObject _otherUserMessage;
        [SerializeField]
        private TextMeshProUGUI _otherUserMessageText;
        [SerializeField]
        private Image _otherUserAvatar;
        [SerializeField]
        private TextMeshProUGUI _otherUserTimeSent;
        [SerializeField]
        private GameObject _currentUserMessageContainer;
        [SerializeField]
        private GameObject _currentUserMessage;
        [SerializeField]
        private TextMeshProUGUI _currentUserMessageText;
        [SerializeField]
        private TextMeshProUGUI _currentUserTimeSent;
        [SerializeField]
        private Button _deleteButton;

        private float _heightChange;
        private Coroutine _holdCoroutine;
        private bool _isPressed;
        private int _messageIndex;
        private float _pressDuration;

        private void OnDisable()
        {
            if (_holdCoroutine != null) StopCoroutine(_holdCoroutine);
        }

        private void OnDestroy()
        {
            if (_holdCoroutine != null) StopCoroutine(_holdCoroutine);

            _deleteButton.onClick.RemoveListener(DeleteMessage);
            OnEditModeStart = null;
            OnDeleteMessage = null;
        }

        private void OnValidate()
        {
            if (!_canValidate) return;

            if (_currentUserMessageText != null && _currentUserMessage != null)
            {
                var messageSize = _currentUserMessageText.GetPreferredValues(_currentUserMessageText.text);
                AdjustMessageSize(messageSize, _currentUserMessage);
            }

            if (_otherUserMessageText != null && _otherUserMessage != null)
            {
                var messageSize = _otherUserMessageText.GetPreferredValues(_otherUserMessageText.text);
                AdjustMessageSize(messageSize, _otherUserMessage);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
            _holdCoroutine = StartCoroutine(CheckHoldDuration());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
            if (_holdCoroutine != null)
            {
                StopCoroutine(_holdCoroutine);
                _holdCoroutine = null;
            }

            _pressDuration = 0f;
        }

        public event Action OnEditModeStart;
        public event Action<int, float> OnDeleteMessage;

        public float InitializeMessage(Message message, User user, bool isCurrentUser, int index,
            bool isEditMode = false)
        {
            if (isCurrentUser)
            {
                _currentUserMessageContainer.SetActive(true);
                _otherUserMessageContainer.SetActive(false);
                InitializeCurrentUserMessage(message);
            }
            else
            {
                _currentUserMessageContainer.SetActive(false);
                _otherUserMessageContainer.SetActive(true);
                InitializeOtherUserMessage(message, user);
            }

            _deleteButton.gameObject.SetActive(isEditMode);
            _deleteButton.onClick.AddListener(DeleteMessage);
            _messageIndex = index;

            return _heightChange;
        }

        public void EnableEditMode()
        {
            _deleteButton.gameObject.SetActive(true);
        }

        public void DisableEditMode()
        {
            if (_deleteButton != null) _deleteButton.gameObject.SetActive(false);
        }

        private void InitializeCurrentUserMessage(Message message)
        {
            _currentUserMessageText.GetPreferredValues(message.MessageText);
            _currentUserMessageText.text = message.MessageText;
            _currentUserTimeSent.text = message.TimeSent;

            var messageSize = _currentUserMessageText.GetPreferredValues(message.MessageText);
            AdjustMessageSize(messageSize, _currentUserMessage);
        }

        private void InitializeOtherUserMessage(Message message, User user)
        {
            _otherUserMessageText.text = message.MessageText;
            _otherUserAvatar.sprite = user.Profile.ProfileImage;
            _otherUserTimeSent.text = message.TimeSent;

            var messageSize = _otherUserMessageText.GetPreferredValues(message.MessageText);
            ;
            AdjustMessageSize(messageSize, _otherUserMessage);
        }

        private void AdjustMessageSize(Vector2 messageSize, GameObject messageObject)
        {
            var scaleRatioWidth = messageSize.x / _defaultTextFieldWidth;
            var scaleRatioHeight = messageSize.y / _defaultTextFieldHeight;

            var newWidth = _defaultMessageWidth * scaleRatioWidth;
            var newHeight = _defaultMessageHeight * scaleRatioHeight;

            if (newHeight < _minMessageHeight) newHeight = _minMessageHeight;

            _heightChange = newHeight;

            if (newWidth < _minMessageWidth) newWidth = _minMessageWidth;

            messageObject.GetComponent<LayoutElement>().preferredHeight = newHeight;
            messageObject.GetComponent<LayoutElement>().preferredWidth = newWidth;
            messageObject.GetComponent<LayoutElement>().minHeight = newHeight;
            messageObject.GetComponent<LayoutElement>().minWidth = newWidth;
        }

        private IEnumerator CheckHoldDuration()
        {
            while (_isPressed)
            {
                _pressDuration += Time.deltaTime;
                if (_pressDuration >= 2f)
                {
                    OnEditModeStart?.Invoke();
                    break;
                }

                yield return null;
            }
        }

        private void DeleteMessage()
        {
            OnDeleteMessage?.Invoke(_messageIndex, _heightChange);
            Destroy(gameObject);
        }
    }
}