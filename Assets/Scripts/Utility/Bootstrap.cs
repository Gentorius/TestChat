using System;
using Controllers;
using Models;
using UnityEngine;

namespace Utility
{
    public class Bootstrap : MonoBehaviour
    {
        private UserConfigController _userConfigController;


        private void Awake()
        {
            LoadUsers();
        }
        
        private void LoadUsers()
        {
            _userConfigController = new UserConfigController();
            _userConfigController.LoadFromJson();
        }
    }
}