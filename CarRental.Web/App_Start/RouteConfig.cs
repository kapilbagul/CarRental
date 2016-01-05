using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarRental.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           

            routes.MapRoute(
               name: "AccountRegister",
               url: "account/register",
               defaults: new { controller = "Account", action = "Register" }
           );

            routes.MapRoute(
              name: "AccountRegisterRoot",
              url: "account/register/{*catchall}",
              defaults: new { controller = "Account", action = "Register" }
          );

            routes.MapRoute(
              name: "CarReserve",
              url: "Customer/ResrveCar",
              defaults: new { controller = "Customer", action = "ReserveCar" }
          );

            routes.MapRoute(
              name: "CarReserveRoot",
              url: "Customer/ResrveCar/{*catchall}",
              defaults: new { controller = "Customer", action = "ReserveCar" }
          );

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
