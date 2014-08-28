using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PPP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SiteRoot",
                url: "",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                name: "Overview",
                url: "Overview/{id}",
                defaults: new { controller = "Shared", action = "Overview", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Signup",
                url: "Signup/{id}",
                defaults: new { controller = "Shared", action = "Signup", id = UrlParameter.Optional }
                );
           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           
        }
    }
}
