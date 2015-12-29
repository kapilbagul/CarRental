using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CarRental.Client.Entities;

using Core.Common.Exceptions;
using Core.Common.Contracts;

namespace CarRental.Client.Contracts
{
    [ServiceContract]
    public interface IInventoryService:IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Car GetCar(int carid);

        [OperationContract]
        Car[] GetAllCar();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Car UpdateCar(Car car);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCar(int carid);

        [OperationContract]
        Car[] GetAvailableCar(DateTime pickupDate, DateTime returnDate);
    }
}
