using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities
{
    public class Car 
    {
        public int CarId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public decimal RentalPrice { get; set; }
        public bool IsCurrentlyRented { get; set; }
    }
}
