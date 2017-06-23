using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ForWebStudy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            //routes.MapRoute(
            //    name: "ActivitysRoute_defalut",
            //    url: "ActivityTest/{pageName}/{id}",
            //    defaults: new { controller = "ActivityTest", action = "Index", id = UrlParameter.Optional, pageName = UrlParameter.Optional },
            //    namespaces: new string[] { "ForWebStudy.Controllers" }
            //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ForWebStudy.Controllers" }
            );
            

        }
    }
}
