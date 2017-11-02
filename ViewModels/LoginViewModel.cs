using System;
using System.ComponentModel.DataAnnotations;

namespace asp_ecommerce.ViewModels
{
    public class LoginViewModel
    {
        [Required, DataType(DataType.Password), MinLength(4), MaxLength(50)]
        [Display(Name = "Password")]
        public string PasswordLogin { set; get; }

        [Required]
        [Display(Name = "Username")]
        public string UsernameLogin { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
