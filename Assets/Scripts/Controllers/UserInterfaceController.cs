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
        private List<IPresenter> _presenters = new List<IPresenter>();
        
        public bool InstantiatePresenter(IPresenter presenter)
        {
            if (_presenters.Contains(presenter))
            {
                Debug.LogError($"Presenter {nameof(presenter)} already exists");
                return false;
            }
            _presenters.Add(presenter);
            return true;
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

        public void DestroyPresenter(IPresenter presenter)
        {
            _presenters.Remove(presenter);
            if(_presenters.Contains(presenter))
                Debug.LogError($"Failed to remove presenter {nameof(presenter)}");
        }

        public static void DestroyWindow<T>()
        {
            Object.Destroy(GameObject.Find($"{nameof(T)}(Clone)"));
        }
    }
}