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
        private readonly Dictionary<Type, Type[]> _failedDependencies = new();
        private readonly object _lock = new ();

        public DIContainer()
        {
            RegisterInstance(this);
        }

        public void RegisterServices()
        {
            lock (_lock)
            {
                foreach (var (_, service) in DIServiceRegistry.GetAllServices())
                {
                    RegisterInstance(service);
                    UpdateDependencies();
                }
            }
        }

        public void InjectDependenciesInAllClasses()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (type.IsClass && !type.IsAbstract)
                {
                    var obj = Activator.CreateInstance(type);
                    RegisterInstance(obj);
                }
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

        private void InjectDependencies(object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var failedInjections = new List<Type>();
            var hasFailedInjections = false;
            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<Inject>() == null) continue;

                var fieldType = field.FieldType;
                if (DIServiceRegistry.TryGetService(fieldType, out var service))
                {
                    field.SetValue(obj, service);
                    continue;
                }

                if (fieldType == typeof(DIContainer))
                {
                    field.SetValue(obj, this);
                    continue;
                }

                hasFailedInjections = true;
                failedInjections.Add(fieldType);
            }

            if (!hasFailedInjections) return;
            RegisterFailedInjection(obj.GetType(), failedInjections.ToArray());
            PrintFailedDependenciesInClass(obj, failedInjections.ConvertAll(type => type.Name).ToArray());
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

        private void RegisterFailedInjection(Type type, Type[] failedDependencies)
        {
            lock (_lock)
            {
                _failedDependencies[type] = failedDependencies;
            }
        }

        private static void PrintFailedDependenciesInClass(object obj, string[] failedDependencies)
        {
            var type = obj.GetType();
            Debug.LogWarning(
                $"Failed to inject dependencies into {type} of types: {string.Join(", ", failedDependencies)}");
        }

        private void TryResolveObjectFailedDependencies(object obj)
        {
            var type = obj.GetType();
            lock (_lock)
            {
                if (!_failedDependencies.TryGetValue(type, out _)) return;
                InjectDependencies(obj);
                _failedDependencies.Remove(type);
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
            lock (_lock)
            {
                foreach (var (type, failedDependencies) in _failedDependencies)
                {
                    var failedDependenciesList = failedDependencies.ToList();

                    Debug.LogError(
                        $"Failed to inject dependencies into {type} of types: {string.Join(", ", failedDependenciesList.ConvertAll(x => x.Name))}");
                }
            }
        }
        
    }
}