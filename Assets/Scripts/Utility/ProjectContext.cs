using System;
using Controllers;
using Interface;
using Presenter;
using Presenter.View;
using UnityEngine;

namespace Utility
{
    public class ProjectContext : MonoBehaviour
    {
        private BasicView _view;
        private UserInterfaceController _userInterfaceController;
        private UserConfigController _userConfigController;
        private ChatConfigController _chatConfigController;
        
        [SerializeField] 
        private GameObject _projectContextGameObject;
        [SerializeField] 
        private GameObject _windowReferenceServicePrefab;

        public UserConfigController UserConfigController => _userConfigController;
        public ChatConfigController ChatConfigController => _chatConfigController;

        private void Awake()
        {
            LoadUsers();
            _windowReferenceServicePrefab = Instantiate(_windowReferenceServicePrefab, _projectContextGameObject.transform);
            _userInterfaceController = new UserInterfaceController();
            _chatConfigController = new ChatConfigController();
            _chatConfigController.LoadFromJson();
        }

        private void OnEnable()
        {
            var defaultPresenter = UserInterfaceController.GetPresenter<WelcomePresenter>();
            defaultPresenter.OpenWindow();
        }
        
        private void LoadUsers()
        {
            _userConfigController ??= new UserConfigController();
            _userConfigController.LoadFromJson();
            _userConfigController.SetActiveUser();
            if (_userConfigController.UserStorage.ActiveUser == null)
                Debug.LogError("Active User is not set");
        }
    }
}