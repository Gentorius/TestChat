using System;
using System.Collections.Generic;
using Interface;

namespace Utility.DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public class DIServiceRegistry : IService
    {
        private readonly Dictionary<Type, IService> _services = new();
        
        public DIServiceRegistry()
        {
            _services[typeof(DIServiceRegistry)] = this;
        }
        
        public T RegisterService<T>(T service) where T : IService
        {
            _services[typeof(T)] = service;
            return service;
        }
        
        public bool TryGetService(Type type,out IService service) 
        {
            if (_services.TryGetValue(type, out var value))
            {
                service = value;
                return true;
            }

            service = default;
            return false;
        }

        public Dictionary<Type, IService> GetAllServices()
        {
            return _services;
        }
    }
}