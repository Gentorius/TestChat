using System;
using TMPro;
using UnityEngine;

namespace Presenter.View
{
    public class WelcomeView : BasicView
    {
        [SerializeField]
        private GameObject _activeUserProfilePicture;
        [SerializeField] 
        private GameObject _activeUserNickname;

        public Action OnClickHandler;

        private void OnEnable()
        {
            
        }

        private void ClickHandler()
        {
            throw new NotImplementedException();
        }
    }
}