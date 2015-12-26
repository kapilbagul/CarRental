using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
       
        public int AccountId { get; set; }
       
        public int CarId { get; set; }
       
        public DateTime RentalDate { get; set; }
       
        public DateTime ReturnDate { get; set; }
    }
}
