using CarRental.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CarRental.Client.Entities;
using System.ComponentModel.Composition;
using Core.Common.ServiceModel;

namespace CarRental.Client.Proxies
{
    [Export(typeof(IInventoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InventoryClient : UserClientBase<IInventoryService>, IInventoryService
    {
        public void DeleteCar(int carid)
        {
            Channel.DeleteCar(carid);
        }

        public Car[] GetAllCar()
        {
            return Channel.GetAllCar();
        }

        public Car[] GetAvailableCar(DateTime pickupDate, DateTime returnDate)
        {
            return Channel.GetAvailableCar(pickupDate, returnDate);
        }

        public Car GetCar(int carid)
        {
            return Channel.GetCar(carid);
        }

        public Car UpdateCar(Car car)
        {
            return Channel.UpdateCar(car);
        }
    }
}
