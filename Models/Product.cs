using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_ecommerce.Models
{
    public class Product
    {
        public Product()
        {
            OrderProducts = new List<OrderProduct>();   
        }

        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Qty { get; set; }
        public string Desc { get; set; }

        [ForeignKey("ApplicationUser")]
        public int ApplicationUserId { get; set; }
        public string ApplicationUserName { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime Created_date_by_seller { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}