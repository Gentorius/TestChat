using Controllers;
using Interface;
using UnityEngine;

namespace Presenter
{
    public class BasisPresenter<TY> where TY : MonoBehaviour, IWindow
    {
        protected TY view;
        private ProjectContextController _projectContextController;

        public void OpenWindow()
        {
            GameObject.Find("Project Context").GetComponent<ProjectContextController>().OpenWindow(view);
        }
        
        public void CloseWindow()
        {
            
        }
    }
}