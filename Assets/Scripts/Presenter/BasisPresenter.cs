using Controllers;
using Interface;
using UnityEngine;
using Utility;

namespace Presenter
{
    public class BasisPresenter<TY> where TY : MonoBehaviour, IWindow
    {
        protected TY view;
        

        public BasisPresenter()
        {
            OpenWindow();
        }

        public void OpenWindow()
        {
            
        }
        
        public void CloseWindow()
        {}
    }
}