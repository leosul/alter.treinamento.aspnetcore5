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
    public class CategoriesController : MainController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(INotificator notificator,
                                    ICategoryRepository categoryRepository,
                                    ICategoryService categoryService,
                                    IMapper mapper) : base(notificator)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAll()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryViewModel>> GetById(Guid id)
        {
            var category = await _categoryRepository.GetbyId(id);

            if (category == null) return CustomResponse(NotFound());

            return CustomResponse(_mapper.Map<CategoryViewModel>(category));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> Add(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _categoryService.Add(_mapper.Map<Category>(categoryViewModel));

            return CustomResponse(categoryViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryViewModel>> Update(Guid id, CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (await GetCategoryById(id) == null)
                return CustomResponse(NotFound());

            return CustomResponse(await _categoryService.Update(_mapper.Map<Category>(categoryViewModel)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Category>> Remove(Guid id)
        {
            if (await GetCategoryById(id) == null)
                return CustomResponse(NotFound());

            return CustomResponse(await _categoryService.Remove(id));
        }

        private async Task<CategoryViewModel> GetCategoryById(Guid id)
        {
            return _mapper.Map<CategoryViewModel>(await _categoryRepository.GetbyId(id));
        }
    }
}
