using Models;
using Presenter;

namespace Controllers
{
    public class UserInterfaceController
    {
        private UserInterfaceModel _userInterfaceModel = new UserInterfaceModel();
        private UserInterfacePresenter _interfacePresenter;

        public UserInterfaceController()
        {
            _interfacePresenter = new UserInterfacePresenter(_userInterfaceModel);
            
        }

        public void LoadOnStart()
        {
            
        }
    }
}