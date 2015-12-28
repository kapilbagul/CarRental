using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Core.Common;
using CarRental.Business.Bootstrapper;
using CarRental.Business.Entities;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using CarRental.Data.Contracts.Repository_Interfaces;
using System.Collections.Generic;
using CarRental.Data;
using CarRental.Data.Data_Repositories;
using Core.Common.Contracts;
using CarRental.Data.Contracts;
using CarRental.Data.Contracts.Repository;


namespace CarRental.Data.Test
{
    [TestClass]
    public class DataLayerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void test_repository()
        {
            RepositoryTestClass repository = new RepositoryTestClass();
            IEnumerable<Car> result = repository.GetCars();

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void test_factory_repository()
        {
            FactoryRepositoryTestClass repository = new FactoryRepositoryTestClass();
            IEnumerable<Car> result = repository.GetCars();

            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void test_repository_mock()
        {
            List<Car> cars = new List<Car>
            {
                new Car { CarId=1,Description="abc"  },
                new Car {CarId = 2, Description="abv" }
            };
            Mock<ICarRepository> mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(obj => obj.Get()).Returns(cars);

            RepositoryTestClass repository = new RepositoryTestClass(mockCarRepository.Object);
            IEnumerable<Car> result = repository.GetCars();

            Assert.IsTrue(result != null);

        }

        [TestMethod]
        public void test_factory_repository_mock()
        {
            List<Car> cars = new List<Car>
            {
                new Car { CarId=1,Description="abc"  },
                new Car {CarId = 2, Description="abv" }
            };


            Mock<IDataRepositoryFactory> mockCarRepository = new Mock<IDataRepositoryFactory>();
            mockCarRepository.Setup(obj => obj.GetDataRepository<ICarRepository>().Get()).Returns(cars);


            FactoryRepositoryTestClass repository = new FactoryRepositoryTestClass(mockCarRepository.Object);
            IEnumerable<Car> result = repository.GetCars();

            Assert.IsTrue(result != null);

        }
    }

    public class RepositoryTestClass
    {
        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryTestClass(ICarRepository carRepository)
        {
            _carRepository = carRepository;
           
        }
        [Import]
        ICarRepository _carRepository;
       

        public IEnumerable<Car> GetCars()
        {
            IEnumerable<Car> cars = _carRepository.Get();
            return cars;
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
        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        public IEnumerable<Car> GetCars()
        {
            ICarRepository carRepository = _dataRepositoryFactory.GetDataRepository<ICarRepository>();
            IEnumerable<Car> cars = carRepository.Get();
            return cars;
        }
    }
}
