using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarRental.Business.Entities;
using CarRental.Data.Contracts.Repository_Interfaces;
using Core.Common.Contracts;
using CarRental.Business.Managers.Managers;
using System.Security.Principal;
using System.Threading;

namespace CarRental.Business.Managers.Tests
{
    [TestClass]
    public class InventoryManagerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            GenericPrincipal genericPrinciple = new GenericPrincipal(new GenericIdentity("kapilb"), new string[] { "CarRentalAdmin" });
            Thread.CurrentPrincipal = genericPrinciple;
        }
        [TestMethod]
        public void UpdateCar_add_new_car()
        {
            Car newCar = new Car();
            Car AddedCar = new Car
            {
                CarId = 1
            };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();

            mockDataRepositoryFactory.Setup(obj => obj.GetDataRepository<ICarRepository>().Add(newCar)).Returns(AddedCar);
            InventoryManager inventoryManager = new InventoryManager(mockDataRepositoryFactory.Object);
            Car car =  inventoryManager.UpdateCar(newCar);
            Assert.IsTrue(car == AddedCar);

        }
    }
}
