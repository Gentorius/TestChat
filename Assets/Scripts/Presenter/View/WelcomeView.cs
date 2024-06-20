using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Presenter.View
{
    public class WelcomeView : BasicView
    {
        [SerializeField] 
        private Button _button;
        
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
            Debug.Log("MouseDown");
        }
    }
}