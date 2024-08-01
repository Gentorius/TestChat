using System;
using System.Collections.Generic;
using Interface;

namespace Utility.DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public class DIServiceRegistry
    {
        private readonly Dictionary<Type, IService> Services = new();
        
        public T InstantiateService<T>(T service) where T : IService
        {
            Services[typeof(T)] = service;
            return service;
        }
        
        public bool TryGetService(Type type,out IService service) 
        {
            if (Services.TryGetValue(type, out var value))
            {
                service = value;
                return true;
            }

            service = default;
            return false;
        }

        public Dictionary<Type, IService> GetAllServices()
        {
            return Services;
        }
    }
}