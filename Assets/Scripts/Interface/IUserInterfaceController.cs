using System;
using UnityEngine;

namespace Interface
{
    public interface IUserInterfaceController : IService
    {
        T GetPresenter<T>() where T : class, IPresenter, new();
        IPresenter GetPresenter(Type type);
        T InstantiateWindow<T>() where T : MonoBehaviour;
        void OpenWindow<T>(T view) where T : MonoBehaviour;
        void DestroyWindow<T>(T view) where T : MonoBehaviour;
        void CloseWindow<T>(T view) where T : MonoBehaviour;
    }
}