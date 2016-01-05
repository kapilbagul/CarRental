using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using CarRental.Web.Core;

namespace CarRental.Web.Controllers.MVC
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("customer")]
    public class CustomerController : ViewControllerBase
    {
        [HttpGet]
        // the route is defined in RouteConfig
        public ActionResult ReserveCar()
        {

            return View();
        }
    }
}