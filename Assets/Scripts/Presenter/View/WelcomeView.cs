using System;
using TMPro;
using UnityEngine;

namespace Presenter.View
{
    public class WelcomeView : WelcomePresenter
    {
        [SerializeField]
        private Sprite _activeUserProfilePicture;
        [SerializeField] 
        private TMP_Text _continueText;
        [SerializeField] 
        private TMP_Text _activeUserNickname;

        private void OnEnable()
        {
            OnClickHandler += ClickHandler;
        }

        private void OnDisable()
        {
            OnClickHandler -= ClickHandler;
        }

        private void ClickHandler()
        {
            throw new NotImplementedException();
        }
    }
}