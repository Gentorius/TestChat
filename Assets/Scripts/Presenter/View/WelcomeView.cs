using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presenter.View
{
    public class WelcomeView : BasicView, IPointerDownHandler
    {
        public event Action OnClick;
        
        private void OnMouseDown()
        {
            OnClick?.Invoke();
            Debug.Log("MouseDown");
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick?.Invoke();
            Debug.Log("MouseDown");
        }
    }
}