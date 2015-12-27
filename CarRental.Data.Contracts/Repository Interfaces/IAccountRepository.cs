using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using CarRental.Business.Entities;

namespace CarRental.Data.Contracts.Repository
{
    public interface IAccountRepository: IDataRepository<Account>
    {
        Account GetByLogin(string login);
    }
}
