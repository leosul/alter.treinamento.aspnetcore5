using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Models;
using alter.treinamento.data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alter.treinamento.data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AlterDbContext _context;

        public ProductRepository(AlterDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId)
        {
            return await Db.Products.AsNoTracking().Where(s => s.CategoryId == categoryId && s.IsActive).Include(s => s.Category)
                .OrderBy(s => s.Category).ToListAsync();
        }
    }
}
