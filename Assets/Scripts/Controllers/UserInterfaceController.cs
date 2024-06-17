using Models;
using Presenter;
using UnityEngine;

namespace Controllers
{
    public class UserInterfaceController
    {
        public void InstantiateWindow(GameObject window)
        {
            window = Object.Instantiate(window, new Vector3(0, 0, 0), Quaternion.identity);
            var userInterface = GameObject.Find("UserInterface");
            window.transform.SetParent(userInterface.transform);
        }
    }
}