using Attributes;
using Interface;
using UnityEngine;

namespace Presenter
{
    public abstract class BasicPresenter<TY> : IPresenter where TY : MonoBehaviour, IWindow
    {
        protected TY View;
        
        [Inject]
        protected IUserInterfaceController UserInterfaceController;

        public void OpenWindow()
        {
            if (View == null)
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
            UserInterfaceController.DestroyWindow(View);
            OnHide();
        }

        protected virtual void OnShow()
        {
            
        }
        
        protected virtual void OnHide()
        {
            
        }
    }
}