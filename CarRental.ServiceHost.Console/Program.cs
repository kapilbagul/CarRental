using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM = System.ServiceModel;
using CarRental.Business.Managers;
using CarRental.Business.Managers.Managers;
using CarRental.Business.Bootstrapper;
using CarRental.Business.Entities;
using Core.Common;
using System.Security.Principal;
using System.Threading;

namespace CarRental.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericPrincipal principal = new GenericPrincipal(
                new GenericIdentity("kapilb"), new string[] { "Administrators", "CarRentalAdmin" });
            Thread.CurrentPrincipal = principal;

            ObjectBase.Container = MEFLoader.Init();


            Console.WriteLine("Starting up the service...");
            Console.WriteLine("");

            SM.ServiceHost InventoryManagerHost = new SM.ServiceHost(typeof(InventoryManager));
            SM.ServiceHost AccountManagerHost = new SM.ServiceHost(typeof(AccountManager));
            SM.ServiceHost RentalManagerHost = new SM.ServiceHost(typeof(RentalManager));

            ServiceHost(InventoryManagerHost, "Inventory Manager");
            ServiceHost(AccountManagerHost, "Account Manager");
            ServiceHost(RentalManagerHost, "Rental Manager");


            Console.WriteLine("");
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();

            StopService(InventoryManagerHost, "Inventory Manager");
            StopService(AccountManagerHost, "Account Manager");
            StopService(RentalManagerHost, "Rental Manager");
        }

        static void ServiceHost(SM.ServiceHost host, string description)
        {
            host.Open();
            Console.WriteLine("Service {0} started",description);

            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine(string.Format("Listening on endpoints"));
                Console.WriteLine(string.Format("Address : {0}",endpoint.Address.Uri));
                Console.WriteLine(string.Format("Binding : {0}", endpoint.Binding.Name));
                Console.WriteLine(string.Format("Binding : {0}", endpoint.Contract.Name));
            }
            Console.WriteLine();

        }

        static void StopService(SM.ServiceHost host,string description)
        {
            
            host.Close();
            Console.WriteLine(string.Format("Service {0} is stopped", description));

        }
    }
}
