using CarRental.Business.Entities;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Common
{
    public interface ICarRentalEngine:IBusinessEngine
    {
        bool GetAvailableCarsForRental(int carid, DateTime pickupDate, DateTime returnDate,
           IEnumerable<Rental> rentals, IEnumerable<Reservation> reservations);

        Rental RentCarToCustomer(string loginEmail,int accountId, int carid, DateTime rentalDate, DateTime returnDate);

        bool IsCarCurrentlyRented(int carId);
    }
}
