using System;
using UnityEngine;

namespace Presenter.View.Widget.Extras
{
    public class TimeSidePicker : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI _timeTextFromActiveUser;
        [SerializeField]
        private TMPro.TextMeshProUGUI _timeTextFromOtherUser;
        
        public void ShowTimeSent(bool isFromActiveUser, DateTime time)
        {
            SetActiveTimeText(isFromActiveUser);
            var timeText = time.ToString("HH:mm");
            if (isFromActiveUser)
            {
                _timeTextFromActiveUser.text = timeText;
            }
            else
            {
                _timeTextFromOtherUser.text = timeText;
            }
        }

        private void SetActiveTimeText(bool isFromActiveUser)
        {
            _timeTextFromActiveUser.gameObject.SetActive(isFromActiveUser);
            _timeTextFromOtherUser.gameObject.SetActive(!isFromActiveUser);
        }
    }
}