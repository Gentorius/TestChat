using System;
using Controllers;
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
        private DataStream _dataStream;
        
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
            _dataStream = new DataStream();
            _dataStream.Initialize(_chatConfigController);
        }

        private void OnEnable()
        {
            var defaultPresenter = UserInterfaceController.GetPresenter<WelcomePresenter>();
            defaultPresenter.OpenWindow();
        }

        private void OnDisable()
        {
            _dataStream.Dispose();
        }

        private void LoadUsers()
        {
            _userConfigController ??= new UserConfigController();
            _userConfigController.LoadFromJson();
        }
    }
}