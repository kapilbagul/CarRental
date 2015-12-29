using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using CarRental.Business.Entities;
using Core.Common.Exceptions;

namespace CarRental.Business.Contracts.Service_Contracts
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Account GetCustomerAccountInfo(string loginEmail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(NotFoundException))]
        void UpdateCustomerAccountInfo(Account account);

    }
}
