using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using CarRental.Business.Contracts;

namespace CarRental.ServiceHost.Test
{
    [TestClass]
    public class ServiceAccessTest
    {
        [TestMethod]
        public void test_inventory_service_connection()
        {
            ChannelFactory<IInventoryService> channel = 
               new ChannelFactory<IInventoryService>("");
            
          IInventoryService proxy=  channel.CreateChannel();

          (proxy as ICommunicationObject).Open();

            channel.Close(); 

        }

        [TestMethod]
        public void test_rental_service_connection()
        {
            ChannelFactory<IRentalService> channel =
               new ChannelFactory<IRentalService>("");

            IRentalService proxy = channel.CreateChannel();

            (proxy as ICommunicationObject).Open();

            channel.Close();

        }

        [TestMethod]
        public void test_account_service_connection()
        {
            ChannelFactory<IAccountService> channel =
               new ChannelFactory<IAccountService>("");

            IAccountService proxy = channel.CreateChannel();

            (proxy as ICommunicationObject).Open();

            channel.Close();

        }
    }
}
