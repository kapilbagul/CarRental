using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Business.Entities;
using Moq;
using CarRental.Data.Contracts.Repository_Interfaces;
using Core.Common.Contracts;

namespace CarRental.Business.Tests
{
    [TestClass]
    public class CarEngineTest
    {
        [TestMethod]
        public void IsCarCurrentlyRented_any_account()
        {
            Rental rental = new Rental
            {
                CarId = 1
            };
            Mock<IRentalRepository> mockRentalRepository = new Mock<IRentalRepository>();
            //            mockRentalRepository.Setup(o=>o.GetCurrentRentalByCar(1)).
            mockRentalRepository.Setup(obj => obj.GetCurrentRentalByCar(1)).Returns(rental);

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(obj => obj.GetDataRepository<IRentalRepository>()).Returns(mockRentalRepository.Object);

            CarRentalEngine engine = new CarRentalEngine(mockDataRepositoryFactory.Object);
           var try1 = engine.IsCarCurrentlyRented(1);
            var try2 = engine.IsCarCurrentlyRented(2);

            Assert.IsTrue(try1 == true);

            Assert.IsTrue(try2 == true);
        }
    }
}
