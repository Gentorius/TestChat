using Controllers;
using Interface;
using Models;
using UnityEngine;
using Utility;
using Utility.DependencyInjection;

public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private ProjectContext _projectContext;
    [SerializeField]
    private UserInterfaceController _userInterfaceController;
    
    
    private DIServiceRegistry _serviceRegistry;
    private DIContainer _container;

    private void Awake()
    {
        InstantiateServices();
        
        _container = new DIContainer(_serviceRegistry);
        _container.RegisterServices();
        
        
        _container.InjectDependenciesInAllClasses();
    }

    private void Start()
    {
        _container.RegisterServices();
        _container.InjectDependenciesInAllClasses();
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