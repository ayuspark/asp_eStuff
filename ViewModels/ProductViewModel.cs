using System;
using System.ComponentModel.DataAnnotations;

namespace asp_ecommerce.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "Stuff name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Stuff IMG URL")]
        public string URL { get; set; }

        [Required]
        [Range(1, 10)]
        [Display(Name = "Quantity")]
        public int Qty { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Desc { get; set; }
    }
}
