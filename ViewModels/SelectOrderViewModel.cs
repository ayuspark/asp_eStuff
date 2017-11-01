using System;
using System.ComponentModel.DataAnnotations;

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
        public int OrderId { get; set; }
    }
}
