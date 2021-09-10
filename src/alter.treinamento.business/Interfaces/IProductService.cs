using alter.treinamento.business.Models;
using System;
using System.Threading.Tasks;

namespace alter.treinamento.business.Interfaces
{
    public interface IProductService
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Remove(Guid id);
        void SetIsActive(bool isActive);
    }
}
