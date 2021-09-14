using alter.treinamento.business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace alter.treinamento.business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId);
    }
}
