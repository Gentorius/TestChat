using Models;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Presenter.View.Widget
{
    public class MessageWidget : MonoBehaviour
    {
        private const float _defaultTextFieldWidth = 300f;
        private const float _defaultTextFieldHeight = 110f;
        
        private const float _defaultMessageWidth = 370f;
        private const float _defaultMessageHeight = 200f;
        private const float _minMessageHeight = 120f;
        private const float _minMessageWidth = 200f;
        
        [SerializeField]
        private bool _canValidate = false;
        
        [SerializeField]
        private GameObject _otherUserMessageContainer;
        [SerializeField]
        private GameObject _otherUserMessage;
        [SerializeField]
        private TMPro.TextMeshProUGUI _otherUserMessageText;
        [SerializeField]
        private Image _otherUserAvatar;
        [SerializeField]
        private TMPro.TextMeshProUGUI _otherUserTimeSent;
        
        [SerializeField]
        private GameObject _currentUserMessageContainer;
        [SerializeField]
        private GameObject _currentUserMessage;
        [SerializeField]
        private TMPro.TextMeshProUGUI _currentUserMessageText;
        [SerializeField]
        private TMPro.TextMeshProUGUI _currentUserTimeSent;
        
        private float _heightChange;
        
        public float InitializeMessage(Message message, User user, bool isCurrentUser)
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

            return _heightChange;
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
            
            var messageSize = _otherUserMessageText.GetPreferredValues(message.MessageText);;
            AdjustMessageSize(messageSize, _otherUserMessage);
        }
        
        private void AdjustMessageSize(Vector2 messageSize, GameObject messageObject)
        {
            var scaleRatioWidth = messageSize.x / _defaultTextFieldWidth;
            var scaleRatioHeight = messageSize.y / _defaultTextFieldHeight;
            
            var newWidth = _defaultMessageWidth * scaleRatioWidth;
            var newHeight = _defaultMessageHeight * scaleRatioHeight;

            if (newHeight < _minMessageHeight)
            {
                newHeight = _minMessageHeight;
            }

            _heightChange = newHeight;
            
            if (newWidth < _minMessageWidth)
            {
                newWidth = _minMessageWidth;
            }
            
            messageObject.GetComponent<LayoutElement>().preferredHeight = newHeight;
            messageObject.GetComponent<LayoutElement>().preferredWidth = newWidth;
            messageObject.GetComponent<LayoutElement>().minHeight = newHeight;
            messageObject.GetComponent<LayoutElement>().minWidth = newWidth;
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
    }
}