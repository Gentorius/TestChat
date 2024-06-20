using System;
using System.Collections.Generic;
using System.Linq;
using Interface;
using Presenter.View;
using UnityEngine;
using Utility;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class UserInterfaceController
    {
        private readonly static Dictionary<Type ,IPresenter> _presenters = new ();

        public static T GetPresenter<T>() where T : IPresenter, new()
        {
            if (_presenters.TryGetValue(typeof(T), out var presenter))
                return presenter as T;
            var value = new T();
            _presenters.Add(typeof(T), value);
            return value;
        }
        
        public static T InstantiateWindow<T>() where T : MonoBehaviour
        {
            var window = GameObject.Find("WindowReferenceService(Clone)").GetComponent<AssetReferenceObject>().GetReference<T>();
            
            var userInterface = GameObject.Find("UserInterface(Clone)");

            window = Object.Instantiate(window, userInterface.transform);

            window.transform.SetParent(userInterface.transform);

            var component = window.GetComponent<T>();
            return component;
        }

        public static void OpenWindow<T>(T view) where T : MonoBehaviour
        {
            view.gameObject.SetActive(true);
        }

        public static void DestroyWindow<T>(T view) where T : MonoBehaviour
        {
            Object.Destroy(view.gameObject);
        }

        public static void CloseWindow<T>(T view) where T : MonoBehaviour
        {
            view.gameObject.SetActive(false);
        }
    }
}