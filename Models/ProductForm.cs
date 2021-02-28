using Microsoft.AspNetCore.Http;
using ProductsApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Models
{
    public record ProductForm
    {
        [Required]
        [MinLength(2)]
        public string Name { get; init; }

        [Range(1, 999999)]
        public double Price { get; init; }

        [Required]
        [FormImageValidator]
        public IFormFile PrimaryImage { get; init; }
    }
}