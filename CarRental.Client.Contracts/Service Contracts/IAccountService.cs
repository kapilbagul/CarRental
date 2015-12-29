using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using CarRental.Client.Entities;
using Core.Common.Exceptions;
using Core.Common.Contracts;

namespace CarRental.Client.Contracts
{
    [ServiceContract]
    public interface IAccountService: IServiceContract
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
