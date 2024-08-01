using System;
using Controllers;
using Interface;
using Presenter;
using Presenter.View;
using UnityEngine;

namespace Utility
{
    public class ProjectContext : MonoBehaviour, IService
    {
        private BasicView _view;
        private UserInterfaceController _userInterfaceController;
        private UserConfigController _userConfigController;
        private ChatDataHandler _chatDataHandler;
        private DataStream _dataStream;
        
        [SerializeField] 
        private GameObject _projectContextGameObject;
        [SerializeField] 
        private GameObject _windowReferenceServicePrefab;

        public UserConfigController UserConfigController => _userConfigController;
        public ChatDataHandler ChatDataHandler => _chatDataHandler;
        public DataStream DataStream => _dataStream;

        private void Awake()
        {
            LoadUsers();
            _windowReferenceServicePrefab = Instantiate(_windowReferenceServicePrefab, _projectContextGameObject.transform);
            _userInterfaceController = new UserInterfaceController();
            _chatDataHandler = new ChatDataHandler();
            _chatDataHandler.LoadHistory();
            _dataStream = new DataStream();
            _dataStream.Initialize(_chatDataHandler);
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