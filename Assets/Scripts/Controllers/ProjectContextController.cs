using Interface;
using Presenter.View;
using UnityEngine;

namespace Controllers
{
    public class ProjectContextController : MonoBehaviour
    {
        private BasicView _view;
        private GameObject _usetInterfaceObject;
        [SerializeField] 
        private GameObject _projectContextGameObject;

        public void OpenWindow(IWindow view)
        {
            _view = (BasicView) view;
            Instantiate(_view, new Vector3(0, 0, 0), Quaternion.identity);
        }

        public void InstantiateUserInterface(IWindow view)
        {
            _view = (BasicView) view;
        }
    }
}