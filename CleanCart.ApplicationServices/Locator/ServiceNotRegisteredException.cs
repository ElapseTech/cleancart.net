using System;

namespace CleanCart.ApplicationServices.Locator
{
    public class ServiceNotRegisteredException : Exception
    {
        public ServiceNotRegisteredException(Type servicetype) : 
            base("Service of type '" + servicetype.FullName + "' is not registered in the service locator.")
        {
        }
    }
}