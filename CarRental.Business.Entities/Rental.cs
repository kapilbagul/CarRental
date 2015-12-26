using Core.Common;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CarRental.Business.Entities
{
    [DataContract]
    public class Rental : EntityBase, IIdentifiableEntity, IAccountOwnedEntity
    {
        [DataMember]
        public int RentalId { get; set; }
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public int CarId { get; set; }
        [DataMember]
        public DateTime DateRented { get; set; }
        [DataMember]
        public DateTime DateReturned { get; set; }
        [DataMember]
        public DateTime DateDue { get; set; }
        
        public int EntityId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int OwnerAccountId
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
