using alter.treinamento.business.Models;
using System;
using System.Threading.Tasks;

namespace alter.treinamento.business.Interfaces
{
    public interface IProductService
    {
        Task<bool> Add(Product product);
        Task<bool> Update(Product product);
        Task<bool> Remove(Guid id);
        void SetIsActive(bool isActive);
    }
}
