using Microsoft.AspNetCore.Http;
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
        public IFormFile PrimaryImage { get; set; }
    }
}