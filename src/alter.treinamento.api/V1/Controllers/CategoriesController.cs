using alter.treinamento.api.Controllers;
using alter.treinamento.api.Extensions;
using alter.treinamento.api.ViewModel;
using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Models;
using AutoMapper;
using Canducci.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace alter.treinamento.api.V1.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoriesController : MainController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(INotificator notificator,
                                    IUser user,
                                    ICategoryRepository categoryRepository,
                                    ICategoryService categoryService,
                                    IMapper mapper) : base(notificator, user)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetAll(int? pageSize, int? pageNumber)
        {
            var categories = await _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAll())
                .ToPaginatedRestAsync(pageNumber ?? 1, pageSize ?? 5);

            var paginationViewModel = new PaginationViewModel()
            {
                PageCount = categories.PageCount,
                TotalItemCount = categories.TotalItemCount,
                PageNumber = categories.PageNumber,
                PageSize = categories.PageSize,
                HasPreviousPage = categories.HasPreviousPage,
                HasNextPage = categories.HasNextPage,
                IsFirstPage = categories.IsFirstPage,
                IsLastPage = categories.IsLastPage,
                FirstItemOnPage = categories.FirstItemOnPage,
                LastItemOnPage = categories.LastItemOnPage
            };

            var paginationItem = new PaginationItems<CategoryViewModel>()
            {
                Pagination = paginationViewModel,
                Items = categories.Items
            };

            return CustomResponse(paginationItem);
        }

        [ClaimsAuthorize("Category", "Get")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryViewModel>> GetById(Guid id)
        {
            var category = await _categoryRepository.GetbyId(id);

            if (category == null) return CustomResponse(NotFound());

            return CustomResponse(_mapper.Map<CategoryViewModel>(category));
        }

        [ClaimsAuthorize("Category", "Add")]
        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> Add(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _categoryService.Add(_mapper.Map<Category>(categoryViewModel));

            return CustomResponse(categoryViewModel);
        }

        [ClaimsAuthorize("Category", "Update")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryViewModel>> Update(Guid id, CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (await GetCategoryById(id) == null)
                return CustomResponse(NotFound());

            return CustomResponse(await _categoryService.Update(_mapper.Map<Category>(categoryViewModel)));
        }

        [ClaimsAuthorize("Category", "Remove")]
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
