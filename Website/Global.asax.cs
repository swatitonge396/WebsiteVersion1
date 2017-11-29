using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using System.Web.Http; //for web api
namespace Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
           // GlobalConfiguration.Configure(WebApiConfig.Register); //for web api to be added before mvc
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes); //for mvc
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
        }
    }
}
