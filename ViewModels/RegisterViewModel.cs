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

        [Required, MaxLength(256), Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, Compare("ConfirmPassword", ErrorMessage = "Password must match!"), DataType(DataType.Password), MinLength(4), MaxLength(50) ]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), MinLength(4), MaxLength(50),Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required, DataType(DataType.Date), PastDate(ErrorMessage = "Date cannot be beyond today!")]
        public DateTime Birthday { get; set; }

        public class PastDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime d = Convert.ToDateTime(value);
                return d <= DateTime.Now;
            }
        }
    }
}
