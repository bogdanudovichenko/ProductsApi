using System;
using System.Collections.Generic;
using System.Net;
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
        private readonly IProductsRepository _repository;
        private readonly FileService _fileService;

        public ProductsController(
            IProductsRepository repository,
            FileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Product>))]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.GetProductsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Product))]
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
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(Guid))]
        public async Task<IActionResult> Post([FromForm] ProductForm productForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var primaryImageUrl = await _fileService.UploadFileAsync(productForm.PrimaryImage);
            var product = new Product(productForm.Name, productForm.Price, primaryImageUrl);
            
            var id = await _repository.CreateProductAsync(product);
            return StatusCode((int)HttpStatusCode.Created, id);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Put(Guid id, [FromForm] ProductForm productForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var primaryImageUrl = await _fileService.UploadFileAsync(productForm.PrimaryImage);
            var product = new Product(productForm.Name, productForm.Price, primaryImageUrl);

            var error = await _repository.UpdateProductAsync(id, product);
            if (string.IsNullOrEmpty(error))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return BadRequest(error);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteProductAsync(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}