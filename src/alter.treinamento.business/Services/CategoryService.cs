using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Models;
using System;
using System.Threading.Tasks;

namespace alter.treinamento.business.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository,
                               INotificator notificator) : base(notificator)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> Add(Category category)
        {
            _categoryRepository.Add(category);
            return await _categoryRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Update(Category category)
        {
            _categoryRepository.Update(category);
            return await _categoryRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Remove(Guid id)
        {
            _categoryRepository.Remove(id);
            return await _categoryRepository.UnitOfWork.Commit();
        }
        public void SetIsActive(bool isActive)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _categoryRepository?.Dispose();
        }
    }
}
