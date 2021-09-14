using alter.treinamento.api.ViewModel;
using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alter.treinamento.api.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(INotificator notificator,
                                  IProductRepository productRepository,
                                  IProductService productService,
                                  IMapper mapper) : base(notificator)
        {
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> GetById(Guid id)
        {
            var product = await _productRepository.GetbyId(id);

            if (product == null) return CustomResponse(NotFound());

            return CustomResponse(_mapper.Map<ProductViewModel>(product));
        }

        [HttpGet("category/{categoryId:guid}")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProductsByCategory(Guid categoryId)
        {
            var products = await _productRepository.GetProductByCategory(categoryId);

            if (!products.Any()) return CustomResponse(NotFound());

            return CustomResponse(_mapper.Map<IEnumerable<ProductViewModel>>(products));
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _productService.Add(_mapper.Map<Product>(productViewModel));

            return CustomResponse(productViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Update(Guid id, ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (await GetProductById(id) == null) return CustomResponse(NotFound());

            return CustomResponse(await _productService.Update(_mapper.Map<Product>(productViewModel)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Remove(Guid id)
        {
            if (await GetProductById(id) == null) return CustomResponse(NotFound());

            return CustomResponse(await _productService.Remove(id));
        }

        private async Task<ProductViewModel> GetProductById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetbyId(id));
        }
    }
}
