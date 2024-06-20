using Controllers;
using Interface;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using Utility;

namespace Presenter
{
    public abstract class BasisPresenter<TY> : IPresenter where TY : MonoBehaviour, IWindow
    {
        protected TY View;

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

        public void CloseWindow()
        {
            UserInterfaceController.DestroyWindow(View);
        }

        protected virtual void OnShow()
        {
            
        }
    }
}