using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Entities
{
    
    public class Account 
    {
        public int AccountId { get; set; }
        
        public string LoginEmail { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Address { get; set; }
        
        public string City { get; set; }
        
        public int ZipCode { get; set; }
        
        public string CreditCard { get; set; }
        
        public string ExpDate { get; set; }
    }
}
