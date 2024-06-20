using Controllers;
using Interface;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Utility;

namespace Presenter
{
    public class BasisPresenter<TY> : IPresenter where TY : MonoBehaviour, IWindow
    {
        protected TY View;
        protected UserInterfaceController UserInterfaceController;

        public void Initialize(UserInterfaceController userInterfaceController)
        {
            UserInterfaceController = userInterfaceController;
            
            if (UserInterfaceController.InstantiatePresenter(this))
                OpenWindow();
        }

        public void OpenWindow()
        {
            View = UserInterfaceController.InstantiateWindow<TY>();
            OnShow();
        }

        public void CloseWindow()
        {
            UserInterfaceController.DestroyWindow<TY>();
            UserInterfaceController.DestroyPresenter(this);
        }

        public virtual void OnShow()
        {
            
        }
    }
}