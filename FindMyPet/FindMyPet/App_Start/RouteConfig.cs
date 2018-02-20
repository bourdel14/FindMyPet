using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FindMyPet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CreateAnimal",
                url: "{controller}/{action}",
                defaults: new { controller = "Animals", action = "Create"}
            );

            routes.MapRoute(
                name: "Login",
                url: "Login/Index",
                defaults: new { controller = "Utilisateurs", action = "Login" }
            );
        }
    }
}
