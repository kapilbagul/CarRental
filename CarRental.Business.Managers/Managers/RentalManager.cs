using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Contracts;
using CarRental.Business.Entities;
using System.ServiceModel;
using CarRental.Data.Contracts.Repository_Interfaces;
using System.ComponentModel.Composition;
using Core.Common.Contracts;
using CarRental.Data.Data_Repositories;
using CarRental.Data.Contracts.Repository;
using Core.Common.Exceptions;
using System.Security.Permissions;
using CarRental.Common;
using CarRental.Business.Common;

namespace CarRental.Business.Managers
{
    [Export(typeof(IRentalService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.PerCall,ConcurrencyMode =ConcurrencyMode.Multiple,ReleaseServiceInstanceOnTransactionComplete =false)]
    public class RentalManager : ManagerBase, IRentalService
    {
        public RentalManager()
        {

        }
        public RentalManager(IDataRepositoryFactory DataRepositoryFactory)
        {
            _DataRepositoryFactory = DataRepositoryFactory;
        }
        public RentalManager(IBusinessEngineFactory BusinessEngineFactory)
        {
            _BusinessEngineFactory = BusinessEngineFactory;
        }
        protected override Account LoadAuthorizationAccount(string loginName)
        {
            IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
            Account authAccount = accountRepository.GetByLogin(loginName);
            if (authAccount == null)
            {
                NotFoundException exception = new NotFoundException(string.Format("Account not found for login {0}", loginName));
                throw new  FaultException<NotFoundException>(exception);
            }
               

            return authAccount;
        }
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;
        [Import]
        IBusinessEngineFactory _BusinessEngineFactory;

        [PrincipalPermission(SecurityAction.Demand,Role =Security.CarRentalAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalUser)]
        public IEnumerable<Rental> GetCarRentalHistory(string loginEmail)
        {
            return HandleFaultHandledOperation(() =>
            {
                IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                Account account = accountRepository.GetByLogin(loginEmail);
                if (account == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("User with email Id {0} not found ", loginEmail));
                    throw new FaultException<NotFoundException>(ex,ex.Message);
                }
                ValidateAuthorization(account);
                IEnumerable<Rental> rentalHistory = rentalRepository.GetRentalHistoryByAccountId(account.AccountId);
                return rentalHistory;
            });
        }

        public bool IsCarCurrentlyRented(int carid)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand,Role =Security.CarRentalAdmin)]
        public void ExecuteRentalFromReservation(int reservationId)
        {
            HandleFaultHandledOperation(() =>
            {
                IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                ICarRentalEngine rentalEngine = _BusinessEngineFactory.GetBusinessEngineFactory<ICarRentalEngine>();
                
                Reservation reservation = reservationRepository.Get(reservationId);
                if(reservation == null)
                {
                    NotFoundException exception = new NotFoundException(string.Format("Reservation not found for {0}", reservationId));
                    throw new FaultException<NotFoundException>(exception, exception.Message);
                }

                Account account = accountRepository.Get(reservation.AccountId);
                if (account == null)
                {
                    NotFoundException exception = new NotFoundException(string.Format("Account not found for {0}", reservation.AccountId));
                    throw new FaultException<NotFoundException>(exception, exception.Message);
                }

                try
                {
                    Rental rental = rentalEngine.RentCarToCustomer(account.LoginEmail, account.AccountId, reservation.CarId, reservation.RentalDate, reservation.ReturnDate);
                }
                catch (UnableToRentForDateException ex)
                {
                    throw new FaultException<UnableToRentForDateException>(ex, ex.Message);
                }
                catch(NotFoundException ex)
                {
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

            });
        }

        public Reservation GetReservation(int reservationId)
        {
            return HandleFaultHandledOperation(() =>
            {
                IReservationRepository reservationRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();
                Reservation reservation = reservationRepository.Get(reservationId);
                if (reservation == null)
                {
                    NotFoundException exception = new NotFoundException(string.Format("Reservation {0} is not found", reservationId.ToString()));
                    throw new FaultException<NotFoundException>(exception,exception.Message);
                }
                return reservation;
            });
        }

        [OperationBehavior(TransactionScopeRequired =true)]
        [PrincipalPermission(SecurityAction.Demand,Role =Security.CarRentalAdmin)]
        public Rental RentCarToCustomer(string loginEmail, int carid,  DateTime rentalDate, DateTime dueDate)
        {
            return HandleFaultHandledOperation(() =>
            {
                ICarRentalEngine rentalEngine = _BusinessEngineFactory.GetBusinessEngineFactory<ICarRentalEngine>();
                IAccountRepository accountRepository = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();
                Account account = accountRepository.GetByLogin(loginEmail);
                if (account == null)
                {
                    NotFoundException exception = new NotFoundException(string.Format("Account not found for login {0}", loginEmail));
                    throw new FaultException<NotFoundException>(exception, exception.Message);
                }
                Rental rental = rentalEngine.RentCarToCustomer(loginEmail,account.AccountId,carid,rentalDate,dueDate);
                return rental;
            });
        }
    }
}
