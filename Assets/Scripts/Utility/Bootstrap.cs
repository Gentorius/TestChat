using System;
using Controllers;
using Models;
using UnityEngine;

namespace Utility
{
    public class Bootstrap : MonoBehaviour
    {
        private UserConfigController _userConfigController;
        
        [SerializeField] 
        private GameObject _projectContext; //Used for behind the scenes processes
        [SerializeField] 
        private GameObject _userInterface; //Used for user in

        private void Awake()
        {
            LoadUsers();
            
            _projectContext = Instantiate(_projectContext, new Vector3(0, 0, 0), Quaternion.identity);

            _userInterface = Instantiate(_userInterface, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
        private void LoadUsers()
        {
            _userConfigController = new UserConfigController();
            _userConfigController.LoadFromJson();
        }

        
    }
}