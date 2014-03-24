using System;

namespace CleanCart.ApplicationServices.Locator
{
    [Serializable]
    public class CannotRegisterServiceTwiceException : Exception
    {
        public CannotRegisterServiceTwiceException(Type serviceType) :
            base("Cannot register service of type '" + serviceType.FullName + "' twice.")
        {
        }
    }
}