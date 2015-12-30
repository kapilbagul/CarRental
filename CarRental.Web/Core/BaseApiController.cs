using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Security;
using System.ServiceModel;
using System.Net;

namespace CarRental.Web.Core
{
    public class BaseApiController: ApiController
    {
        protected HttpResponseMessage GetResponseMessage(HttpRequestMessage request, Func<HttpResponseMessage> codeToExecute)
        {
            HttpResponseMessage response;
            try
            {
               response= codeToExecute.Invoke();
            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError,ex.Message);
            }
            catch (Exception ex)
            {

                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return response;
        }
    }
}