using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace tltjr
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "RSS",
                "rss",
                new {controller = "Blog", action = "Rss"});

            routes.MapRoute(
                "Post",
                "{year}/{month}/{day}/{slug}",
                new {controller = "Blog", action = "Post" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}