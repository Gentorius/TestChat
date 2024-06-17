using System;
using TMPro;
using UnityEngine;

namespace Presenter.View
{
    public class WelcomeView : BasicView
    {
        [SerializeField]
        private Sprite _activeUserProfilePicture;
        [SerializeField] 
        private TMP_Text _continueText;
        [SerializeField] 
        private TMP_Text _activeUserNickname;

        public Action OnClickHandler;
        
        private void ClickHandler()
        {
            throw new NotImplementedException();
        }
    }
}