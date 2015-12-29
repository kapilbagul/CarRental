using CarRental.Business.Entities;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Contracts.Repository_Interfaces
{
    public interface IRentalRepository: IDataRepository<Rental>
    {
        IEnumerable<Rental> GetRentalHistoryByCar(int carid);
        Rental GetCurrentRentalByCar(int carid);
        IEnumerable<Rental> GetCurrentlyRentedCars();
        IEnumerable<CustomerRentalInfo> GetCurrentCustomerRentalInfo();
        IEnumerable<Rental> GetRentalHistoryByAccountId(int accountId);
    }
}
