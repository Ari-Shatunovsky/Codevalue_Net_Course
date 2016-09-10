using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace ShoppingCartWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
                        var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            //config.SuppressDefaultHostAuthentication();
//            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ByCategory",
                routeTemplate: "api/products/category/{id}",
                defaults: new { controller = "products", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "RandomCarts",
                routeTemplate: "api/products/randomcarts",
                defaults: new { controller = "products" }
            );

            config.Routes.MapHttpRoute(
                name: "Carts",
                routeTemplate: "api/products/carts",
                defaults: new { controller = "products" }
            );

            config.Routes.MapHttpRoute(
                name: "Cart",
                routeTemplate: "api/products/cart",
                defaults: new { controller = "products" }
            );

            config.Routes.MapHttpRoute(
                name: "SimilarProducts",
                routeTemplate: "api/products/similar",
                defaults: new { controller = "products" }
            );

            config.Routes.MapHttpRoute(
                name: "ConnectProducts",
                routeTemplate: "api/products/connectproducts",
                defaults: new { controller = "products" }
            );

            config.Routes.MapHttpRoute(
                name: "Search",
                routeTemplate: "api/products/search/{shopId}",
                defaults: new { controller = "products" }
            );

            //            config.Routes.MapHttpRoute(
            //                name: "Categories",
            //                routeTemplate: "api/products/{id}",
            //                defaults: new { id = RouteParameter.Optional }
            //            );

        }
    }
}
