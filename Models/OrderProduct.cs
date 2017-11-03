using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp_ecommerce.Models
{
    public class OrderProduct
    {
        [Key]
        public int OrderProductId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime Ordered_date { get; set; }
        public int QtyOrdered { get; set; }
    }
}
