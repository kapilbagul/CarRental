using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business;
using CarRental.Client.Entities;

using CarRental.Business.Entities;
using System.ComponentModel.Composition;
using CarRental.Business.Common;
using Core.Common.Contracts;

namespace CarRental.Business
{
    [Export(typeof(ICarRentalEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
   public class CarRentalEngine: ICarRentalEngine
    {
        public bool GetAvailableCarsForRental(int carid, DateTime pickupDate, DateTime returnDate, IEnumerable<Entities.Rental> rentals, IEnumerable<Entities.Reservation> reservations)
        {
            bool IsAvailable = true;

            Entities.Reservation reservation = reservations.Where(item => item.CarId == carid).FirstOrDefault();
            if (reservations != null && (pickupDate >= reservation.RentalDate && returnDate <= reservation.ReturnDate) ||
                (returnDate >= reservation.ReturnDate && returnDate <= reservation.ReturnDate))
            {
                IsAvailable = false;

            }

            if (IsAvailable)
            {
                Entities.Rental rental = rentals.Where(item => item.CarId == carid).FirstOrDefault();
                if (rental != null && pickupDate < rental.DateDue)
                    IsAvailable = false;
            }
            return IsAvailable;
        }

        
    }
}
