using CarRental.Business.Contracts.Service_Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Entities;
using System.ServiceModel;
using System.Runtime.Serialization;
using Core.Common.Contracts;
using System.ComponentModel.Composition;
using System.Security.Permissions;
using CarRental.Common;
using CarRental.Data.Contracts.Repository;
using Core.Common.Exceptions;

namespace CarRental.Business.Managers.Managers
{
    [ServiceBehavior(ConcurrencyMode =ConcurrencyMode.Multiple,InstanceContextMode =InstanceContextMode.PerCall,ReleaseServiceInstanceOnTransactionComplete =false)]
    public class AccountManager : ManagerBase, IAccountService
    {
        public AccountManager()
        {

        }
        public AccountManager(IDataRepositoryFactory _DataRepositoryFactory)
        {
            dataRepositoryFactory = _DataRepositoryFactory;
        }
        [Import]
        IDataRepositoryFactory dataRepositoryFactory;

        [PrincipalPermission(SecurityAction.Demand,Role = Security.CarRentalAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name =  Security.CarRentalUser)]
        public Account GetCustomerAccountInfo(string loginEmail)
        {
            return HandleFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                Account account = accountRepository.GetByLogin(loginEmail);
                if (account == null)
                {
                    NotFoundException exception = new NotFoundException(string.Format("Account not found for {0}", loginEmail));
                    throw new FaultException<NotFoundException>(exception, exception.Message);
                }

                return account;
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name =  Security.CarRentalUser)]
        [OperationBehavior(TransactionScopeRequired =true)]
        public void UpdateCustomerAccountInfo(Account account)
        {
            HandleFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                Account updatedAccount = accountRepository.Update(account);
            });
        }
    }
}
