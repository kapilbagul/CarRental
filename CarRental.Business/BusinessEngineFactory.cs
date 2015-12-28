using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using System.ComponentModel.Composition;
using Core.Common;

namespace CarRental.Business
{
    public class BusinessEngineFactory : IBusinessEngineFactory
    {
        public T GetBusinessEngineFactory<T>() where T : IBusinessEngine
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}
