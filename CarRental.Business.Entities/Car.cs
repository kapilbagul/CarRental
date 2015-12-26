using System;
using System.Runtime.Serialization;
using Core.Common;
using Core.Common.Contracts;

namespace CarRental.Business.Entities
{
    [DataContract]
    public class Car: EntityBase, IIdentifiableEntity, IAccountOwnedEntity
    {
        [DataMember]
        public int CarId { get; set; }
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public decimal RentalPrice { get; set; }
        [DataMember]
        public bool IsCurrentlyRented { get; set; }

        public int EntityId
        {
            get
            {
                return CarId;
            }

            set
            {
                CarId = value;
            }
        }

        public int OwnerAccountId
        {
            get
            {
                return AccountId;
            }
        }
    }
}
