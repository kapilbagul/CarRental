using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CarRental.Client.Contracts;
using CarRental.Client.Entities;
using System.ComponentModel.Composition;
using Core.Common.ServiceModel;

namespace CarRental.Client.Proxies
{
    [Export(typeof(IRentalService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RentalClient : UserClientBase<IRentalService>, IRentalService
    {
        public void ExecuteRentalFromReservation(int reservationId)
        {
            Channel.ExecuteRentalFromReservation(reservationId);
        }

        public IEnumerable<Rental> GetCarRentalHistory(string loginEmail)
        {
           return Channel.GetCarRentalHistory(loginEmail);
        }

        public Reservation GetReservation(int reservationId)
        {
            return Channel.GetReservation(reservationId);
        }

        public bool IsCarCurrentlyRented(int carid)
        {
            return Channel.IsCarCurrentlyRented(carid);
        }

        public Rental RentCarToCustomer(string loginEmail, int carid, DateTime rentalDate, DateTime dueDate)
        {
            return Channel.RentCarToCustomer(loginEmail, carid, rentalDate, dueDate);
        }

        public Reservation MakeReservation(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate)
        {
            return Channel.MakeReservation(loginEmail, carId, rentalDate, returnDate);
        }
    }
}
