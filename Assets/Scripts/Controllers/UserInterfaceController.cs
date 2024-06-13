using Models;
using Presenter;
using UnityEngine;

namespace Controllers
{
    public class UserInterfaceController
    {
        private UserInterfaceModel _userInterfaceModel = new UserInterfaceModel();
        private UserInterfacePresenter _interfacePresenter;

        public UserInterfaceController(GameObject projectContext)
        {
            _interfacePresenter = new UserInterfacePresenter(_userInterfaceModel, projectContext);
            
        }

        public void LoadOnStart()
        {
            
        }
    }
}