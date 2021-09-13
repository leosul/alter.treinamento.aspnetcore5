using alter.treinamento.business.Models;
using System;
using System.Threading.Tasks;

namespace alter.treinamento.business.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> Add(Category category);
        Task<bool> Update(Category category);
        Task<bool> Remove(Guid id);
        void SetIsActive(bool isActive);
    }
}
