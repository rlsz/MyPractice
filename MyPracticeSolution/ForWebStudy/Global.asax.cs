using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ForWebStudy
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var viewEngine = ViewEngines.Engines[1] as RazorViewEngine;
            viewEngine.AreaViewLocationFormats = new[]
            {
                "~/Activitys/{2}/Views/{1}/{0}.cshtml",
                "~/Activitys/{2}/Views/{1}/{0}.vbhtml",
                "~/Activitys/{2}/Views/Shared/{0}.cshtml",
                "~/Activitys/{2}/Views/Shared/{0}.vbhtml"
            };
            viewEngine.AreaMasterLocationFormats = new[]
            {
                "~/Activitys/{2}/Views/{1}/{0}.cshtml",
                "~/Activitys/{2}/Views/{1}/{0}.vbhtml",
                "~/Activitys/{2}/Views/Shared/{0}.cshtml",
                "~/Activitys/{2}/Views/Shared/{0}.vbhtml"
            };
            viewEngine.AreaPartialViewLocationFormats = new[]
            {
                "~/Activitys/{2}/Views/{1}/{0}.cshtml",
                "~/Activitys/{2}/Views/{1}/{0}.vbhtml",
                "~/Activitys/{2}/Views/Shared/{0}.cshtml",
                "~/Activitys/{2}/Views/Shared/{0}.vbhtml"
            };
        }
    }
}
