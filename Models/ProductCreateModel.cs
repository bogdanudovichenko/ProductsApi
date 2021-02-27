using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Models
{
    public class ProductCreateModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Range(1, 999999)]
        public double Price { get; set; }
    }
}