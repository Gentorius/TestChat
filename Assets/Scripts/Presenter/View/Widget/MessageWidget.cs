using Models;
using Presenter.View.Widget.Extras;
using UnityEngine;

namespace Presenter.View.Widget
{
    public class MessageWidget : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI _messageText;
        [SerializeField]
        private Sprite _userAvatar;
        [SerializeField]
        private TimeSidePicker _timeSent;
        
        public void SpawnMessage(MessageModel message, UserModel userModel, bool isFromActiveUser)
        {
            _messageText.text = message.Message;
            _timeSent.ShowTimeSent(isFromActiveUser, message.TimeSent);
            _userAvatar = userModel.Profile.ProfileImage;
        }
    }
}