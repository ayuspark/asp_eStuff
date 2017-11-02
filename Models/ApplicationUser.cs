using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace asp_ecommerce.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            ProductsToSell = new HashSet<Product>();
        }

        public DateTime Birthday { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public ICollection<Product> ProductsToSell { get; set; }
        public virtual Customer Customer { get; set; }
    }

    public class ApplicationRole : IdentityRole<Guid>
    {
        
    }
}