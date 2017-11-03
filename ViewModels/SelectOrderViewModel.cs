using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace asp_ecommerce.ViewModels
{
    public class SelectOrderViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ProductQty { get; set; }

        [Required]
        public int OrderProductId { get; set; }

        [Required]
        [HiddenInput]
        public int OrderId { get; set; }
    }
}
