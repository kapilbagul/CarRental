using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Web.Http;
using Core.Common.Extensions;
using System.Web.Http.Dependencies;

namespace CarRental.Web.Core
{
    public class MefAPIDependencyResolver : IDependencyResolver
    {
        CompositionContainer _container;

        public MefAPIDependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.GetExportedValueByType(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetExportedValuesByType(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            
        }
    }
}