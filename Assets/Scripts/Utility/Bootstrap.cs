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
        [SerializeField] 
        private GameObject _projectContext;

        private void Awake()
        {
            LoadUsers();
            _activeUser = _userConfigController.SetActiveUser();
            Instantiate(_projectContext, new Vector3(0, 0, 0), Quaternion.identity);
            _interfaceController = new UserInterfaceController();
        }
        
        private void LoadUsers()
        {
            _userConfigController = new UserConfigController();
            _userConfigController.LoadFromJson();
        }

        
    }
}