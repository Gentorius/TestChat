using System;
using Controllers;
using Models;
using UnityEngine;

namespace Utility
{
    public class Bootstrap : MonoBehaviour
    {
        private UserConfigController _userConfigController;
        private UserModel _activeUser;
        private UserInterfaceController _interfaceController;

        private void Awake()
        {
            LoadUsers();
            _activeUser = _userConfigController.SetActiveUser();
            _interfaceController = new UserInterfaceController();
        }
        
        private void LoadUsers()
        {
            _userConfigController = new UserConfigController();
            _userConfigController.LoadFromJson();
        }

        
    }
}