using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class Pie
    {
        [Key]
        public int PieId { get; set; }
        [Required]
        [Display(Name = "Pie Name")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-]*$", ErrorMessage = "Pie name should contain only Characters")]
        public string Name { get; set; }
        [Display(Name = "Short Description")]
        [Required]
        [MinLength(30, ErrorMessage = "Short Description should have 30-50 Characters")]
        [MaxLength(50, ErrorMessage = "Short Description should have 30-50 Characters")]
        public string ShortDescription { get; set; }
        [Display(Name = "Long Description")]
        [Required]
        public string LongDescription { get; set; }
        [RegularExpression(@"^[-9]*$", ErrorMessage = "Price should be greater than 0")]
        [Required]
        public double Price { get; set; }
        [ValidateNever]

        public string ImageUrl { get; set; }
    }
}
