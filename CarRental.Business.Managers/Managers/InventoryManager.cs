using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common;
using System.ComponentModel.Composition;
using CarRental.Business.Contracts;
using CarRental.Business.Entities;
using Core.Common.Exceptions;
using System.ServiceModel;

using CarRental.Data.Contracts.Repository_Interfaces;
using CarRental.Business.Common;
using System.Security.Permissions;
using CarRental.Common;


namespace CarRental.Business.Managers.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,ConcurrencyMode =ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete =false)]
    public class InventoryManager: ManagerBase,IInventoryService
    {
        public InventoryManager()
        {
            
        }
        public InventoryManager(IDataRepositoryFactory DataRepositoryFactory)
        {
            _DataRepositoryFactory = DataRepositoryFactory;

        }

        public InventoryManager(IBusinessEngineFactory BusinessEngineFactory)
        {
            _BusinessEngineFactory = BusinessEngineFactory;
        }
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;
        [Import]
        IBusinessEngineFactory _BusinessEngineFactory;

        [PrincipalPermission(SecurityAction.Demand,Role = Security.CarRentalAdmin)]
        [PrincipalPermission(SecurityAction.Demand,Name = Security.CarRentalUser)]
        public Car GetCar(int carid)
        {
            return HandleFaultHandledOperation(() =>
            {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                Car carEntity = carRepository.Get(carid);
                if (carEntity == null)
                {
                    NotFoundException ex =
                        new NotFoundException(string.Format("Car with CarId {} not found", carid));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return carEntity;
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Car[] GetAllCar()
        {
            return HandleFaultHandledOperation(() =>
            {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
                IEnumerable<Car> cars = carRepository.Get();
                IEnumerable<Rental> rentedCars = rentalRepository.GetCurrentlyRentedCars();

                foreach (Car car in cars)
                {
                    Rental rentedcar = rentedCars.Where(r => r.CarId == car.CarId).FirstOrDefault();
                    car.IsCurrentlyRented = (rentedcar != null);
                }

                return cars.ToArray();

            });
           
           
        }

        [OperationBehavior(TransactionScopeRequired =true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdmin)]
        public Car UpdateCar(Car car)
        {
            return HandleFaultHandledOperation(() =>
            {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                Car updatedEntity=null;
                if(car.CarId==0)
                {
                    updatedEntity = carRepository.Add(car);
                }
                else
                {
                    updatedEntity = carRepository.Update(car);
                }
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired =true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdmin)]
        
        public void DeleteCar(int carid)
        {
            HandleFaultHandledOperation(() =>
            {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                carRepository.Remove(carid);
            });
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdmin)]
        //[PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Car[] GetAvailableCar(DateTime pickupDate, DateTime returnDate)
        {
           
            return HandleFaultHandledOperation(() =>
            {
                ICarRepository carRepository = _DataRepositoryFactory.GetDataRepository<ICarRepository>();
                IRentalRepository rentalRepository = _DataRepositoryFactory.GetDataRepository<IRentalRepository>();
                IReservationRepository reserveRepository = _DataRepositoryFactory.GetDataRepository<IReservationRepository>();

                ICarRentalEngine carRentalEngine = _BusinessEngineFactory.GetBusinessEngineFactory<ICarRentalEngine>();

                List<Car> availableCar = new List<Car>();

                IEnumerable<Car> cars = carRepository.Get();
                IEnumerable<Rental> rental = rentalRepository.GetCurrentlyRentedCars();
                IEnumerable<Reservation> reservation = reserveRepository.Get();

                foreach (Car car in cars)
                {
                    if (carRentalEngine.GetAvailableCarsForRental(car.CarId,
                        pickupDate,returnDate,rental,reservation))

                        availableCar.Add(car);
                }
                return availableCar.ToArray();
            });
           
        }
    }
}
