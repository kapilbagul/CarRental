using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Web.Models
{
    public class CarReserveModel
    {
        public int Car { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}