using System;
using Controllers;
using Models;
using UnityEngine;

namespace Utility
{
    public class Bootstrap : MonoBehaviour
    {
        private UserConfigController _userConfigController;
        private UserInterfaceController _interfaceController;
        [SerializeField] 
        private GameObject _projectContext;

        private void Awake()
        {
            LoadUsers();
            Instantiate(_projectContext, new Vector3(0, 0, 0), Quaternion.identity);
            _interfaceController = new UserInterfaceController(_projectContext);
        }
        
        private void LoadUsers()
        {
            _userConfigController = new UserConfigController();
            _userConfigController.LoadFromJson();
        }

        
    }
}