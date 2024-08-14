using Attributes;
using Controllers;
using Interface;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using Utility;

namespace Presenter
{
    public abstract class BasicPresenter<TY> : IPresenter where TY : MonoBehaviour, IWindow
    {
        protected TY View;
        
        [Inject]
        protected UserInterfaceController UserInterfaceController;

        public void OpenWindow()
        {
            if (View.IsUnityNull())
            {
                View = UserInterfaceController.InstantiateWindow<TY>();
                OnShow();
                return;
            }
            
            UserInterfaceController.OpenWindow(View);
            OnShow();
        }

        protected void CloseWindow()
        {
            OnHide();
            UserInterfaceController.DestroyWindow(View);
        }

        protected virtual void OnShow()
        {
            
        }
        
        protected virtual void OnHide()
        {
            
        }
    }
}