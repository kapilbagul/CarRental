using System.Collections.Generic;
using System.Linq;
using CarRental.Business.Entities;
using CarRental.Data.Contracts.Repository;
using System.ComponentModel.Composition;

namespace CarRental.Data.Data_Repositories
{
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AccountRepository : DataRepositoryBase<Account>, IAccountRepository
    {
        protected override Account AddEntity(CarRentalContext entityContext, Account entity)
        {
            return entityContext.AccountSet.Add(entity);
        }

        protected override IEnumerable<Account> GetEntities(CarRentalContext entityContext)
        {
            return from e in entityContext.AccountSet
                   select e;
        }

        protected override Account GetEntity(CarRentalContext entityContext, int id)
        {
            var query = from e in entityContext.AccountSet
                        where e.AccountId == id
                        select e;

            var results = query.FirstOrDefault();
            return results;

        }

        protected override Account UpdateEntity(CarRentalContext entityContext, Account entity)
        {
            return (from e in entityContext.AccountSet
                    where e.AccountId == entity.AccountId
                    select e).FirstOrDefault();
        }

        public Account GetByLogin(string login)
        {
            using(CarRentalContext dbContext = new CarRentalContext())
            {
                return (from e in dbContext.AccountSet
                        where e.LoginEmail == login
                        select e).SingleOrDefault();
                    
            }
        }

    }
}
