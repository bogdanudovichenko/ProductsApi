using ProductsApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsApi.Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(Guid id);
        Task<string> UpdateProductAsync(Guid id, Product product);
        Task<Guid> CreateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}