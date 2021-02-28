using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using ProductsApi.Models;

namespace ProductsApi.Repositories
{
    public class ProductsRepository
    {
        private const string FileName = "products.json";

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await ReadAllProductsAsync();
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            var products = await ReadAllProductsAsync();
            return products.FirstOrDefault(product => product.Id == id);
        }

        public async Task<Guid> CreateProductAsync(Product product)
        {
            product.Id = Guid.NewGuid();

            var products = await ReadAllProductsAsync();
            var result = products.Concat(new [] { product }).ToList();
            await WriteAllProductsAsync(result);

            return product.Id;
        }

        public async Task<string> UpdateProductAsync(Guid id, Product product)
        {
            var products = await ReadAllProductsAsync();

            var existingProduct = products.SingleOrDefault(product => product.Id == id);
            if(existingProduct == null)
            {
                return $"Product {id} ({product.Name}) not found.";
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.PrimaryImageUrl = product.PrimaryImageUrl;

            await WriteAllProductsAsync(products);
            return string.Empty;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var products = await ReadAllProductsAsync();
            var result = products.Where(product => product.Id != id).ToList();
            await WriteAllProductsAsync(result);
        }

        private async Task<IEnumerable<Product>> ReadAllProductsAsync()
        {
            if(!File.Exists(FileName))
            {
                return new List<Product>();
            }

            var json = await File.ReadAllTextAsync(FileName);
            if(string.IsNullOrEmpty(json))
            {
                return new List<Product>();
            }

            return JsonSerializer.Deserialize<IEnumerable<Product>>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }       

        private async Task WriteAllProductsAsync(IEnumerable<Product> products)
        {
            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            await File.WriteAllTextAsync(FileName, json);
        }
    }
}