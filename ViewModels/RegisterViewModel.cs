using System;
using System.ComponentModel.DataAnnotations;

namespace asp_ecommerce.ViewModels
{
    public class RegisterViewModel
    {
        [Required, Display(Name = "First Name")]
        public string FName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string LName { get; set; }

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; }

        [Required, MaxLength(256)]
        public string UserName { get; set; }

        [Required, Compare("ConfirmPassword", ErrorMessage = "Password must match!"), DataType(DataType.Password), MinLength(4), MaxLength(50) ]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), MinLength(4), MaxLength(50)]
        public string ConfirmPassword { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}
