using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities
{
    public class Rental
    {
        
        public int RentalId { get; set; }
        
        public int AccountId { get; set; }
        
        public int CarId { get; set; }
        
        public DateTime DateRented { get; set; }
        
        public DateTime DateReturned { get; set; }
        
        public DateTime DateDue { get; set; }
    }
}
