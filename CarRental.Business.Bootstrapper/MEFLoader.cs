using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using CarRental.Data.Data_Repositories;
using CarRental.Data.Contracts;
using CarRental.Business.Contracts;
namespace CarRental.Business.Bootstrapper
{
   public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CarRentalEngine).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(AccountRepository).Assembly));
             CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }
    }
}
