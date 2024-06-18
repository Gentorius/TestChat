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
        
        private GameObject _usetInterfaceObject;
        [SerializeField] 
        private GameObject _projectContextGameObject;
        [SerializeField] 
        private GameObject _windowReferenceServicePrefab;

        private void Awake()
        {
            _windowReferenceServicePrefab = Instantiate(_windowReferenceServicePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            _windowReferenceServicePrefab.transform.SetParent(_projectContextGameObject.transform);
            _userInterfaceController = new UserInterfaceController(_windowReferenceServicePrefab.GetComponent<AssetReferenceObject>());
        }

        private void OnEnable()
        {
            var defaultPresenter = new WelcomePresenter();
            defaultPresenter.Initialize(_userInterfaceController);
        }
    }
}