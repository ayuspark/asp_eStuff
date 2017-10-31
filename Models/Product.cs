using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp_ecommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Qty { get; set; }
        public string Desc { get; set; }
        public string ApplicationUserEmail { get; set; }
        public DateTime Created { get; set; }
    }
}