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
    private IUserDataHandler _userDataHandler;
    private ChatManager _chatManager;

    private void Awake()
    {
        InstantiatePrefabsOfServices();
        InstantiateServices();
        
        _container = new DIContainer(_serviceRegistry, _userInterfaceController);
    }

    private void Start()
    {
        _container.RegisterServices();
        var basePresenter = _userInterfaceController.GetPresenter<WelcomePresenter>();
        _userDataHandler.LoadUserData();
        _chatManager.LoadHistory();
        basePresenter.OpenWindow();
    }
    
    private void InstantiatePrefabsOfServices()
    {
        Instantiate(_projectContext);
        Instantiate(_coroutineRunner);
    }
    
    private void InstantiateServices()
    {
        _serviceRegistry = new DIServiceRegistry();
        _serviceRegistry.RegisterService(_projectContext);
        _serviceRegistry.RegisterService<IUserInterfaceController>(_userInterfaceController);
        _serviceRegistry.RegisterService<IDataHandler>(new DataHandler());
        _chatManager = (ChatManager)_serviceRegistry.RegisterService<IChatDataHandler>(new ChatManager());
        _serviceRegistry.RegisterService<IChatManager>(_chatManager);
        _userDataHandler = _serviceRegistry.RegisterService<IUserDataHandler>(new UserDataHandler());
        
        var dataStream = new DataStream();
        _serviceRegistry.RegisterService<IDataSender>(dataStream);
        _serviceRegistry.RegisterService<IDataReceiver>(dataStream);
    }
}