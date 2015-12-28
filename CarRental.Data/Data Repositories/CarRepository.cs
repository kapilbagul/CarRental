using CarRental.Business.Entities;
using CarRental.Data.Contracts.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;


namespace CarRental.Data.Data_Repositories
{
    [Export(typeof(ICarRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CarRepository : DataRepositoryBase<Car>, ICarRepository
    {
        protected override Car AddEntity(CarRentalContext entityContext, Car entity)
        {
            return entityContext.CarSet.Add(entity);
        }

        protected override IEnumerable<Car> GetEntities(CarRentalContext entityContext)
        {
            return (from c in entityContext.CarSet
                    select c).ToList();
        }

        protected override Car GetEntity(CarRentalContext entityContext, int id)
        {
            var query = from c in entityContext.CarSet
                        where c.CarId == id
                        select c;
            return query.FirstOrDefault();
        }

        protected override Car UpdateEntity(CarRentalContext entityContext, Car entity)
        {
            var query = from c in entityContext.CarSet
                        where c.CarId == entity.CarId
                        select c;
            return query.FirstOrDefault();
        }
    }
}
