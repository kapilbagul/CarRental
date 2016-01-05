using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarRental.Business.Entities;
using CarRental.Data.Contracts.Repository_Interfaces;
using Core.Common.Contracts;
using CarRental.Business.Managers.Managers;
using System.Security.Principal;
using System.Threading;
using Core.Common;
using System.ComponentModel.Composition;
using CarRental.Business.Bootstrapper;
using System.Collections.Generic;
using CarRental.Business.Common;
using CarRental.Data.Contracts;

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

            ObjectBase.Container = MEFLoader.Init();
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
        [TestMethod]
        public void GetAvailableCars()
        {
           
           // FactoryRepositoryTestClass repository = new FactoryRepositoryTestClass();
            IDataRepositoryFactory fact = new DataRepositoryFactory();
            ICarRepository car =  fact.GetDataRepository<ICarRepository>();

            IBusinessEngineFactory engine = new BusinessEngineFactory();
           ICarRentalEngine carEngine =  engine.GetBusinessEngineFactory<ICarRentalEngine>();
            InventoryManager mgr = new InventoryManager(engine);
        Car[] cars=    mgr.GetAvailableCar(new DateTime(2016, 01, 04), new DateTime(2016, 01, 07));

            //IEnumerable<Car> result = repository.GetAvailableCars();
        }
    }

    public class FactoryRepositoryTestClass
    {
        public FactoryRepositoryTestClass()
        {
            // ObjectBase.Container.SatisfyImportsOnce(this);
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public FactoryRepositoryTestClass(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        public FactoryRepositoryTestClass(IBusinessEngineFactory businessEngineFactory)
        {
            _businessEngineFactory = businessEngineFactory;
        }

        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;
        [Import]
        IBusinessEngineFactory _businessEngineFactory;

        public IEnumerable<Car> GetAvailableCars()
        {

            
            InventoryManager manager = new InventoryManager();
            return manager.GetAvailableCar(new DateTime(2016,01,04),new DateTime(2016, 01, 07));
        }
    }
}
