using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Read",
                url: "blog/post/{id}",
                defaults: new { controller="Home", action = "Read", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                 name: "Delete",
                 url: "cms/delete/{id}",
                 defaults: new { controller = "Cms", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "Edit",
                 url: "cms/edit/{id}",
                 defaults: new { controller = "Cms", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
