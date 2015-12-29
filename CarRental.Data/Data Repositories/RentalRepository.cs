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
    [Export(typeof(IRentalRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RentalRepository : DataRepositoryBase<Rental>, IRentalRepository
    {
        public IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var query = from r in context.RentalSet
                            where r.DateReturned == null
                            join a in context.AccountSet on r.AccountId equals a.AccountId
                            join c in context.CarSet on r.CarId equals c.CarId
                            select new CustomerRentalInfo
                            {
                                Car = c,
                                Customer = a,
                                Rental = r
                            };
                return query.ToList().ToArray();
            }
        }

        public IEnumerable<Rental> GetCurrentlyRentedCars()
        {
            using(CarRentalContext context = new CarRentalContext())
            {
                var query = from r in context.RentalSet
                            where r.DateRented != null && r.DateReturned == null
                            select r;
                return query.ToList().ToArray();
                             
            }
        }

        public Rental GetCurrentRentalByCar(int carid)
        {
            using ( CarRentalContext context = new CarRentalContext())
            {
                var query = from e in context.RentalSet
                            where e.CarId == carid && e.DateReturned == null
                            select e;
                return query.FirstOrDefault();
            }
        }

        public IEnumerable<Rental> GetRentalHistoryByCar(int carid)
        {
            using ( CarRentalContext context = new CarRentalContext())
            {
                var query = from e in context.RentalSet
                            where e.CarId == carid
                            select e;

                return query.ToList();
            }
        }

        public IEnumerable<Rental> GetRentalHistoryByAccountId(int accountId)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var query = from e in context.RentalSet
                            where e.AccountId == accountId
                            select e;

                return query.ToList();
            }
        }

        protected override Rental AddEntity(CarRentalContext entityContext, Rental entity)
        {
            return entityContext.RentalSet.Add(entity);
        }

        protected override IEnumerable<Rental> GetEntities(CarRentalContext entityContext)
        {
            return (from rental in entityContext.RentalSet
                    select rental);
        }

        protected override Rental GetEntity(CarRentalContext entityContext, int id)
        {
            var query =  (from rental in entityContext.RentalSet
                    where rental.RentalId == id
                    select rental);
            return query.FirstOrDefault();                    
        }

        protected override Rental UpdateEntity(CarRentalContext entityContext, Rental entity)
        {
            return (from e in entityContext.RentalSet
                    where e.RentalId == entity.RentalId
                    select e).FirstOrDefault();

        }
    }
}
