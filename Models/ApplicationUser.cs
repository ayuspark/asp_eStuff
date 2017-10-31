using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace asp_ecommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ProductsToSell = new HashSet<Product>();
        }
        public DateTime Birthday { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public ICollection<Product> ProductsToSell { get; set; }
    }
}