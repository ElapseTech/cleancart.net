using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CleanCart.ConfigurationContexts;

namespace CleanCart
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private DemoInMemoryContext _context;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _context = new DemoInMemoryContext();
            _context.Apply();
        }

        protected void Application_BeginRequest()
        {
            _context.BeforeRequest();
        }

        protected void Application_EndRequest()
        {
            _context.AfterRequest();
        }
    }
}