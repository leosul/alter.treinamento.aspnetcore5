using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Models;
using alter.treinamento.data.Context;

namespace alter.treinamento.data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AlterDbContext _context;

        public CategoryRepository(AlterDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
