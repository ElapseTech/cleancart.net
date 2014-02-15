using System;

namespace CleanCart.ApplicationServices.Locator
{
    public class CannotRegisterServiceTwiceException : Exception
    {
        public CannotRegisterServiceTwiceException(Type serviceType) :
            base("Cannot register service of type '" + serviceType.FullName + "' twice.")
        {
        }
    }
}