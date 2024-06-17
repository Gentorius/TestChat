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
            var windowReferenceService = GameObject.Find("WindowReferenceService");
            var window = windowReferenceService.GetComponent<AssetReferenceObject>().GetReference<T>();
            
            window = Object.Instantiate(window, new Vector3(0, 0, 0), Quaternion.identity);
            
            var userInterface = GameObject.Find("UserInterface");
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
    }
}