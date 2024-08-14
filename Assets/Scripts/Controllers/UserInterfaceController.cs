using System;
using System.Collections.Generic;
using Attributes;
using Interface;
using UnityEngine;
using Utility;
using Utility.DependencyInjection;

namespace Controllers
{
    public class UserInterfaceController : MonoBehaviour, IUserInterfaceController
    {
        [Inject]
        private ProjectContext _projectContext;
        [Inject]
        private DIServiceRegistry _serviceRegistry;
        
        private static readonly Dictionary<Type ,IPresenter> _presenters = new ();

        public T GetPresenter<T>() where T : class, IPresenter, new()
        {
            if (_presenters.TryGetValue(typeof(T), out var presenter))
                return presenter as T;
            var value = new T();
            _presenters.Add(typeof(T), value);
            return value;
        }
        
        public IPresenter GetPresenter(Type type)
        {
            if (_presenters.TryGetValue(type, out var presenter))
                return presenter;
            var value = Activator.CreateInstance(type) as IPresenter;
            _presenters.Add(type, value);
            return value;
        }
        
        public T InstantiateWindow<T>() where T : MonoBehaviour
        {
            if(_projectContext == null)
                throw new NullReferenceException("ProjectContext is null in UserInterfaceController during InstantiateWindow");
            
            var window = _projectContext.GetComponent<AssetReferenceObject>().GetReference<T>();
            
            if (window == null)
                throw new NullReferenceException("Window is null in UserInterfaceController during InstantiateWindow");

            window = Instantiate(window, gameObject.transform);

            window.transform.SetParent(gameObject.transform);

            var component = window.GetComponent<T>();
            return component;
        }

        public void OpenWindow<T>(T view) where T : MonoBehaviour
        {
            view.gameObject.SetActive(true);
        }

        public void DestroyWindow<T>(T view) where T : MonoBehaviour
        {
            Destroy(view.gameObject);
        }

        public void CloseWindow<T>(T view) where T : MonoBehaviour
        {
            view.gameObject.SetActive(false);
        }
    }
}