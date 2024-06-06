using System;
using Models;
using Presenter.View;

namespace Presenter
{
    public class UserInterfacePresenter : BasisPresenter<UserInterfaceView>
    {
        private UserInterfaceModel _userInterfaceModel;
        
        public UserInterfacePresenter(UserInterfaceModel userInterfaceModel)
        {
            _userInterfaceModel = userInterfaceModel;
        }
    }
}