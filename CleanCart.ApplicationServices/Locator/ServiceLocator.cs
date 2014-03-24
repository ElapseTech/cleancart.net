
using System;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices.Locator
{
    public class ServiceLocator
    {
        private static volatile ServiceLocator _locator;
        private static readonly object SyncRoot = new object();

        private readonly IDictionary<Type, dynamic> _implementations = new Dictionary<Type, dynamic>();

        public void Register<TService>(TService implementation) where TService : class
        {
            var serviceType = typeof(TService);
            if (IsServiceDefined(serviceType))
            {
                throw new CannotRegisterServiceTwiceException(serviceType);
            }
            _implementations.Add(serviceType, implementation);
        }

        public TService Resolve<TService>() where TService : class
        {
            var serviceType = typeof(TService);
            if (!IsServiceDefined(serviceType))
            {
                throw new ServiceNotRegisteredException(serviceType);
            }
            return _implementations[typeof(TService)] as TService;
        }

        public bool IsServiceDefined(Type serviceType)
        {
            return _implementations.ContainsKey(serviceType);
        }


        public static ServiceLocator Locator
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_locator == null)
                    {
                        _locator = new ServiceLocator();
                    }
                }
                return _locator;
            }
        }


    }
}
