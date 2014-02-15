
using System;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices.Locator
{
    public class ServiceLocator
    {
        private static readonly IDictionary<Type, dynamic> Implementations = new Dictionary<Type, dynamic>(); 

        public static void Register<TService>(TService implementation) where TService : class
        {
            var serviceType = typeof (TService);
            if (ServiceIsDefined(serviceType))
            {
                throw new CannotRegisterServiceTwiceException(serviceType);
            }
            Implementations.Add(serviceType, implementation);
        }

        public static TService Resolve<TService>() where TService : class
        {
            var serviceType = typeof (TService);
            if (!ServiceIsDefined(serviceType))
            {
                throw new ServiceNotRegisteredException(serviceType);
            }
            return Implementations[typeof(TService)] as TService;
        }

        private static bool ServiceIsDefined(Type serviceType) 
        {
            return Implementations.ContainsKey(serviceType);
        }

        public static void Reset()
        {
            Implementations.Clear();
        }
    }
}
