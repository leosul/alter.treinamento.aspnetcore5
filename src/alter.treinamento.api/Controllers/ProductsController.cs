using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace alter.treinamento.api.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public ProductsController(IProductRepository productRepository,
                                  IProductService productService)
        {
            _productRepository = productRepository;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return Ok(await _productRepository.GetAll());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            var product = await _productRepository.GetbyId(id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> add(Product product)
        {
            await _productService.Add(product);

            return Ok(product);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Product>> Update(Guid id, Product product)
        {
            return Ok(await _productService.Update(product));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Product>> Remove(Guid id)
        {
            return Ok(await _productService.Remove(id));
        }
    }
}
