
using System;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices.Locator
{
    public class ServiceLocator
    {
        private readonly IDictionary<Type, dynamic> _implementations = new Dictionary<Type, dynamic>(); 

        public void Register<TService>(TService implementation) where TService : class
        {
            var serviceType = typeof (TService);
            if (ServiceIsDefined(serviceType))
            {
                throw new CannotRegisterServiceTwiceException(serviceType);
            }
            _implementations.Add(serviceType, implementation);
        }

        public TService Resolve<TService>() where TService : class
        {
            var serviceType = typeof (TService);
            if (!ServiceIsDefined(serviceType))
            {
                throw new ServiceNotRegisteredException(serviceType);
            }
            return _implementations[typeof(TService)] as TService;
        }

        private bool ServiceIsDefined(Type serviceType) 
        {
            return _implementations.ContainsKey(serviceType);
        }
    }
}
