using Microsoft.AspNetCore.Http;
using ProductsApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Models
{
    public class ProductForm
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Range(1, 999999)]
        public double Price { get; set; }

        [Required]
        [FormImageValidator]
        public IFormFile PrimaryImage { get; set; }
    }
}