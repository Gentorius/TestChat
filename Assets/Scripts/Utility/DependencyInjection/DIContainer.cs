using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Attributes;
using Interface;
using UnityEngine;

namespace Utility.DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public class DIContainer : IDependencyInjection
    {
        private readonly List<object> _instances = new();
        private readonly List<Type> _failedDependencies = new();
        private readonly object _lock = new ();
        private readonly DIServiceRegistry _serviceRegistry;
        private IUserInterfaceController _userInterfaceController;

        public DIContainer(DIServiceRegistry serviceRegistry, IUserInterfaceController userInterfaceController)
        {
            _serviceRegistry = serviceRegistry;
            _userInterfaceController = userInterfaceController;
            RegisterInstance(this);
        }
        
        public DIContainer(DIServiceRegistry serviceRegistry)
        {
            _serviceRegistry = serviceRegistry;
            RegisterInstance(this);
        }

        public void RegisterServices()
        {
            lock (_lock)
            {
                foreach (var (_, service) in _serviceRegistry.GetAllServices())
                {
                    RegisterInstance(service);
                }
                UpdateDependencies();
            }
        }

        public void RegisterInstance(object instance)
        {
            lock (_lock)
            {
                _instances?.Add(instance);
                InjectDependencies(instance);
            }
        }

        public void InjectDependencies(object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<Inject>() == null) continue;

                var fieldType = field.FieldType;
                
                if (fieldType == typeof(DIContainer))
                {
                    field.SetValue(obj, this);
                    continue;
                }
                
                if (_serviceRegistry.TryGetService(fieldType, out var service))
                {
                    field.SetValue(obj, service);
                    continue;
                }
                
                if (fieldType.GetInterfaces().Contains(typeof(IPresenter)))
                {
                    field.SetValue(obj, _userInterfaceController.GetPresenter(fieldType));
                    continue;
                }

                lock (_lock)
                {
                    if(_failedDependencies.Contains(fieldType)) continue;
                    _failedDependencies.Add(fieldType);
                }
            }
        }

        private void UpdateDependencies()
        {
            lock (_lock)
            {
                foreach (var instance in _instances)
                {
                    InjectDependencies(instance);
                }
                
                TryResolveAllFailedDependencies();
                PrintFailedDependenciesError();
            }
        }

        private void TryResolveObjectFailedDependencies(object obj)
        {
            var type = obj.GetType();
            lock (_lock)
            {
                _failedDependencies.Remove(type);
                InjectDependencies(obj);
            }
        }

        private void TryResolveAllFailedDependencies()
        {
            lock (_lock)
            {
                foreach (var instance in _instances)
                {
                    TryResolveObjectFailedDependencies(instance);
                }
            }
        }

        private void PrintFailedDependenciesError()
        {
            if (_failedDependencies.Count == 0) return;
            
            lock (_lock)
            {
                
                Debug.LogError(
                    $"Failed to inject dependencies of types: {string.Join(", ", _failedDependencies.ConvertAll(x => x.Name))}");
            }
        }
        
    }
}