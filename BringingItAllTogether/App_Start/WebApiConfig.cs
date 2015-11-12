using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BringingItAllTogether.ActionFilter;
using BringingItAllTogether.Filters;

namespace BringingItAllTogether
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new ApiAuthenticationFilter());
            config.Filters.Add(new LoggingFilterAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
