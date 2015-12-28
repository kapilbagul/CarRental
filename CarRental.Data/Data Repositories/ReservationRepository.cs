using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Entities;
using CarRental.Data.Contracts.Repository_Interfaces;
using System.ComponentModel.Composition;
using CarRental.Data.Contracts;

namespace CarRental.Data.Data_Repositories
{
    [Export(typeof(IReservationRepository))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ReservationRepository : DataRepositoryBase<Reservation>, 
        IReservationRepository
    {
        public IEnumerable<CustomerReservationInfo> GetCurrentCustomerReservationInfo()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var query = from r in context.ReservationSet
                            join a in context.AccountSet on r.AccountId equals a.AccountId
                            join c in context.CarSet on r.CarId equals c.CarId
                            select new CustomerReservationInfo
                            {
                                Customer = a,
                                Reservation = r,
                                Car = c
                            };
                return query.ToList().ToArray();
            }
        }

        public IEnumerable<CustomerReservationInfo> GetCustomerOpenReservationInfo(int accountId)
        {
            using(CarRentalContext context = new CarRentalContext())
            {
                var query = from r in context.ReservationSet
                            join a in context.AccountSet on r.AccountId equals a.AccountId
                            join c in context.CarSet on r.CarId equals c.CarId
                            where r.AccountId == accountId
                            select new CustomerReservationInfo
                            {
                                Reservation = r,
                                Customer = a,
                                Car = c
                            };

                return query.ToList().ToArray();
            }
        }

        public IEnumerable<Reservation> GetReservationsByPickupDate(DateTime pickupDate)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var query = from r in context.ReservationSet
                            where r.RentalDate < pickupDate
                            select r;
                return query.ToList().ToArray();
            }
        }

        public IEnumerable<Reservation> GetReservedCars()
        {
            using ( CarRentalContext context = new CarRentalContext())
            {
                var query = from r in context.ReservationSet
                            where r.ReturnDate == null
                            select r;
                return query.ToList().ToArray();
            }
        }

        protected override Reservation AddEntity(CarRentalContext entityContext, Reservation entity)
        {
            return entityContext.ReservationSet.Add(entity);
        }

        protected override IEnumerable<Reservation> GetEntities(CarRentalContext entityContext)
        {
            return (from e in entityContext.ReservationSet
                    select e);
        }

        protected override Reservation GetEntity(CarRentalContext entityContext, int id)
        {
            var query =  (from e in entityContext.ReservationSet
                    where e.ReservationId == id
                    select e);

            return query.FirstOrDefault();
        }

        protected override Reservation UpdateEntity(CarRentalContext entityContext, Reservation entity)
        {
            var query =  (from e in entityContext.ReservationSet
                    where e.ReservationId == entity.ReservationId
                    select e);

            return query.FirstOrDefault();
        }
    }
}
