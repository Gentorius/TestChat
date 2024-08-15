using Controllers;
using Interface;
using Models;
using Presenter;
using UnityEngine;
using Utility;
using Utility.DependencyInjection;

public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private ProjectContext _projectContext;
    [SerializeField]
    private UserInterfaceController _userInterfaceController;
    [SerializeField]
    private CoroutineRunner _coroutineRunner;
    
    private DIServiceRegistry _serviceRegistry;
    private DIContainer _container;

    private void Awake()
    {
        InstantiatePrefabsOfServices();
        InstantiateServices();
        
        _container = new DIContainer(_serviceRegistry);
    }

    private void Start()
    {
        _container.RegisterServices();
        var basePresenter = _userInterfaceController.GetPresenter<WelcomePresenter>();
        basePresenter.OpenWindow();
    }
    
    private void InstantiatePrefabsOfServices()
    {
        Instantiate(_projectContext);
        Instantiate(_coroutineRunner);
        Instantiate(_userInterfaceController);
    }
    
    private void InstantiateServices()
    {
        _serviceRegistry = new DIServiceRegistry();
        _serviceRegistry.InstantiateService(_projectContext);
        _serviceRegistry.InstantiateService<IDataHandler>(new DataHandler());
        _serviceRegistry.InstantiateService<IChatDataHandler>(new ChatDataHandler());
        _serviceRegistry.InstantiateService<IChatManager>(new ChatManager());
        _serviceRegistry.InstantiateService<IChatHistory>(new ChatHistory());
        _serviceRegistry.InstantiateService<IUserDataHandler>(new UserDataHandler());
        _serviceRegistry.InstantiateService<IUserInterfaceController>(_userInterfaceController);
        
        var dataStream = new DataStream();
        _serviceRegistry.InstantiateService<IDataSender>(dataStream);
        _serviceRegistry.InstantiateService<IDataReceiver>(dataStream);
    }
}