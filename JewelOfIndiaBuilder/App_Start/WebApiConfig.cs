using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JewelOfIndiaBuilder
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            //config.EnableCors();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "featureApi",
                routeTemplate: "featureApi/{controller}/{propertyId}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "UserApi",
                routeTemplate: "UserApi/{controller}/{userName}/{password}",
                defaults: new { userName = RouteParameter.Optional, password = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "detailApi",
               routeTemplate: "detailApi/{controller}/{detailItemId}/{itemType}",
               defaults: new { detailItemId = RouteParameter.Optional, itemType = RouteParameter.Optional }
           );
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
