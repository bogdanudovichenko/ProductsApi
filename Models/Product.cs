using System;

namespace ProductsApi.Models
{
    public record Product(string Name, double Price, string PrimaryImageUrl)
    {
        public Guid Id { get; init; }
    }
}