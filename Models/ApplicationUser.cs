using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace asp_ecommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthday { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public List<RestoReview> Reviews { get; set; }

        public ApplicationUser()
        {
            Reviews = new List<RestoReview>();
        }
    }
}