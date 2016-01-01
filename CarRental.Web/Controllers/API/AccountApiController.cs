using CarRental.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.Composition;
using CarRental.Web.Models;
using CarRental.Web.Services;
using Core.Common.Contracts;
using System.ComponentModel.Composition.Hosting;
using CarRental.Web.Helpers;
namespace CarRental.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/account")]
    public class AccountApiController : BaseApiController
    {
       
        [ImportingConstructor]
        public AccountApiController(ISecurityAdapter securityAdapter)
        {
            _securityAdapter = securityAdapter;
        }
        ISecurityAdapter _securityAdapter;
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(HttpRequestMessage request, [FromBody] AccountLoginModel accountLoginModel)
        {
            return GetResponseMessage(request, () =>
            {
                HttpResponseMessage response;
                 bool success = _securityAdapter.Login(accountLoginModel.LoginEmail,accountLoginModel.Password,accountLoginModel.RememberMe);
                if (success)
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                    
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest,"Unauthorized login");
                }
                return response;
            });
            
        }
        [HttpPost]
        [Route("register/validate1")]
        public HttpResponseMessage ValidateRegistrationStep1(HttpRequestMessage request, 
            [FromBody] AccountRegisterModel accountRegisterModel)
        {
            return GetResponseMessage(request, () =>
            {
                HttpResponseMessage response=null;
                List<string> errors = new List<string>();
                List<State> states = Helpers.States.GetStates();

                State state = states.FirstOrDefault(item => item.Name == accountRegisterModel.State);
                if (state == null)
                    errors.Add("Invalid state.");

                if (errors.Count() == 0)
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, errors.ToArray());
                }
                
                return response;
            });
            
        }

        [HttpPost]
        [Route("register/validate2")]
        public HttpResponseMessage ValidateRegistrationStep2(HttpRequestMessage request,
           [FromBody] AccountRegisterModel accountRegisterModel)
        {
            return GetResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                if (_securityAdapter.UserExists(accountRegisterModel.LoginEmail))
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, new List<string>() {"Account already register with email address" }.ToArray());
                }

                return response;
            });

        }

        [HttpPost]
        [Route("register/validate3")]
        public HttpResponseMessage ValidateRegistrationStep3(HttpRequestMessage request,
           [FromBody] AccountRegisterModel accountRegisterModel)
        {
            return GetResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                List<string> errors = new List<string>();
                if (accountRegisterModel.CreditCard.Length != 16)
                    errors.Add("Credit Card # must be  16 characters");
                

                if (errors.Count() == 0)
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                    response = request.CreateResponse(HttpStatusCode.BadRequest, errors.ToArray());

                return response;
            });

        }

        [HttpPost]
        [Route("register")]
        public HttpResponseMessage CreateAccount(HttpRequestMessage request,
           [FromBody] AccountRegisterModel accountRegisterModel)
        {
            return GetResponseMessage(request, () =>
            {
                HttpResponseMessage response = null;
                if (ValidateRegistrationStep1(request,accountRegisterModel).IsSuccessStatusCode &&
                ValidateRegistrationStep2(request, accountRegisterModel).IsSuccessStatusCode &&
                ValidateRegistrationStep3(request, accountRegisterModel).IsSuccessStatusCode)
                {
                    _securityAdapter.Register(accountRegisterModel.LoginEmail, accountRegisterModel.Password, new
                    {
                        FirstName = accountRegisterModel.FirstName,
                        LastName = accountRegisterModel.LastName,
                        Address = accountRegisterModel.Address,
                        City = accountRegisterModel.City,
                        State = accountRegisterModel.State,
                        ZipCode = accountRegisterModel.ZipCode,
                        LoginEmail=accountRegisterModel.LoginEmail,
                        Password=accountRegisterModel.Password,
                        CreditCard=accountRegisterModel.CreditCard,
                        ExpDate=accountRegisterModel.ExpDate
                    });

                    _securityAdapter.Login(accountRegisterModel.LoginEmail, accountRegisterModel.Password, false);
                    response = request.CreateResponse(HttpStatusCode.OK);

                }

                return response;
            });

        }


    }
}
