using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.Client.Proxies;

namespace CarRental.Proxy.Test
{
    [TestClass]
    public class ServiceAccessTest
    {
        [TestMethod]
        public void test_inventory_client_connection()
        {
            Client.Proxies.InventoryClient proxy = new Client.Proxies.InventoryClient();
            proxy.Open();
        }

        [TestMethod]
        public void test_account_client_connection()
        {
            AccountClient proxy = new Client.Proxies.AccountClient();
            proxy.Open();
        }

        [TestMethod]
        public void test_rental_client_connection()
        {
            RentalClient proxy = new Client.Proxies.RentalClient();
            proxy.Open();
        }
    }
}
