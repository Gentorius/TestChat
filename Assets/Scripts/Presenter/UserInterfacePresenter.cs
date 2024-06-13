using System;
using Controllers;
using Models;
using Presenter.View;
using UnityEngine;

namespace Presenter
{
    public class UserInterfacePresenter : BasisPresenter<UserInterfaceView>
    {
        private UserInterfaceModel _userInterfaceModel;
        
        public UserInterfacePresenter(UserInterfaceModel userInterfaceModel, GameObject projectContext)
        {
            _userInterfaceModel = userInterfaceModel;
            
            OpenUserInterfaceWindow(projectContext);
        }

        private void OpenUserInterfaceWindow(GameObject projectContext)
        {
            projectContext.GetComponent<ProjectContextController>().InstantiateUserInterface(view);
        }
    }
}