using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business;


using CarRental.Business.Entities;
using System.ComponentModel.Composition;
using CarRental.Business.Common;
using Core.Common.Contracts;
using CarRental.Data.Contracts;
using CarRental.Data.Contracts.Repository;
using CarRental.Data.Contracts.Repository_Interfaces;
using Core.Common.Exceptions;

namespace CarRental.Business
{
    [Export(typeof(ICarRentalEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
   public class CarRentalEngine: ICarRentalEngine
    {
        public CarRentalEngine()
        {

        }
        [ImportingConstructor]
        public CarRentalEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }
       
        IDataRepositoryFactory _dataRepositoryFactory;
        public bool GetAvailableCarsForRental(int carid, DateTime pickupDate, DateTime returnDate, IEnumerable<Rental> rentedCars, IEnumerable<Entities.Reservation> reservedCars)
        {
            
            bool available = true;

            Reservation reservation = reservedCars.Where(item => item.CarId == carid).FirstOrDefault();
            if (reservation != null && (
                (pickupDate >= reservation.RentalDate && pickupDate <= reservation.ReturnDate) ||
                (returnDate >= reservation.RentalDate && returnDate <= reservation.ReturnDate)))
            {
                available = false;
            }

            if (available)
            {
                Rental rental = rentedCars.Where(item => item.CarId == carid).FirstOrDefault();
                if (rental != null && (pickupDate <= rental.DateDue))
                    available = false;
            }

            return available;
        }

        public Rental RentCarToCustomer(string loginEmail, int accountId, int carid, DateTime rentalDate, DateTime returnDate)
        {
            if (rentalDate > DateTime.Now)
                throw new UnableToRentForDateException(string.Format("Can not rent car for date {} yet", rentalDate.ToShortDateString()));
            
            IAccountRepository accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
            IRentalRepository rentalRepository = _dataRepositoryFactory.GetDataRepository<IRentalRepository>();

            Account account = accountRepository.GetByLogin(loginEmail);
            if (account == null)
                throw new NotFoundException(string.Format("Account not found for login {}", loginEmail));

            Rental rental = new Rental
            {
                AccountId = account.AccountId,
                CarId = carid,
                DateRented = rentalDate,
                DateReturned = returnDate
            };

            Rental savedEntity = rentalRepository.Add(rental);
            return savedEntity;

        }

        public bool IsCarCurrentlyRented(int carId)
        {
           
            IRentalRepository rentalRepository = _dataRepositoryFactory.GetDataRepository<IRentalRepository>();
            var currentRental = rentalRepository.GetCurrentRentalByCar(carId);
            if (currentRental != null)
                return true;

            return false;

        }
    }
}
