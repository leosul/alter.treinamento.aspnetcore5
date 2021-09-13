using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Models;
using alter.treinamento.data.Context;

namespace alter.treinamento.data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AlterDbContext _context;

        public ProductRepository(AlterDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
