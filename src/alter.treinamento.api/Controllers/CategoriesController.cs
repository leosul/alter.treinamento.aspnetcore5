using alter.treinamento.api.ViewModel;
using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace alter.treinamento.api.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository,
                                    ICategoryService categoryService,
                                    IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAll()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryViewModel>> GetById(Guid id)
        {
            var category = await _categoryRepository.GetbyId(id);

            if (category == null) return NotFound();

            return Ok(_mapper.Map<CategoryViewModel>(category));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> add(CategoryViewModel categoryViewModel)
        {
            await _categoryService.Add(_mapper.Map<Category>(categoryViewModel));

            return Ok(categoryViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryViewModel>> Update(Guid id, CategoryViewModel categoryViewModel)
        {
            if (GetCategoryById(id) == null)
                return NotFound();

            return Ok(await _categoryService.Update(_mapper.Map<Category>(categoryViewModel)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Category>> Remove(Guid id)
        {
            if (GetCategoryById(id) == null)
                return NotFound();

            return Ok(await _categoryService.Remove(id));
        }

        private async Task<CategoryViewModel> GetCategoryById(Guid id)
        {
            return _mapper.Map<CategoryViewModel>(await _categoryRepository.GetbyId(id));
        }
    }
}
