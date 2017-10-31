using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp_ecommerce.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public int CustomerId { get; set; }
        public DateTime Created { get; set; }
        public string ApplicationUserEmail { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}