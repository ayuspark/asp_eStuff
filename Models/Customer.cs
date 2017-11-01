using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public string ApplicationUserEmail { get; set; }
        public virtual ApplicationUser User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}