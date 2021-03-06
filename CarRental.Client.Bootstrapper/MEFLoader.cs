﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using CarRental.Client.Proxies;
using Core.Common.Contracts;

namespace CarRental.Client.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(InventoryClient).Assembly));
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(ISecurityAdapter).Assembly));

            if (catalogParts!=null)
                foreach (var part in catalogParts)
                   catalog.Catalogs.Add(part);

            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }
    }
}
