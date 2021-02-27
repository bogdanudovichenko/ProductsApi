using System;

namespace ProductsApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string PrimaryImage { get; set; }
    }
}