using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Web.Core;
using System.ComponentModel.Composition;
using CarRental.Web.Models;
using Core.Common.Contracts;
using System.ComponentModel.Composition.Hosting;

namespace CarRental.Web.Controllers.MVC
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("account")]
    public class AccountController : ViewControllerBase
    {
        [ImportingConstructor]
        public AccountController(ISecurityAdapter SecurityAdapter)
        {
            _SecurityAdapter = SecurityAdapter;
        }


        ISecurityAdapter _SecurityAdapter;

        [HttpGet]
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {

           _SecurityAdapter.Initialize();
            
            return View(new AccountLoginModel() { ReturnUrl=returnUrl}   );
        }

        [HttpGet]
        public ActionResult Register()
        {
            _SecurityAdapter.Initialize();
             return View();
        }
            
    }
}