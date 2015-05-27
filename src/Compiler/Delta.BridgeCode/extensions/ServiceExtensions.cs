using System;
using System.ComponentModel.Design;
using Delta.BridgeCode.ComponentModel;

namespace Delta.BridgeCode
{
    /// <summary>
    /// Extension method allowing to use generic forms of <c>GetService</c> on <see cref="IServiceProvider"/> instances,
    /// and generic forms of <c>AddService</c> and <c>RemoveService</c> on <see cref="IServiceContainer"/> instances.
    /// </summary>
    internal static class ServiceExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider, bool mandatory = false) where T : class
        {
            T t = serviceProvider.GetService(typeof(T)) as T;
            if (t == null)
            {
                if (mandatory) throw new ServiceNotFoundException<T>();
                else return null;
            }

            return t;
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, Func<T> serviceCreator) where T : class
        {
            serviceContainer.AddService(typeof(T), new ServiceCreatorCallback((c, t) => serviceCreator()));
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, Func<T> serviceCreator, bool promote) where T : class
        {
            serviceContainer.AddService(typeof(T), new ServiceCreatorCallback((c, t) => serviceCreator()), promote);
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, ServiceCreatorCallback callback) where T : class
        {
            serviceContainer.AddService(typeof(T), callback);
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, ServiceCreatorCallback callback, bool promote) where T : class
        {           
            serviceContainer.AddService(typeof(T), callback, promote);
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, T serviceInstance) where T : class
        {
            serviceContainer.AddService(typeof(T), serviceInstance);
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, T serviceInstance, bool promote) where T : class
        {
            serviceContainer.AddService(typeof(T), serviceInstance, promote);
        }

        public static void RemoveService<T>(this IServiceContainer serviceContainer, bool promote) where T : class
        {
            serviceContainer.RemoveService(typeof(T), promote);
        }

        public static void RemoveService<T>(this IServiceContainer serviceContainer) where T : class
        {
            serviceContainer.RemoveService(typeof(T));
        }
    }
}
