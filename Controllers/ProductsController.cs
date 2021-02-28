using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models;
using ProductsApi.Repositories;
using ProductsApi.Services;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsRepository _repository;
        private readonly FileService _fileService;

        public ProductsController(
            ProductsRepository repository,
            FileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.GetProductsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _repository.GetProductAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductForm productForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var primaryImageUrl = await _fileService.UploadFileAsync(productForm.PrimaryImage);

            var product = new Product
            {
                Name = productForm.Name,
                Price = productForm.Price,
                PrimaryImageUrl = primaryImageUrl
            };
            
            var id = await _repository.CreateProductAsync(product);

            return Ok(id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] ProductForm productForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var primaryImageUrl = await _fileService.UploadFileAsync(productForm.PrimaryImage);

            var product = new Product
            {
                Name = productForm.Name,
                Price = productForm.Price,
                PrimaryImageUrl = primaryImageUrl
            };

            var error = await _repository.UpdateProductAsync(id, product);
            if (string.IsNullOrEmpty(error))
            {
                return Ok();
            }

            return BadRequest(error);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteProductAsync(id);
            return Ok();
        }
    }
}