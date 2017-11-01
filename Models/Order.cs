using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp_ecommerce.Models
{
    public class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Created { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}