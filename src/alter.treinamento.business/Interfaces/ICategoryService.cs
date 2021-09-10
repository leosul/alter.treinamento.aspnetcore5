using alter.treinamento.business.Models;
using System;
using System.Threading.Tasks;

namespace alter.treinamento.business.Interfaces
{
    public interface ICategoryService
    {
        Task Add(Category category);
        Task Update(Category category);
        Task Remove(Guid id);
        void SetIsActive(bool isActive);
    }
}
