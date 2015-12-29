using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Common;
using CarRental.Client.Bootstrapper;
using CarRental.Client.Contracts;
using Core.Common.Contracts;
using CarRental.Client.Proxies;

namespace CarRental.Proxy.Test
{
    [TestClass]
    public class ProxyObtainmentTest
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();

        }
        [TestMethod]
        public void obtain_proxy_from_container_using_service_contract()
        {
            IInventoryService proxy = ObjectBase.Container.GetExportedValue<IInventoryService>();
            Assert.IsTrue(proxy is IInventoryService);
        }
        [TestMethod]
        public void obtain_proxy_from_service_factory()
        {
            IServiceFactory factory = new ServiceFactory();
            IInventoryService proxy =  factory.CreateClient<IInventoryService>();
            Assert.IsTrue(proxy is IInventoryService);
        }

        [TestMethod]
        public void obtain_proxy_from_service_factory_Proxy_from_container()
        {
            IServiceFactory factory = ObjectBase.Container.GetExportedValue<IServiceFactory>();
            IInventoryService proxy = factory.CreateClient<IInventoryService>();

            Assert.IsTrue(proxy is IInventoryService);

        }

    }
}
