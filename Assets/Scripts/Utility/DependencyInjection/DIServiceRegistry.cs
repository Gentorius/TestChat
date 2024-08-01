using System;
using System.Collections.Generic;
using Interface;
using Unity.VisualScripting;

namespace Utility.DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public static class DIServiceRegistry
    {
        private static readonly Dictionary<Type, IService> Services = new();
        
        public static T InstantiateService<T>(T service) where T : IService
        {
            Services[typeof(T)] = service;
            return service;
        }
        
        public static bool TryGetService(Type type,out IService service) 
        {
            if (Services.TryGetValue(type, out var value))
            {
                service = value;
                return true;
            }

            service = default;
            return false;
        }

        public static Dictionary<Type, IService> GetAllServices()
        {
            return Services;
        }
    }
}