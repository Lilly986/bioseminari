using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MSDNRoles
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Startup startup = new Startup();
            //startup.CreateRolesandUsers();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string path = HttpContext.Current.Server.MapPath("~/");
            string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\")) + @"Baza\";
            AppDomain.CurrentDomain.SetData("DataDirectory", newPath);
        }
    }
}