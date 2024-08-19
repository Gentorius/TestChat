using System;
using Presenter.View.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.View
{
    public class WelcomeView : BasicView
    {
        [SerializeField] 
        private Button _button;
        [SerializeField]
        private UserBasicProfileWidget _userBasicProfileWidget;
        
        public UserBasicProfileWidget UserBasicProfileWidget => _userBasicProfileWidget;
        
        public event Action OnClick;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnMouseDownOrTap);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnMouseDownOrTap);
        }

        private void OnMouseDownOrTap()
        {
            OnClick?.Invoke();
        }
    }
}