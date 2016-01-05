using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.Composition;
using CarRental.Web.Core;
using CarRental.Client.Contracts;
using CarRental.Client.Entities;
using CarRental.Web.Models;

namespace CarRental.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/reservation")]
    public class ReservationApiController : BaseApiController
    {
        [ImportingConstructor]
        public ReservationApiController(IInventoryService inventoryService, IRentalService rentalservice)
        {
            _inventoryService = inventoryService;
            _rentalservice = rentalservice;
        }

        IInventoryService _inventoryService;
        IRentalService _rentalservice;

        [HttpGet]
        [Route("availablecars/{pickupdate}/{returndate}")]
        public HttpResponseMessage GetAvailableCars(HttpRequestMessage request, DateTime PickupDate, DateTime ReturnDate)
        {
            return GetResponseMessage(request, () =>
            {
                
                Car[] cars = _inventoryService.GetAvailableCar(PickupDate, ReturnDate);
                return request.CreateResponse<Car[]>( HttpStatusCode.OK, cars);
                
            });
        }
        [HttpPost]
        [Route("reservecar")]
        public HttpResponseMessage ReserveCar(HttpRequestMessage request, [FromBody] CarReserveModel model)
        {
            return GetResponseMessage(request, () =>
            {
                HttpResponseMessage response;
                string username = User.Identity.Name;
               Reservation reservation= _rentalservice.MakeReservation(username, model.Car, model.PickUpDate, model.ReturnDate);
                response = request.CreateResponse<Reservation>(HttpStatusCode.OK, reservation);
                return response;
            });
        }
    }
}
